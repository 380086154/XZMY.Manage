using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using XZMY.Manage.WindowsService.Utility;
using XZMY.Manage.WindowsService.Service;
using System.Data;
using XZMY.Manage.WindowsService.Model;

namespace XZMY.Manage.WindowsService
{
    public partial class BackupToFileService : ServiceBase
    {
        private string dataPath = string.Empty;
        private string databakPath = string.Empty;
        private DatabaseHelper db;
        private DatabaseHelper originDb = null;
        private BranchService branchService = null;
        private XfxxService xfxxService = null;
        private HyxxService hyxxService = null;
        private LogService logService = null;
        private EmailService emailService = null;
        private ConnectionStringService connectionStringService = null;
        private RandomTimeService randomTimeService = null;
        private BranchDto branchDto = null;//分店信息

        private IList<string> excludeList = new List<string> { "log.txt", "backuplog.xml" };
        private IList<LogDto> sendList = new List<LogDto>();
        private XmlUtility xmlUtility;
        private FileUtility fileUtility;
        private HardwareUtility hardwareUtility;
        private string version = string.Empty;
        private bool IsWait = true;
        private bool IsSendLogFile = false;

        //时间及文件数控制
        private int size = 168;//保留文件数量（假设电脑全天开启，就保留7天的文件数）
        private int sendTime = 10;//邮件发送间隔时间，10分钟
        private int waitTime = 10;//轮询间隔时间，默认10分钟

        public BackupToFileService()
        {
            InitializeComponent();

            dataPath = PathUtility.dataPath;
            databakPath = PathUtility.databakPath;
            connectionStringService = new ConnectionStringService();

            db = connectionStringService.InitDatabaseHelper(dataPath);
            xfxxService = new XfxxService(db);//检查自定义字段是否存在
            originDb = db.DeepClone();

            //读取已发送列表
            xmlUtility = new XmlUtility(databakPath);
            fileUtility = new FileUtility();
            sendList = xmlUtility.GetAll();
        }

