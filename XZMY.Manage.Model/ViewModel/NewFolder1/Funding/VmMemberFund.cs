using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Funding;

namespace XZMY.Manage.Model.ViewModel.Funding
{
    [Serializable]
    public class VmMemberFund : IActionViewModel<MemberFund>
    {
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("")] 
        public Guid DataId { get; set; }
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
        public String MemberName { get; set; }
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

        #endregion

        #region Extendsions

        public MemberFund CreateNewDataModel()
        {
            var model = new MemberFund();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Code = Code;
            model.MemberId = MemberId;
            model.MemberName = MemberName;
            model.Quantity = Quantity;
            model.Type = Type;
            model.UnitPrice = UnitPrice;
            model.Amount = Amount;
            return model;
        }

        public MemberFund MergeDataModel(MemberFund model)
        {
            model.Name = Name;
            model.Code = Code;
            model.MemberId = MemberId;
            model.MemberName = MemberName;
            model.Quantity = Quantity;
            model.Type = Type;
            model.UnitPrice = UnitPrice;
            model.Amount = Amount;
            return model;
        }
        #endregion
    }
}
