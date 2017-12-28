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
    public class VmOrderCourse : ViewBase, IActionViewModel<OrderCourse>
    {
        #region Properties 

        /// <summary>
        /// 课程订单id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("课程订单id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("OrderNo")] 
        //[DisplayName("")] 
        public String OrderNo { get; set; }
        /// <summary>
        /// 课程Id
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("课程Id")] 
        public Guid CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        //[EntAttributes.DBColumn("CourseName")] 
        //[DisplayName("课程名称")] 
        public String CourseName { get; set; }
        /// <summary>
        /// 课程开始日期
        /// </summary>
        //[EntAttributes.DBColumn("BeginDate")] 
        //[DisplayName("课程开始日期")] 
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 课程结束日期
        /// </summary>
        //[EntAttributes.DBColumn("EndDate")] 
        //[DisplayName("课程结束日期")] 
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 学生名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学生名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 电话手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("电话手机号")] 
        public String Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        public String Email { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        //[EntAttributes.DBColumn("Education")] 
        //[DisplayName("年级")] 
        public String Education { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        //[EntAttributes.DBColumn("IdentityCard")] 
        //[DisplayName("身份证")] 
        public String IdentityCard { get; set; }
        /// <summary>
        /// 地区Id
        /// </summary>
        //[EntAttributes.DBColumn("LocationId")] 
        //[DisplayName("地区Id")] 
        public Guid LocationId { get; set; }
        /// <summary>
        /// 地区名称  如中国-重庆
        /// </summary>
        //[EntAttributes.DBColumn("LocationName")] 
        //[DisplayName("地区名称  如中国-重庆")] 
        public String LocationName { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        //[EntAttributes.DBColumn("Address")] 
        //[DisplayName("详细地址")] 
        public String Address { get; set; }
        /// <summary>
        /// 会员Id
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("会员Id")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 学生Id
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生Id")] 
        public Guid StudentId { get; set; }
        /// <summary>
        /// 订单流程状态  1  已报名 2进行中  3已结束  4已取消
        /// </summary>
        //[EntAttributes.DBColumn("ProcessState")] 
        //[DisplayName("订单流程状态  1  已报名 2进行中  3已结束  4已取消")] 
        public EOrderProcessState ProcessState { get; set; }
        /// <summary>
        /// 流程状态名称
        /// </summary>
        public string ProcessStateName
        {
            get
            {
                string strReturn = "";
                if (ProcessState == EOrderProcessState.已取消)
                {
                    strReturn = "已取消";
                }
                else if (PayPrice < DepositPrice|| PayPrice == 0)
                {
                    strReturn = "未付款";
                }
                else if (PayPrice == DepositPrice)
                {
                    strReturn = "已付定金";
                }
                else if (PayPrice == TotalPrice)
                {
                    strReturn = "已付款";
                }
                else if (BeginDate >= DateTime.Now && EndDate <= DateTime.Now)
                {
                    strReturn = "进行中";
                }
                else if (EndDate > DateTime.Now)
                {
                    strReturn = "已结束";
                }
                return strReturn;
            }
        }

        /// <summary>
        /// 报名方式  1 自己报名 2代报名
        /// </summary>
        //[EntAttributes.DBColumn("EnrollType")] 
        //[DisplayName("报名方式  1 自己报名 2代报名")] 
        public EOrderEnrollType EnrollType { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        //[EntAttributes.DBColumn("TotalPrice")] 
        //[DisplayName("订单金额")] 
        public Decimal TotalPrice { get; set; }
        /// <summary>
        /// 订单累计支付金额
        /// </summary>
        //[EntAttributes.DBColumn("PayPrice")] 
        //[DisplayName("订单累计支付金额")] 
        public Decimal PayPrice { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        //[EntAttributes.DBColumn("DepositPrice")] 
        //[DisplayName("定金")]
        public Decimal DepositPrice{get; set;}
        /// <summary>
        /// 是否支付完成 1是 2否
        /// </summary>
            //[EntAttributes.DBColumn("IsPayCompletion")] 
            //[DisplayName("是否支付完成 1是 2否")] 
        public Int32 IsPayCompletion { get; set; }
        /// <summary>
        /// h获取 支付完成状态
        /// </summary>
        public String PayCompletionStateName
        {
            get
            {
                string s = "未知";
                switch (IsPayCompletion)
                {
                    case 1:
                        s = "是";
                        break;
                    case 2:
                        s = "否";
                        break;
                }
                return s;
            }
        }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        //[EntAttributes.DBColumn("PayCompletionTime")] 
        //[DisplayName("支付完成时间")] 
        public DateTime PayCompletionTime { get; set; }
        /// <summary>
        /// 代报名类型  1代理商 2家长 3学生
        /// </summary>
        //[EntAttributes.DBColumn("AgentType")] 
        //[DisplayName("代报名类型  1代理商 2家长 3学生")] 
        public EOrderAgentType AgentType { get; set; }
        /// <summary>
        /// 代报名人员的Id  配合 OrderAgentType 字段使用   如代理商  就是代理商表   家长就是家长表主键id  学生 就是学生表主键Id
        /// </summary>
        //[EntAttributes.DBColumn("AgentId")] 
        //[DisplayName("代报名人员的Id  配合 OrderAgentType 字段使用   如代理商  就是代理商表   家长就是家长表主键id  学生 就是学生表主键Id")] 
        public Guid AgentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("AgentName")] 
        //[DisplayName("")] 
        public String AgentName { get; set; }
        /// <summary>
        /// 评论规划师ID
        /// </summary>
        public Guid CommentPlannerId { get; set; }
        /// <summary>
        /// 评论规划师名字
        /// </summary>
        public String CommentPlannerName { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public String CommentContent { get; set; }
        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CancelTime { get; set; }
        public DateTime CreatedTime { get; set; }
        #endregion

        #region Extendsions

        public OrderCourse CreateNewDataModel()
        {
            var model = new OrderCourse();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.OrderNo = OrderNo;
            model.CourseId = CourseId;
            model.CourseName = CourseName;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Education = Education;
            model.IdentityCard = IdentityCard;
            model.LocationId = LocationId;
            model.LocationName = LocationName;
            model.Address = Address;
            model.MemberId = MemberId;
            model.ProcessState = ProcessState;
            model.EnrollType = EnrollType;
            model.TotalPrice = TotalPrice;
            model.PayPrice = PayPrice;
            model.IsPayCompletion = IsPayCompletion;
            model.PayCompletionTime = PayCompletionTime;
            model.AgentType = AgentType;
            model.AgentId = AgentId;
            model.AgentName = AgentName;
            model.CommentContent = CommentContent;
            model.CommentPlannerId = CommentPlannerId;
            model.CommentPlannerName = CommentPlannerName;
            model.CommentTime = CommentTime;
            model.CancelTime = CancelTime;
            return model;
        }

        public OrderCourse MergeDataModel(OrderCourse model)
        {
            model.OrderNo = OrderNo;
            model.CourseId = CourseId;
            model.CourseName = CourseName;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Education = Education;
            model.IdentityCard = IdentityCard;
            model.LocationId = LocationId;
            model.LocationName = LocationName;
            model.Address = Address;
            model.MemberId = MemberId;
            model.ProcessState = ProcessState;
            model.EnrollType = EnrollType;
            model.TotalPrice = TotalPrice;
            model.PayPrice = PayPrice;
            model.IsPayCompletion = IsPayCompletion;
            model.PayCompletionTime = PayCompletionTime;
            model.AgentType = AgentType;
            model.AgentId = AgentId;
            model.AgentName = AgentName;
            model.CommentContent = CommentContent;
            model.CommentPlannerId = CommentPlannerId;
            model.CommentPlannerName = CommentPlannerName;
            model.CommentTime = CommentTime;
            model.CancelTime = CancelTime;
            return model;
        }
        #endregion
    }

}
