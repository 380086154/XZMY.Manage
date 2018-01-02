using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Funding
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class MemberFund : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 基金名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("基金名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 基金编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("基金编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 关联会员ID
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("关联会员ID")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 交易数量  卖出为负数
        /// </summary>
        //[EntAttributes.DBColumn("Quantity")] 
        //[DisplayName("交易数量  卖出为负数")] 
        public Decimal Quantity { get; set; }
        /// <summary>
        /// 类型  1 买入 2卖出
        /// </summary>
        //[EntAttributes.DBColumn("Type")] 
        //[DisplayName("类型  1 买入 2卖出")] 
        public Int32 Type { get; set; }
        /// <summary>
        /// 交易单价  每股多少钱
        /// </summary>
        //[EntAttributes.DBColumn("UnitPrice")] 
        //[DisplayName("交易单价  每股多少钱")] 
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 本次交易金额
        /// </summary>
        //[EntAttributes.DBColumn("Amount")] 
        //[DisplayName("本次交易金额")] 
        public decimal Amount { get; set; }
        public string MemberName { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}
