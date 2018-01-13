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
    /// 会员信息 服务
    /// </summary>
    public class HyxxService
    {
        public DatabaseHelper db = null;
        public Guid BranchDataId = Guid.Empty;

        public HyxxService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public HyxxService(DatabaseHelper databaseHelper, Guid branchNameDataId)
        {
            db = databaseHelper;
            BranchDataId = branchNameDataId;
        }

        /// <summary>
        /// 根据会员姓名 获取会员卡号
        /// </summary>
        /// <param name="hyxm"></param>
        /// <returns></returns>
        public string GetHykhByHyxm(string hyxm)
        {
            var sql = string.Format("SELECT hykh FROM [Hyxx] WHERE hyxm = '{0}' ", hyxm);
            var table = db.GetDataTable(sql, "Hyxx", EProviderName.OleDB);

            return table.Rows.Count == 0
                ? string.Empty
                : table.Rows[0][0].ToString().Trim();
        }

        /// <summary>
        /// 根据卡号获取本地会员信息
        /// </summary>
        /// <param name="hykh"></param>
        /// <returns></returns>
        public DataTable GetByHykh(string hykh)
        {
            var sql = string.Format("SELECT * FROM [Hyxx] WHERE hykh = '{0}' ", hykh);
            return db.GetDataTable(sql, "Hyxx", EProviderName.OleDB);
        }

        /// <summary>
        /// 判断服务器中是否有数据
        /// </summary>
        /// <param name="hykh">会员卡号</param>
        /// <returns></returns>
        public bool IsDataOnServerByHykh(string hykh)
        {
            var sql = string.Format("SELECT COUNT(0) FROM [Hyxx] WHERE hykh = '{0}' AND BranchDataId = '{1}' ", hykh, BranchDataId);
            var result = db.ExecuteScalar(sql, EProviderName.SqlClient);

            return result > 0;
        }

        /// <summary>
        /// 根据表名获取服务器中是否有数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsDataOnServer()
        {
            var sql = string.Format("SELECT COUNT(0) FROM [Hyxx] WHERE BranchDataId = '{0}' ", BranchDataId);
            var result = db.ExecuteScalar(sql, EProviderName.SqlClient);

            return result > 0;
        }

        /// <summary>
        /// 根据会员姓名获取本地会员信息
        /// </summary>
        /// <param name="hyxm"></param>
        /// <returns></returns>
        public DataTable GetByHyxm(string hyxm)
        {
            var sql = string.Format("SELECT * FROM [Hyxx] WHERE hyxm = '{0}'", hyxm);
            return db.GetDataTable(sql, "Hyxx", EProviderName.OleDB);
        }

        /// <summary>
        /// 根据 hykh 更新 Hyxx 金额相关
        /// </summary>
        /// <param name="hykh"></param>
        public void UpdateDigitByHykh(string hykh)
        {
            if (!IsDataOnServerByHykh(hykh)) return;//首先判断服务器数据库中是否有数据

            var hyxxDataTable = GetByHykh(hykh);//获取本地会员信息
            if (hyxxDataTable.Rows.Count == 0) return;

            var hyxxDataRow = hyxxDataTable.Rows[0];
            var xfxxService = new XfxxService(db);

            var sb = new StringBuilder();
            sb.Append("UPDATE [hyxx] SET");
            sb.AppendFormat(" hyje = {0},", hyxxDataRow["hyje"]);//累计消费金额
            //卡内金额 - 打折后金额 = 剩余金额
            sb.AppendFormat(" knje = {0},", hyxxDataRow["knje"].ToString().ToDecimal(0));
            sb.AppendFormat(" Count = {0}", xfxxService.GetPaymentCountByHykh(hykh));//更新消费次数

            sb.AppendFormat(" WHERE hykh = '{0}'", hykh);
            sb.AppendFormat(" AND BranchDataId = '{0}'", BranchDataId);

            db.ExecuteNonQuery(sb.ToString(), EProviderName.SqlClient);
        }

        /// <summary>
        /// 根据 hykh 更新 Hyxx 会员信息
        /// </summary>
        /// <param name="hykh">会员卡号</param>
        public void UpdateInfoByHyxm(string hykh)
        {
            var hyxxDataTable = GetByHykh(hykh);//获取会员信息

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
 WHERE hykh = @hykh AND BranchDataId = @BranchDataId
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
                new SqlParameter("@BranchDataId", BranchDataId),//其他信息
            };

            db.ExecuteNonQuery(sql, EProviderName.SqlClient, sp);
        }
    }
}
