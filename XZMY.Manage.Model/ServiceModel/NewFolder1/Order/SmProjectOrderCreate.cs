using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Project;

namespace XZMY.Manage.Model.ServiceModel.Order
{

    [Serializable]
    [DataContract]
    public class SmProjectOrderCreate : IActionServiceModel2C<OrderProject>
    {
        public Guid DataId
        {
            get; set;
        }
        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动Id")] 
        [DataMember]
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        //[EntAttributes.DBColumn("ProjectName")] 
        //[DisplayName("活动名称")] 
        [DataMember]
        public String ProjectName { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        //[EntAttributes.DBColumn("ProjectName")] 
        //[DisplayName("活动名称")] 
        [DataMember]
        public String OrderNo { get; set; }
        /// <summary>
        /// 活动开始日期
        /// </summary>
        //[EntAttributes.DBColumn("BeginDate")] 
        //[DisplayName("活动开始日期")] 
        [DataMember]
        public DateTime BeginDate { get; set; } 
        /// <summary>
        /// 活动结束日期
        /// </summary>
        //[EntAttributes.DBColumn("EndDate")] 
        //[DisplayName("活动结束日期")] 
        [DataMember]
        public DateTime EndDate { get; set; } 
        /// <summary>
        /// 取消时间
        /// </summary>
        //[EntAttributes.DBColumn("CancelTime")] 
        //[DisplayName("取消时间")] 
        [DataMember]
        public DateTime CancelTime { get; set; } 
        /// <summary>
        /// 出发城市
        /// </summary>
        [DataMember]
        public String DepartureCity { get; set; }
        /// <summary>
        /// 学生名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学生名称")] 
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 电话手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("电话手机号")] 
        [DataMember]
        public String Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        [DataMember]
        public String Email { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        //[EntAttributes.DBColumn("Education")] 
        //[DisplayName("学历")] 
        [DataMember]
        public String Education { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        //[EntAttributes.DBColumn("Address")] 
        //[DisplayName("详细地址")] 
        [DataMember]
        public String Address { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        //[EntAttributes.DBColumn("IdentityCard")] 
        //[DisplayName("身份证号")] 
        [DataMember]
        public String IdentityCard { get; set; }
        /// <summary>
        /// 地区Id
        /// </summary>
        //[EntAttributes.DBColumn("LocationId")] 
        //[DisplayName("地区Id")] 
        [DataMember]
        public Guid LocationId { get; set; }
        /// <summary>
        /// 地区名称  如中国-重庆
        /// </summary>
        //[EntAttributes.DBColumn("LocationName")] 
        //[DisplayName("地区名称  如中国-重庆")] 
        [DataMember]
        public String LocationName { get; set; }
        /// <summary>
        /// 会员Id
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("会员Id")] 
        [DataMember]
        public Guid MemberId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生ID")] 
        [DataMember]
        public Guid StudentId { get; set; }
        /// <summary>
        /// 订单流程
        /// </summary>
        //[EntAttributes.DBColumn("ProcessState")] 
        //[DisplayName("订单流程")] 
        [DataMember]
        public EOrderProcessState ProcessState { get; set; }

        /// <summary>
        /// 下单报名方式 1自己报名  2代理商报名
        /// </summary>
        //[EntAttributes.DBColumn("EnrollType")] 
        //[DisplayName("下单报名方式")] 
        [DataMember]
        public EOrderEnrollType EnrollType { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        //[EntAttributes.DBColumn("TotalPrice")] 
        //[DisplayName("订单金额")] 
        [DataMember]
        public Decimal TotalPrice { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        //[EntAttributes.DBColumn("PayPrice")] 
        //[DisplayName("支付金额")] 
        [DataMember]
        public Decimal PayPrice { get; set; }

        /// <summary>
        /// 定金
        /// </summary>
        //[EntAttributes.DBColumn("DepositPrice")] 
        //[DisplayName("定金")] 
        [DataMember]
        public Decimal DepositPrice { get; set; }
        /// <summary>
        /// 主办方
        /// </summary>
        //[EntAttributes.DBColumn("Sponsor")] 
        //[DisplayName("主办方")] 
        [DataMember]
        public String Sponsor { get; set; }
        /// <summary>
        /// 是否支付完成 1 是 2否
        /// </summary>
        //[EntAttributes.DBColumn("IsPayCompletion")] 
        //[DisplayName("是否支付完成")] 
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
        /// 代报名类型
        /// </summary>
        //[EntAttributes.DBColumn("AgentType")] 
        //[DisplayName("代报名类型")] 
        [DataMember]
        public EOrderAgentType AgentType { get; set; }
        /// <summary>
        /// 代理商ID
        /// </summary>
        //[EntAttributes.DBColumn("AgentId")] 
        //[DisplayName("代理商ID")] 
        [DataMember]
        public Guid AgentId { get; set; }
        /// <summary>
        /// 代理商名称
        /// </summary>
        //[EntAttributes.DBColumn("AgentName")] 
        //[DisplayName("代理商名称")] 
        [DataMember]
        public String AgentName { get; set; }

        /// 联系人名称
        /// </summary>
        //[EntAttributes.DBColumn("ContactName")] 
        //[DisplayName("联系人姓名")] 
        [DataMember]
        public String ContactName { get; set; }
        /// <summary>
        /// 联系人电话手机号
        /// </summary>
        //[EntAttributes.DBColumn("ContactMobile")] 
        //[DisplayName("联系人电话手机号")] 
        [DataMember]
        public String ContactMobile { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        //[EntAttributes.DBColumn("ContactEmail")] 
        //[DisplayName("联系人邮箱")] 
        [DataMember]
        public String ContactEmail { get; set; }
        /// <summary>
        /// 下单备注
        /// </summary>
        //[EntAttributes.DBColumn("Remark")] 
        //[DisplayName("下单备注")] 
        [DataMember]
        public String Remark { get; set; }
        /// <summary>
        /// 活动评论的规划师ID
        /// </summary>
        //[EntAttributes.DBColumn("CommentPlannerId")] 
        //[DisplayName("活动评论的规划师ID")] 
        [DataMember]
        public Guid CommentPlannerId { get; set; }

        /// <summary>
        /// 活动评论的规划师名称
        /// </summary>
        //[EntAttributes.DBColumn("CommentPlannerName")] 
        //[DisplayName("活动评论的规划师名称")] 
        [DataMember]
        public String CommentPlannerName { get; set; }
        /// <summary>
        /// 活动评论的时间
        /// </summary>
        //[EntAttributes.DBColumn("CommentTime")] 
        //[DisplayName("活动评论的时间")] 
        [DataMember]
        public DateTime CommentTime { get; set; }
        /// <summary>
        /// 活动评论的内容
        /// </summary>
        //[EntAttributes.DBColumn("CommentContent")] 
        //[DisplayName("活动评论的内容")] 
        [DataMember]
        public String CommentContent { get; set; }
        /// <summary>
        /// 课程对象
        /// </summary>
        public VmProjectEdit ModelProject { get; set; }
        public OrderProject CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new OrderProject();
            //model.Id = Id;
            model.ProjectId = ProjectId;
            model.ProjectName = ProjectName;
            model.OrderNo = OrderNo;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.DepartureCity = DepartureCity;
            model.CancelTime = CancelTime;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Education = Education;
            model.Address = Address;
            model.IdentityCard = IdentityCard;
            model.LocationId = LocationId;
            model.LocationName = LocationName;
            model.MemberId = MemberId;
            model.StudentId = StudentId;
            model.ProcessState = ProcessState;
            model.EnrollType = EnrollType;
            model.TotalPrice = TotalPrice;
            model.PayPrice = PayPrice;
            model.DepositPrice = DepositPrice;
            model.Sponsor = Sponsor;
            model.IsPayCompletion = IsPayCompletion;
            model.PayCompletionTime = PayCompletionTime;
            model.AgentType = AgentType;
            model.AgentId = AgentId;
            model.AgentName = AgentName;
            model.ContactName = ContactName;
            model.ContactMobile = ContactMobile;
            model.ContactEmail = ContactEmail;
            model.Remark = Remark;
            model.CommentPlannerId = CommentPlannerId;
            model.CommentPlannerName = CommentPlannerName;
            model.CommentTime = CommentTime;
            model.CommentContent = CommentContent;
            return model;
        }
    }
}
