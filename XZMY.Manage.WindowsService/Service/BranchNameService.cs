using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 分店 服务
    /// </summary>
    public class BranchNameService
    {
        public DatabaseHelper db = null;

        public BranchNameService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        /// <summary>
        /// 获取分店Id
        /// </summary>
        /// <param name="hostName"></param>
        public Guid GetBranchNameIdByValue(string hostName)
        {
            var sql = string.Format("SELECT TOP 1 [DataId] FROM [BranchName] WHERE [Value] LIKE '%{0}%'", hostName == "DESKTOP-2OK0OVC" ? "DESKTOP-UT9M89G" : hostName);
            var dt = db.GetDataTable(sql, "BranchName", EProviderName.SqlClient);

            if (dt.Rows.Count > 0)
            {
                return Guid.Parse(dt.Rows[0]["DataId"].ToString());
            }
            return Guid.Empty;
        }
    }
}
