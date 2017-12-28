using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Order
{
    [Serializable]
    public class VmInCome : ViewBase, IActionViewModel<InCome>
    {
        #region Properties 
        public Guid DataId { get; set; }
        /// <summary>
        /// 报名订单Id 如 OrderActivity的Id
        /// </summary>
        //[EntAttributes.DBColumn("OrderId")] 
        //[DisplayName("报名订单Id 如 OrderActivity的Id")] 
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单类型  1活动 2 课程
        /// </summary>
        //[EntAttributes.DBColumn("Type")] 
        //[DisplayName("订单类型  1活动 2 课程")] 
        public Int32 Type { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        //[EntAttributes.DBColumn("PayPrice")] 
        //[DisplayName("支付金额")] 
        public Decimal PayPrice { get; set; }
        /// <summary>
        /// 支付类型 1线上支付2线下支付
        /// </summary>
        //[EntAttributes.DBColumn("PayType")] 
        //[DisplayName("支付类型 1线上支付2线下支付")] 
        public EOrderPayType PayType { get; set; }
        public String PayTypeName  { get; set; }
        /// <summary>
        /// 支付方式 数据字典  如支付宝、微信、中国建设银行、工商银行
        /// </summary>
        //[EntAttributes.DBColumn("PayMode")] 
        //[DisplayName("支付方式 数据字典  如支付宝、微信、中国建设银行、工商银行")] 
        public Guid PayMode { get; set; }
        /// <summary>
        /// 支付方式 数据字典 描述
        /// </summary>
        //[EntAttributes.DBColumn("PayModeName")] 
        //[DisplayName("支付方式 数据字典 描述")] 
        public String PayModeName { get; set; }
     
        /// <summary>
        /// 支付时间
        /// </summary>
        //[EntAttributes.DBColumn("PayTime")] 
        //[DisplayName("支付时间")] 
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        //[EntAttributes.DBColumn("SerialNumber")] 
        //[DisplayName("交易流水号")] 
        public String SerialNumber { get; set; }
        /// <summary>
        /// 支付账号
        /// </summary>
        //[EntAttributes.DBColumn("PayAccount")] 
        //[DisplayName("支付账号")] 
        public String PayAccount { get; set; }
        /// <summary>
        /// 支付人姓名
        /// </summary>
        //[EntAttributes.DBColumn("PayName")] 
        //[DisplayName("支付人姓名")] 
        public String PayName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("备注")] 
        public String Description { get; set; }
        
        #endregion
        #region Extendsions

        public InCome CreateNewDataModel()
        {

            
            var model = new InCome();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.OrderId = OrderId;
            model.Type = Type;
            model.PayPrice = PayPrice;
            model.PayType = PayType;
            model.PayMode = PayMode;
            model.PayModeName = PayModeName;
            model.PayTime = PayTime;
            model.SerialNumber = SerialNumber;
            model.PayAccount = PayAccount;
            model.PayName = PayName;
            model.Description = Description;
            return model;
        }
        public InCome MergeDataModel(InCome model)
        {
            model.OrderId = OrderId;
            model.Type = Type;
            model.PayPrice = PayPrice;
            model.PayType = PayType;
            model.PayMode = PayMode;
            model.PayModeName = PayModeName;
            model.PayTime = PayTime;
            model.SerialNumber = SerialNumber;
            model.PayAccount = PayAccount;
            model.PayName = PayName;
            model.Description = Description;
            return model;
        }
        #endregion
    }
}
