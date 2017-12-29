using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 消费信息 服务
    /// </summary>
    public class XfxxService
    {
        public DatabaseHelper db = null;
        public Guid BranchNameDataId = Guid.Empty;

        public XfxxService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        /// <summary>
        /// 获取消费次数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="hykh"></param>
        /// <returns></returns>
        public string GetPaymentCount(DataTable dt, string hykh)
        {
            var result = "0";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["hykh"].ToString().Trim() == hykh.Trim())
                    result = dr[0].ToString();
            }
            return result;
        }

        /// <summary>
        /// 查询消费次数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable GetPaymentCountDataTable(string str)
        {
            var sql = string.Format("SELECT COUNT(0),hykh FROM [xfxx] WHERE hykh IN({0}) GROUP BY hykh", str);
            return db.GetDataTable(sql, "xfxx", EProviderName.SqlClient);
        }

        /// <summary>
        /// 查询消费次数
        /// </summary>
        /// <param name="hykh"></param>
        /// <returns></returns>
        public int GetPaymentCountByHykh(string hykh)
        {
            var sql = string.Format("SELECT COUNT(0),hykh FROM [xfxx] WHERE hykh = '{0}' GROUP BY hykh", hykh);
            var table = db.GetDataTable(sql, "xfxx", EProviderName.OleDB);

            return table.Rows[0][0].ToString().ToInt32(0);
        }

        /// <summary>
        /// 删除消费信息
        /// </summary>
        /// <param name="hykh"></param>
        public void DeleteByHykh(string hykh)
        {
            var sql = string.Format("DELETE FROM [xfxx] WHERE hykh = '{0}' AND BranchNameDataId = '{1}'", hykh, BranchNameDataId);
            db.ExecuteNonQuery(sql, EProviderName.SqlClient);
        }
    }
}
