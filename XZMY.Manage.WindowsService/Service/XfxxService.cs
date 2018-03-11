using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.WindowsService.Utility;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 消费信息 服务
    /// </summary>
    public class XfxxService
    {
        public DatabaseHelper db = null;
        public Guid BranchDataId = Guid.Empty;

        public XfxxService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
            //CheckCustomFieldIsExits();
        }

        public XfxxService(DatabaseHelper databaseHelper, Guid branchNameDataId)
        {
            db = databaseHelper;
            BranchDataId = branchNameDataId;
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

        #region Local

        /// <summary>
        /// 判断自定义字段是否存在
        /// </summary>
        public bool CheckCustomFieldIsExits()
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM [xfxx]";
                var dt = db.GetDataTable(sql, "xfxx", EProviderName.OleDB);

                if (!dt.Columns.Contains("CreatedTime"))//创建时间
                {
                    db.ExecuteNonQuery("ALTER TABLE xfxx ADD COLUMN CreatedTime datetime default now()", EProviderName.OleDB);
                    db.ExecuteNonQuery(@"UPDATE xfxx SET CreatedTime = '1900-01-01 12:00:00'", EProviderName.OleDB);
                }

                if (!dt.Columns.Contains("Balance"))//余额
                {
                    db.ExecuteNonQuery("ALTER TABLE xfxx ADD COLUMN Balance Currency default 0", EProviderName.OleDB);
                    //db.ExecuteNonQuery("ALTER TABLE xfxx ADD COLUMN Balance money", EProviderName.OleDB);
                    //db.ExecuteNonQuery("ALTER TABLE xfxx ADD COLUMN Balance varchar(10) ", EProviderName.OleDB);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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

            if (table.Rows.Count == 0)
                return 0;

            return table.Rows[0][0].ToString().ToInt32(0);
        }

        /// <summary>
        /// 根据会员卡号集合获取消费信息
        /// </summary>
        /// <param name="hykh">会员卡号</param>
        /// <returns></returns>
        public DataTable GetPaymentByHykhList(string hykh)
        {
            var sql = string.Format("SELECT * FROM [xfxx] WHERE hykh IN ({0})", hykh);
            return db.GetDataTable(sql, "xfxx", EProviderName.OleDB);
        }

        /// <summary>
        /// 查询消费次数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable GetPaymentCountDataTable(string str)
        {
            var sql = string.Format("SELECT COUNT(0),hykh FROM [xfxx] WHERE hykh IN({0}) GROUP BY hykh", str);
            return db.GetDataTable(sql, "xfxx", EProviderName.OleDB);
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            return db.ExecuteScalar("SELECT COUNT(0) FROM [xfxx]", EProviderName.OleDB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetLastData()
        {
            var sql = "SELECT TOP 1 * FROM [xfxx] ORDER BY CreatedTime DESC";
            return db.GetDataTable(sql, "xfxx", EProviderName.OleDB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public decimal GetLastDataById(string id)
        {
            var sql = string.Format("SELECT * FROM [xfxx] WHERE id='{0}' ORDER BY CreatedTime DESC", id);
            var dt = db.GetDataTable(sql, "xfxx", EProviderName.OleDB);
            if (dt.Rows.Count == 0)
                return 0;

            return dt.Rows[0]["Balance"].ToString().ToDecimal(0);
        }

        /// <summary>
        /// 获取最后的消费信息
        /// </summary>
        /// <param name="hykh"></param>
        /// <returns></returns>
        public Tuple<bool, DataTable> GetLastData(string hykh)
        {
            var sql = string.Format("SELECT TOP 2 * FROM [xfxx] WHERE hykh = '{0}' ORDER BY CreatedTime DESC", hykh);
            var dt = db.GetDataTable(sql, "xfxx", EProviderName.OleDB);

            if (dt.Rows.Count < 2)
                return new Tuple<bool, DataTable>(false, dt);

            var time1 = dt.Rows.Count >= 1 ? dt.Rows[0]["CreatedTime"].ToString().ToDateTime().Value : DateTimePlus.GetMinDateTime;
            var time2 = dt.Rows.Count >= 2 ? dt.Rows[1]["CreatedTime"].ToString().ToDateTime().Value : DateTimePlus.GetMaxDateTime;

            var flag = time1.ToString("yyyyMMddHH") == time2.ToString("yyyyMMddHH") && (time1.Second - time2.Second) < 5;//两次时间间隔必须很小

            return new Tuple<bool, DataTable>(flag, dt);
        }

        /// <summary>
        /// 更新会员当前余额
        /// </summary>
        /// <param name="hykh"></param>
        /// <param name="id"></param>
        /// <param name="balance">余额</param>
        public decimal UpdateBalance(string hykh, string id, decimal balance)
        {
            var sql = "UPDATE xfxx SET Balance=@Balance WHERE id=@id;";
            var op = new OleDbParameter[] {
                new OleDbParameter("@Balance", balance),//余额
                new OleDbParameter("@id", id)//id
            };
            var result = db.ExecuteNonQuery(sql, EProviderName.OleDB, op);
            return result > 0 ? balance : 0;
        }

        #endregion

        #region Server

        /// <summary>
        /// 删除消费信息
        /// </summary>
        /// <param name="hykh"></param>
        public void DeleteByHykh(string hykh)
        {
            var sql = string.Format("DELETE FROM [xfxx] WHERE hykh = '{0}' AND BranchDataId = '{1}'", hykh, BranchDataId);
            db.ExecuteNonQuery(sql, EProviderName.SqlClient);
        }

        /// <summary>
        /// 根据会员卡号集合同步消费信息
        /// </summary>
        /// <param name="dict"></param>
        /// /// <param name="branchDataId"></param>
        public void SyncDataByHykhList(Dictionary<string, string> dict, Guid branchDataId)
        {
            if (dict == null || dict.Count == 0) return;

            var hykhList = string.Join(",", dict.Keys.Select(x => string.Format("'{0}'", x)));
            var dataTables = GetPaymentByHykhList(hykhList);
            var havaCustomerField = !dataTables.Columns.Contains("CreatedTime");

            dataTables.Columns.Add("hyxm", System.Type.GetType("System.String"));
            dataTables.Columns.Add("DataId", System.Type.GetType("System.Guid"));
            dataTables.Columns.Add("BranchDataId", System.Type.GetType("System.Guid"));

            if (havaCustomerField) //判断自定义字段是否存在
                dataTables.Columns.Add("CreatedTime", System.Type.GetType("System.DateTime"));

            foreach (DataRow dr in dataTables.Rows)
            {
                dr["hyxm"] = dict[dr["hykh"].ToString()];
                dr["DataId"] = Guid.NewGuid();
                dr["BranchDataId"] = branchDataId;

                if (havaCustomerField) dr["CreatedTime"] = DateTime.Now;
            }

            db.SqlBulkCopyByDataTable(dataTables, "Xfxx", EProviderName.SqlClient);
        }

        #endregion
    }
}