        protected override void OnStart(string[] args)
        {
            var thread = new Thread(() =>
            {
                hardwareUtility = new HardwareUtility();
                version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                randomTimeService = new RandomTimeService();
                DirectoryInfo di = new DirectoryInfo(databakPath);
                FileComparer fc = new FileComparer();

                logService = new LogService(db, hardwareUtility.IpAddress, Guid.Empty, hardwareUtility.ComputerName);
                branchService = new BranchService(db, logService);
                branchDto = branchService.GetByValue(hardwareUtility);//获取分店信息
                logService.BranchDataId = branchDto.DataId;
                logService.UserName = branchDto.Name;

                //Thread.Sleep(1000 * 10);//
                try
                {
                    #region 数据备份

                    Log.Add(dataPath);
                    WatcherStart(dataPath, "*.mdb", true, false);

                    #endregion

                    #region 文件备份

                    while (true)
                    {
                        var networkHelper = new NetworkHelper();
                        if (!networkHelper.Status)//无网络不操作
                            return;

                        FileInfo[] fileList = di.GetFiles();
                        Array.Sort(fileList, fc);//按文件创建时间排正序

                        var remaining = fileList.Length - sendList.Count(x => x.TypeName == Type.正常);//剩余未发送文件数

                        sendTime = remaining > 9 ? 1 : 60;//当未备份文件总数超过10个文件，每1分钟发送一个备份，尽快处理了

                        if (remaining > 3 && !branchDto.Value.Contains("BFEBFBFF000506E3"))//打包发送
                        {
                            var dataFolder = DateTime.Now.ToString("yyyy-MM-dd-HHmm");
                            var folder = databakPath + dataFolder;
                            var currentSendList = new List<string>();

                            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                            foreach (var file in fileList)
                            {
                                var fileNewName = folder + "/" + file.Name;

                                if (File.Exists(fileNewName) || sendList != null && sendList.Any(x => x.FileName == file.FullName)
                                    || excludeList.Contains(file.Name)
                                    || file.Name.Contains(".zip")) continue;

                                File.Copy(file.FullName, fileNewName, true);

                                currentSendList.Add(file.FullName);
                                fileUtility.DeleteFile(file.FullName);//删除已复制文件
                            }

                            var fileName = string.Format("{0}（{1}）.zip", dataFolder, currentSendList.Count);
                            fileUtility.CompressRar(folder, databakPath, fileName);//压缩文件

                            SendMailUseGmail(databakPath + fileName, string.Join("<br/>", currentSendList));//发送邮件

                            fileUtility.DeleteFolder(folder);//删除文件夹
                        }
                        else//一个一个发送
                        {
                            foreach (var file in fileList)
                            {
                                if (sendList.Any(x => x.FileName == file.FullName)) continue;

                                if (!IsSendLogFile && file.FullName.Contains(".xml"))
                                {
                                    IsSendLogFile = true;//每次开机只发送一次日志文件
                                }

                                SendMailUseGmail(file.FullName);

                                Thread.Sleep(1000 * 60 * sendTime);//动态计算发送时间，1分钟或则10分钟
                            }
                        }

                        var len = fileList.Length - 166;
                        //删除超出预留文件数以外的文件
                        for (int i = len; i > 0; --i)
                        {
                            var file = fileList[i];
                            if (file.FullName.Contains(".xml")) continue;
                            fileUtility.DeleteFile(file.FullName);//删除文件
                        }

                        fileUtility.RevmoeEmptyFolder(databakPath); //删除根目录的空文件夹

                        var r = randomTimeService.Minute * (1000 * 60);//1-10分钟的随机波动，避免时间太一致，被服务器加入黑名单
                        var sleepNumber = 1000 * 60 * sendTime;
                        //Log.Add("随机数：" + r);
                        if (sleepNumber > r)
                        {
                            sleepNumber = DateTime.Now.Millisecond % 2 == 0
                                ? sleepNumber - r
                                : sleepNumber + r;
                        }
                        Thread.Sleep(sleepNumber);//轮询时间间隔一小时一次
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    Log.Add(ex.Message + "\r\n" + ex.StackTrace);

                    logService.Add("数据备份异常", ex.Message, ex.StackTrace, LogLevel.Error);
                }
            }) { IsBackground = true };
            thread.Start();
        }

        protected override void OnStop()
        {
        }

        #region 备份至数据库

        private FileSystemWatcher fsw = null;

        /// <summary>
        /// 初始化监听
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filter">类型</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="isInclude">是否监听子目录</param>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void WatcherStart(string path, string filter, bool isEnable, bool isInclude)
        {
            fsw = new FileSystemWatcher();//文件系统监听

            //fsw.BeginInit();//初始化监听
            fsw.Path = path;//设置监听路径
            fsw.Filter = filter;//设置监听文件类型
            fsw.IncludeSubdirectories = isInclude;//设置是否监听子目录
            //设置监听的类型
            //fsw.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size | NotifyFilters.LastAccess;
            fsw.NotifyFilter = NotifyFilters.LastWrite;

            fsw.Changed += new FileSystemEventHandler(OnChanged);
            fsw.EnableRaisingEvents = isEnable;//设置是否启用监听
        }

        private object canExecute = null;

        /// <summary>
        /// 当指定目录的文件或目录发生改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(1000);
            //logService.Add("数据备份触发 OnChanged", "------------------- Update() ：" + DateTime.Now);
            UpdateBalance();//实时更新余额

            Thread.Sleep(2000);

            if (canExecute != null)
                return;
            canExecute = false;

            Thread.Sleep(3000);

            var path = string.Empty;

            try
            {
                var networkHelper = new NetworkHelper();
                if (networkHelper.Status)//无网络不操作
                {
                    path = CopyDataToBackup();
                    WriteDataToServer(path);
                }
            }
            catch (Exception ex)
            {
                logService.Add("数据备份异常", ex.Message, ex.StackTrace, LogLevel.Error);
            }
            finally
            {
                canExecute = null;
                if (File.Exists(path)) File.Delete(path);//删除临时数据库文件
            }
        }

        #region 数据备份

