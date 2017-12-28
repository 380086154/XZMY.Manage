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

        /// <summary>
        /// 根据 xfxx 信息更新 Hyxx 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="hykh"></param>
        public void UpdateByHykh(DataRow dr, string hykh)
        {
            var hyxxDataTable = GetByHykh(hykh);//获取会员信息

            if (hyxxDataTable.Rows.Count == 0) return;
            var hyxxDataRow = hyxxDataTable.Rows[0];

            var sb = new StringBuilder();
            sb.Append("UPDATE [hyxx] SET");
            sb.AppendFormat("hyje = {0},", dr["je"]);
            //卡内金额 - 打折后金额 = 剩余金额
            sb.AppendFormat("knje = {0}", hyxxDataRow["knje"].ToString().ToDecimal(0) - dr["dzhje"].ToString().ToDecimal(0));

            sb.AppendFormat("WHERE hykh = '{0}'", hykh);
            sb.AppendFormat("AND BranchNameDataId = '{0}'", BranchNameDataId);

            db.ExecuteNonQuery(sb.ToString(), EProviderName.SqlClient);
        }
    }
}
