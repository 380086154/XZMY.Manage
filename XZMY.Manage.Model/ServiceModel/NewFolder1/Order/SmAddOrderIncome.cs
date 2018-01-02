using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ServiceModel.Order
{
    [Serializable]
    [DataContract]
    public class SmAddOrderIncome : IActionServiceModel2C<InCome>
    {
        public Guid DataId { get; set; }
        [DataMember]
        public Guid OrderId { get; set; }
        [DataMember]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 订单累计支付金额
        /// </summary>
        //[EntAttributes.DBColumn("PayPrice")] 
        //[DisplayName("订单累计支付金额")] 
        [DataMember]
        public Decimal PayPrice { get; set; }
        /// <summary>
        /// 是否支付完成 1是 2否
        /// </summary>
        //[EntAttributes.DBColumn("IsPayCompletion")] 
        //[DisplayName("是否支付完成 1是 2否")] 
        [DataMember]
        public EOrderIsPayCompletion IsPayCompletion { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        //[EntAttributes.DBColumn("PayCompletionTime")] 
        //[DisplayName("支付完成时间")] 
        [DataMember]
        public DateTime PayCompletionTime { get; set; }
        /// <summary>
        /// 支付方式 数据字典  如支付宝、微信、中国建设银行、工商银行
        /// </summary>
        //[EntAttributes.DBColumn("PayMode")] 
        //[DisplayName("支付方式 数据字典  如支付宝、微信、中国建设银行、工商银行")] 
        [DataMember]
        public Guid PayMode { get; set; }
        /// <summary>
        /// 支付方式 数据字典 描述
        /// </summary>
        //[EntAttributes.DBColumn("PayModeName")] 
        //[DisplayName("支付方式 数据字典 描述")] 
        [DataMember]
        public String PayModeName { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        //[EntAttributes.DBColumn("PayTime")] 
        //[DisplayName("支付时间")] 
        [DataMember]
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        //[EntAttributes.DBColumn("SerialNumber")] 
        //[DisplayName("交易流水号")] 
        [DataMember]
        public String SerialNumber { get; set; }
        /// <summary>
        /// 支付账号
        /// </summary>
        //[EntAttributes.DBColumn("PayAccount")] 
        //[DisplayName("支付账号")] 
        [DataMember]
        public String PayAccount { get; set; }
        /// <summary>
        /// 支付人姓名
        /// </summary>
        //[EntAttributes.DBColumn("PayName")] 
        //[DisplayName("支付人姓名")] 
        [DataMember]
        public String PayName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("备注")] 
        [DataMember]
        public String PayDescription { get; set; }

        /// <summary>
        /// 支付类型 1线上支付2线下支付
        /// </summary>
        //[EntAttributes.DBColumn("PayType")] 
        //[DisplayName("支付类型 1线上支付2线下支付")] 
        [DataMember]
        public Int32 PayType { get; set; }


        public InCome CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new InCome();
            model.DataId = Guid.NewGuid();
            model.OrderId = DataId;
            model.Type = (int)OrderType;
            model.PayPrice = PayPrice;
            model.PayType = (EOrderPayType)PayType;
            model.PayMode = PayMode;
            model.PayModeName = PayModeName;
            model.PayTime = PayTime;
            model.SerialNumber = SerialNumber;
            model.PayAccount = PayAccount;
            model.PayName = PayName;
            model.Description = PayDescription;
            return model;
        }

    }

    public enum OrderType
    {
        Project = 1,
        Course = 2
    }
}