        //复制数据文件到备份目录，防止影响美萍运行
        public string CopyDataToBackup()
        {
            var files = Directory.GetFiles(dataPath, "*.mdb");
            var fileFullName = databakPath + string.Format("mphygl{0}.mdb", DateTime.Now.ToString("-yyyy-MM-dd-HHmmss"));
            try
            {
                if (files.Length > 0)
                {
                    File.Copy(files[0], fileFullName);//复制文件至备份目录
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fileFullName;
        }

        //将数据写入服务器
        public void WriteDataToServer(string path)
        {
            //先从服务器中获取总数，然后再从本地获取总数，两相对比计算差值，以这样的方式获取需要备份的行数

            if (string.IsNullOrWhiteSpace(db.ConnectionString_SqlServer))
                connectionStringService.InitDatabaseHelper(path);
            db.ConnectionString_Access = path;

#if DEBUG
            if (branchDto == null)
            {
                hardwareUtility = new HardwareUtility();
                logService = new LogService(db, hardwareUtility.IpAddress, Guid.Empty, hardwareUtility.ComputerName);
                branchService = new BranchService(db, logService);
                branchDto = branchService.GetByValue(hardwareUtility);//获取分店信息
            }
#endif

            xfxxService = new XfxxService(db, branchDto.DataId);
            hyxxService = new HyxxService(db, branchDto.DataId);

            //Log.Add("execute OnChanged event ChangeType = " + branchDto.DataId);

            //必须是 xfxx 在前面，在同步时会根据消费信息查询会员信息，为避免数据异常，所以待 xfxx 同步完成后再同步 hyxx
            var dataTatbles = new string[] { "xfxx", "hyczk", "rz", "zkk", "czk", "hyxx" };
            var needSyncHykh = new Dictionary<string, string>();//需要再次同步的 消费信息 会员卡号

            for (int i = 0; i < dataTatbles.Length; i++)
            {
                var tableName = dataTatbles[i];

                Execute(tableName, needSyncHykh); //执行数据同步
            }

            //单独同步因为删除消费记录不完整的消费信息
            xfxxService.SyncDataByHykhList(needSyncHykh, branchDto.DataId);
        }

        //执行数据同步
        private void Execute(string tableName, Dictionary<string, string> dict)
        {
            var sql = "SELECT COUNT(0) FROM [{0}] ";
            var paymentCountDataTable = new DataTable();//消费信息
            var hyxxDataTable = new DataTable();//会员信息
            var isDataOnServer = hyxxService.IsDataOnServer();//获取服务器是否有数据

            #region 排序设置，为了获取最新的数据

            var orderSql = "";
            switch (tableName)
            {
                case "xfxx":
                    //orderSql = " ORDER BY xfrq desc";
                    orderSql = " ORDER BY CreatedTime desc";//该字段为自定义字段
                    break;
                case "hyczk":
                    orderSql = " ORDER BY khrq desc";
                    break;
                case "rz":
                    orderSql = " ORDER BY sj desc";
                    break;
                case "hyxx":
                    orderSql = " ORDER BY fdid desc";
                    break;
                default:
                    break;
            }

            #endregion

            //开始数据备份
            var serverCount = db.ExecuteScalar(string.Format(sql + "WHERE [BranchDataId] = '" + branchDto.DataId + "'", tableName), EProviderName.SqlClient);//                
            var localCount = db.ExecuteScalar(string.Format(sql, tableName), EProviderName.OleDB);//

            var result = localCount - serverCount;
            if (result != 0)
            {
                Log.Add("execute WriteDataToServer event ================================== localCount - serverCount = " + result);
                logService.Add(string.Format("备份[{0}]表", tableName),
                    string.Format("localCount({0}) - serverCount({1}) = {2}", localCount, serverCount, result),
                    "", LogLevel.Normal);
            }

            if (result > 0)//新增
            {
                var dt = db.GetDataTable(string.Format("SELECT TOP {0} * FROM " + tableName, result) + orderSql
                    , tableName, EProviderName.OleDB);//

                dt.Columns.Add("DataId", System.Type.GetType("System.Guid"));
                dt.Columns.Add("BranchDataId", System.Type.GetType("System.Guid"));
                if (!dt.Columns.Contains("CreatedTime"))
                {
                    dt.Columns.Add("CreatedTime", System.Type.GetType("System.DateTime"));
                }

                if (tableName == "hyxx")//更新消费次数
                {
                    dt.Columns.Add("Count", System.Type.GetType("System.Int32"));
                    var hykhs = string.Join(",", dt.Rows.OfType<DataRow>().Select(x => string.Format("'{0}'", x["hykh"])));
                    paymentCountDataTable = xfxxService.GetPaymentCountDataTable(hykhs);
                }
                else if (tableName == "xfxx")
                {
                    var hykhs = string.Join(",", dt.Rows.OfType<DataRow>().Select(x => string.Format("'{0}'", x["hykh"])));
                    hyxxDataTable = hyxxService.GetByHykhList(hykhs);
                    dt.Columns.Add("hyxm", System.Type.GetType("System.String"));
                    //dt.Columns.Add("Balance", System.Type.GetType("System.Decimal"));
                }

                foreach (DataRow dr in dt.Rows)
                {
                    var hykh = "";
                    if (dr.Table.Columns.Contains("hykh"))
                    {
                        hykh = dr["hykh"].ToString().Trim();
                    }

                    dr["DataId"] = Guid.NewGuid();
                    dr["BranchDataId"] = branchDto.DataId;
                    dr["CreatedTime"] = DateTime.Now;

                    switch (tableName)
                    {
                        case "hyxx"://会员信息
                            {
                                dr["Count"] = xfxxService.GetPaymentCount(paymentCountDataTable, hykh);//获取并赋值消费次数

                                var fdid = dr["fdid"].ToString().Trim();
                                var ss = fdid.Length > 0 ? fdid.Substring(fdid.Length - 2) : "00";
                                var mm = fdid.Length > 0 ? fdid.Substring(fdid.Length - 4, 2) : "00";
                                var hh = fdid.Length > 0 ? fdid.Substring(fdid.Length - 6, 2) : "00";
                                dr["jrrq"] = dr["jrrq"].ToString().ToDateTime().Value.ToString(string.Format("yyyy-MM-dd {0}:{1}:{2}", hh, mm, ss));
                            }
                            break;
                        case "xfxx"://消费信息
                            {
                                dr["hyxm"] = hyxxService.GetHyxm(hyxxDataTable, hykh);//会员姓名赋值

                                var fdid = dr["fdid"].ToString().Trim();
                                var ss = fdid.Length > 0 ? fdid.Substring(fdid.Length - 2) : "00";
                                dr["xfrq"] = dr["xfrq"].ToString().ToDateTime().Value.ToString("yyyy-MM-dd HH:mm:" + ss);

                                logService.Add("数据备份触发 OnChanged", "------------------- Execute() ：" + dr["Balance"]);

                                if (!isDataOnServer) continue;

                                //根据 xfxx 更新 hyxx （主要是 金额 信息）
                                hyxxService.UpdateDigitByHykh(hykh);
                            }
                            break;
                        case "rz"://日志信息
                            if (!isDataOnServer) continue;

                            var rznr = dr["rznr"].ToString();//日志内容

                            if (rznr.Contains("修改会员"))
                            {//操作员：admin，修改会员：蒋小鹿路信息
                                var hyxm = rznr.Split("修改会员")[1].Replace("：", "").Replace("信息", "");//截取会员名称
                                hykh = hyxxService.GetHykhByHyxm(hyxm);
                                hyxxService.UpdateInfoByHyxm(hykh);
                            }
                            else if (rznr.Contains("删除会员") && rznr.Contains("的消费记录"))
                            {//操作员：admin，删除会员蒋冬梅                 的消费记录，消费金额为：18元
                                var hyxm = rznr.Split("删除会员")[1].Replace("：", "").Split("的消费记录")[0].Trim();
                                hykh = hyxxService.GetHykhByHyxm(hyxm);
                                if (hykh.Length > 0)
                                {
                                    xfxxService.DeleteByHykh(hykh);//删除指定消费数据
                                    hyxxService.UpdateDigitByHykh(hykh);//更新金额相关信息

                                    if (!dict.Keys.Contains(hykh))
                                    {
                                        dict.Add(hykh, hyxm);//记录需要同步的会员卡号
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                db.SqlBulkCopyByDataTable(dt, tableName, EProviderName.SqlClient);
            }
            else if (result < 0) //删除
            {
                //删除消费信息已在同步 rz 表时处理
            }
        }

        #endregion

        #endregion

        #region 邮件发送相关

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="fileName"></param>
        public void SendMailUseGmail(string fileName, string fileList = "")
        {
            if (emailService == null) emailService = new EmailService();

            var fromEmailAddress = emailService.FromEmail;
            var toEmailAddress = emailService.ToEmail;

            if (string.IsNullOrWhiteSpace(fromEmailAddress)) fromEmailAddress = "xzmjwx@163.com";
            if (string.IsNullOrWhiteSpace(toEmailAddress)) toEmailAddress = "liuxiaoping.com.cn@163.com";

            var emailFromPassword = "abc123";
            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var entity = new LogDto(fileName, "发送成功", hardwareUtility.IpAddress, hardwareUtility.ComputerName);

            try
            {
                var msg = new System.Net.Mail.MailMessage();
                msg.To.Add(toEmailAddress);

                var attachment = GetAttachment(fileName);
                // Add the file attachment to this e-mail message.
                msg.Attachments.Add(attachment);
                //msg.Attachments.Add(GetAttachment(xmlUtility.LogFileName));

                msg.From = new MailAddress(fromEmailAddress, "小钟美业-" + branchDto.Name, System.Text.Encoding.UTF8);
                /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
                msg.Subject = "[Backup]" + attachment.Name;//邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
                msg.Body = "美萍会员管理系统数据备份服务：" + version//邮件内容
                    + " <br/> 关键值：" + string.Format("{0}|{1}|{2} - {3}", hardwareUtility.CpuID, hardwareUtility.MacAddress, hardwareUtility.DiskID, hardwareUtility.ComputerName)
                    + " <br/> IP地址：" + hardwareUtility.IpAddress
                    + " <br/> 备份时间：" + date
                    + " <br/> 备份文件：" + attachment.Name;

                if (!string.IsNullOrWhiteSpace(fileList))
                {
                    msg.Body += " <br/> " + fileList;
                }

                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = true;//是否是HTML邮件   
                msg.Priority = MailPriority.Normal;//邮件优先级

                SmtpClient client = new SmtpClient();
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, emailFromPassword);
                //上述写你的GMail邮箱和密码   
                client.Port = 25;//Gmail使用的端口
                client.Host = "smtp.163.com";
                client.EnableSsl = true;//经过ssl加密   
                object userState = msg;

                client.SendAsync(msg, userState);
                //简单一点儿可以client.Send(msg);
            }
            catch (SmtpException ex)
            {
                entity.TypeName = Type.异常;
                entity.Description = ex.Message;//记录日志
            }

            sendList.Add(entity);
            xmlUtility.Create(entity);
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Attachment GetAttachment(string fileName)
        {
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(fileName, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(fileName);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileName);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(fileName);

            return data;
        }

        #endregion

        #region 本地数据库操作

        /// <summary>
        /// 实时更新消费信息中的余额
        /// </summary>
        private void UpdateBalance()
        {
            try
            {
                fsw.EnableRaisingEvents = false;

                var XfxxService = new XfxxService(originDb);
                var HyxxService = new HyxxService(originDb);
                var dt = XfxxService.GetLastData();
                if (dt.Rows.Count > 0)
                {
                    var hykh = dt.Rows[0]["hykh"].ToString().Trim();
                    var balance = HyxxService.GetBalance(hykh);//余额
                    XfxxService.UpdateBalance(hykh, dt.Rows[0]["id"].ToString(), balance);
                }
            }
            catch (Exception ex)
            {
                logService.Add("实时更新余额异常 UpdateBalance", "------------------- UpdateBalance() ：" + DateTime.Now);
            }
            finally
            {
                fsw.EnableRaisingEvents = true;
            }
        }

        #endregion
    }
}
