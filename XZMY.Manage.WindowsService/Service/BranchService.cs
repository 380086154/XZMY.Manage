using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XZMY.Manage.WindowsService.Utility;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 分店 服务
    /// </summary>
    public class BranchService
    {
        public DatabaseHelper db = null;
        private LogService logService = null;

        public BranchService(DatabaseHelper databaseHelper, LogService ls)
        {
            db = databaseHelper;
            logService = ls;
        }

        #region Public method

        /// <summary>
        /// 获取分店Id
        /// </summary>
        /// <param name="hostName"></param>
        public Guid GetIdByValue(HardwareUtility hardware)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * FROM [BranchName] WHERE ");
            sb.AppendFormat("[Value] LIKE '%{0}%'", hardware.CpuID);
            sb.AppendFormat(" OR [Value] LIKE '%{0}%'", hardware.DiskID);
            sb.AppendFormat(" OR [Value] LIKE '%{0}%'", hardware.MacAddress);

            var dt = db.GetDataTable(sb.ToString(), "BranchName", EProviderName.SqlClient);

            if (dt.Rows.Count > 0)
            {
                var dataId = dt.Rows[0]["DataId"].ToString();
                var thread = new Thread(() =>
                {
                    try
                    {
                        var oldValue = dt.Rows[0]["Value"].ToString();//CPUID,DISKID,MAC
                        var newValue = hardware.CpuID + "|" + hardware.DiskID + "|" + hardware.MacAddress;//顺序必须保持

                        logService.Add("判断是否自动更新分店信息", "newValue：" + newValue, "oldValue：" + oldValue, LogLevel.Debug);
                        AutoUpdate(dataId, oldValue.Split('|'), newValue.Split('|'));//自动更新分店信息
                    }
                    catch (Exception ex)
                    {
                        logService.Add("自动更新分店信息异常", ex.Message, ex.StackTrace, LogLevel.Error);
                    }
                }) { IsBackground = false };
                thread.Start();

                return Guid.Parse(dataId);
            }
            return Guid.Parse("e7d12da5-50d8-4a01-ae3f-cab673845db7");//测试数据
        }

        /// <summary>
        /// 获取分店名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameById(string id)
        {
            var sql = string.Format("SELECT TOP 1 [Name] FROM [BranchName] WHERE DataId = '{0}'", id);

            var dt = db.GetDataTable(sql, "BranchName", EProviderName.SqlClient);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Name"].ToString();
            }
            return "未知分店";
        }

        #endregion

        #region Private method

        /// <summary>
        /// 有硬件更新后，自动更新分店信息
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        private void AutoUpdate(string dataId, string[] oldValue, string[] newValue)
        {
            var matchCount = 0;
            var oldValueString = string.Join("|", oldValue);
            for (int i = 0; i < newValue.Length; i++)
            {
                if (oldValueString.Contains(newValue[i]))
                {
                    matchCount++;
                }
            }

            //硬件无变化，不需要更新
            if (matchCount == newValue.Length)
                return;

            //硬件相似度大于等于2，看起来是更换了一个硬件
            if (matchCount >= 1)
            {
                var sql = string.Format("UPDATE [BranchName] SET [Value] = '{0}' WHERE DataId = '{1}'", string.Join("|", newValue), dataId);
                db.ExecuteNonQuery(sql, EProviderName.SqlClient);
            }
            else
            {
                //硬件相似度太低，看起来是换电脑了，需要手动确定
                logService.Add("出现硬件更新需手动确认更新", "newValue：" + newValue, "oldValue：" + oldValue, LogLevel.Warn);
            }
        }

        #endregion
    }
}
