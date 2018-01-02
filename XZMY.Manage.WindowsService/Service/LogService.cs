using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 日志 服务
    /// </summary>
    public class LogService
    {
        public DatabaseHelper db = null;
        public Guid BranchNameDataId = Guid.Empty;
        public string IP = string.Empty;
        public string UserId = string.Empty;
        public string UserName = string.Empty;

        public LogService(DatabaseHelper databaseHelper, string ip, Guid userId, string userName)
        {
            db = databaseHelper;
            IP = ip;
            UserId = userId.ToString();
            UserName = userName;
        }

        public LogService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public LogService(DatabaseHelper databaseHelper, Guid branchNameDataId)
        {
            db = databaseHelper;
            BranchNameDataId = branchNameDataId;
        }

        public void Add(LogEntity entity)
        {
            var sql = @"INSERT INTO [SYS_LOG]
           ([DataId],[Title],[Message],[Exception],[Level],[IP],[UserId],[UserName],[CreatedTime])
     VALUES
           (NEWID(),@Title,@Message,@Exception,@Level,@IP,@UserId,@UserName,GETDATE())";

            var sp = new SqlParameter[] {
                new SqlParameter("@Title", entity.Title),//姓名
                new SqlParameter("@Message", entity.Message),//卡类型
                new SqlParameter("@Exception", entity.Exception),//卡类型名称
                new SqlParameter("@Level", entity.Level),//折扣（卡名称）
                new SqlParameter("@IP", entity.IP),//积分
                new SqlParameter("@UserId",entity.UserId),//当前状态
                new SqlParameter("@UserName", entity.UserName),//性别
            };

            db.ExecuteNonQuery(sql, EProviderName.SqlClient, sp);
        }

        public void Add(string title, string message, string exception, LogLevel level)
        {
            Add(new LogEntity
            {
                Title = title,
                Message =message,
                Exception =exception,
                Level =level,
                IP = IP,
                UserId = UserId,
                UserName = UserName
            });
        }

        /// <summary>
        /// 根据 hykh 更新 Hyxx 会员信息
        /// </summary>
        /// <param name="hykh">会员卡号</param>
        public void UpdateInfoByHyxm(string hykh)
        {
            var hyxxDataTable = new DataTable();//获取会员信息

            if (hyxxDataTable.Rows.Count == 0) return;

            var hyxxDataRow = hyxxDataTable.Rows[0];
            var xfxxService = new XfxxService(db);

            var sql = @"UPDATE [hyxx] SET 
                hyxm = @hyxm,
                 klx = @klx,
               klxmc = @klxmc,
                 kmc = @kmc,
                hyjf = @hyjf,
                zxzt = @zxzt,
                  xb = @xb,
                csrq = @csrq,
                dwzy = @dwzy,
                yddh = @yddh,
                gddh = @gddh,
                dzyj = @dzyj,
                zjlx = @zjlx,
                zjhm = @zjhm,
                lxdz = @lxdz,
                qtxx = @qtxx
 WHERE hykh = @hykh AND BranchNameDataId = @BranchNameDataId
";
            var sp = new SqlParameter[] {
                new SqlParameter("@hyxm", hyxxDataRow["hyxm"]),//姓名
                new SqlParameter("@klx", hyxxDataRow["klx"]),//卡类型
                new SqlParameter("@klxmc", hyxxDataRow["klxmc"]),//卡类型名称
                new SqlParameter("@kmc", hyxxDataRow["kmc"]),//折扣（卡名称）
                new SqlParameter("@hyjf", hyxxDataRow["hyjf"]),//积分
                new SqlParameter("@zxzt", hyxxDataRow["zxzt"]),//当前状态
                new SqlParameter("@xb", hyxxDataRow["xb"]),//性别
                new SqlParameter("@csrq", hyxxDataRow["csrq"]),//出生日期
                new SqlParameter("@dwzy", hyxxDataRow["dwzy"]),//职业
                new SqlParameter("@yddh", hyxxDataRow["yddh"]),//移动电话
                new SqlParameter("@gddh", hyxxDataRow["gddh"]),//固定电话
                new SqlParameter("@dzyj", hyxxDataRow["dzyj"]),//电子邮件
                new SqlParameter("@zjlx", hyxxDataRow["zjlx"]),//证件类型
                new SqlParameter("@zjhm", hyxxDataRow["zjhm"]),//证件号码
                new SqlParameter("@lxdz", hyxxDataRow["lxdz"]),//联系地址
                new SqlParameter("@qtxx", hyxxDataRow["qtxx"]),//其他信息

                new SqlParameter("@hykh", hykh),//会员卡号
                new SqlParameter("@BranchNameDataId", BranchNameDataId),//其他信息
            };

            db.ExecuteNonQuery(sql, EProviderName.SqlClient, sp);
        }
    }
}
