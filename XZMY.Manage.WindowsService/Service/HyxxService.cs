using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 会员信息 服务
    /// </summary>
    public class HyxxService
    {
        public DatabaseHelper db = null;
        public Guid BranchNameDataId = Guid.Empty;

        public HyxxService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        /// <summary>
        /// 根据卡号获取会员信息
        /// </summary>
        /// <param name="hykh"></param>
        /// <returns></returns>
        public DataTable GetByHykh(string hykh)
        {
            var sql = string.Format("SELECT * FROM [Hyxx] where hykh = '{0}' and BranchNameDataId = '{1}'", hykh, BranchNameDataId);
            return db.GetDataTable(sql, "Hyxx", EProviderName.SqlClient);
        }

        public void UpdateByHykh(DataRow dr, string hykh)
        {
            var hyxx = GetByHykh(hykh);//获取会员信息

            var sql = string.Format("");
        }
    }
}
