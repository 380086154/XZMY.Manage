using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Order
{

    [Serializable]
    public class VmOrderProject : ViewBase, IActionViewModel<OrderProject>
    {
        #region Properties 

        /// <summary>
        /// 活动订单id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("活动订单id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("OrderNo")] 
        //[DisplayName("")] 
        public String OrderNo { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动Id")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        //[EntAttributes.DBColumn("ProjectName")] 
        //[DisplayName("活动名称")] 
        public String ProjectName { get; set; }
        /// <summary>
        /// 活动开始日期
        /// </summary>
        //[EntAttributes.DBColumn("BeginDate")] 
        //[DisplayName("活动开始日期")] 
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 活动结束日期
        /// </summary>
        //[EntAttributes.DBColumn("EndDate")] 
        //[DisplayName("活动结束日期")] 
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 学生名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学生名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("手机号")] 
        public String Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        public String Email { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }        
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
                else if (PayPrice < DepositPrice || PayPrice==0)
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
        public Decimal DepositPrice { get; set; }
        /// <summary>
        /// 主办方
        /// </summary>
        public string Sponsor { get; set; }
        /// <summary>
        /// 是否支付完成 1是 2否
        /// </summary>
        //[EntAttributes.DBColumn("IsPayCompletion")] 
        //[DisplayName("是否支付完成 1是 2否")] 
        public EOrderIsPayCompletion IsPayCompletion { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        //[EntAttributes.DBColumn("PayCompletionTime")] 
        //[DisplayName("支付完成时间")] 
        public DateTime PayCompletionTime { get; set; }
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
        public String PayDescription { get; set; }

        /// <summary>
        /// 支付类型 1线上支付2线下支付
        /// </summary>
        //[EntAttributes.DBColumn("PayType")] 
        //[DisplayName("支付类型 1线上支付2线下支付")] 
        public Int32 PayType { get; set; }

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
        /// 联系人名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学生名称")] 
        public String ContactName { get; set; }
        /// <summary>
        /// 联系人电话手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("电话手机号")] 
        public String ContactMobile { get; set; }

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
        #endregion

        #region Member

        /// <summary>
        /// 真实姓名
        /// </summary>
        //[EntAttributes.DBColumn("RealName")] 
        //[DisplayName("真实姓名")] 
        public String RealName { get; set; }
        /// <summary>
        /// 所在地名称
        /// </summary>
        //[EntAttributes.DBColumn("LocationPathName")] 
        //[DisplayName("所在地名称")] 
        public String LocationPathName { get; set; }

        public DateTime CreatedTime { get; set; }
     

        #endregion

        #region Extendsions

        public OrderProject CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            if (MemberId == Guid.Empty) MemberId = Guid.NewGuid();
            var model = new OrderProject();
            //model.Id = Id;
            model.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            model.ProjectId = ProjectId;
            model.ProjectName = ProjectName;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Education = Education;
            model.IdentityCard = IdentityCard;
            model.LocationId = LocationId == Guid.Empty ? Guid.Parse("1F1F0E5D-129F-469D-B3B0-493E909F2417") : LocationId;
            model.LocationName = LocationName;
            model.Address = Address;
            model.MemberId = MemberId;
            model.ProcessState = ProcessState ==0 ? EOrderProcessState.已报名 : ProcessState;
            model.EnrollType = EnrollType;
            model.TotalPrice = TotalPrice;
            model.PayPrice = PayPrice;
            model.IsPayCompletion = IsPayCompletion;
            model.PayCompletionTime = PayCompletionTime;
            model.AgentType = AgentType;
            model.AgentId = AgentId;
            model.AgentName = AgentName;
            model.ContactName = ContactName;
            model.ContactMobile = ContactMobile;
            model.CommentContent = CommentContent;
            model.CommentPlannerId = CommentPlannerId;
            model.CommentPlannerName = CommentPlannerName;
            model.CommentTime = CommentTime;
            return model;
        }

        public OrderProject MergeDataModel(OrderProject model)
        {
            model.OrderNo = OrderNo;
            model.ProjectId = ProjectId;
            model.ProjectName = ProjectName;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Education = Education;
            model.IdentityCard = IdentityCard;
            model.LocationId = LocationId == Guid.Empty ? Guid.Parse("1F1F0E5D-129F-469D-B3B0-493E909F2417") : LocationId;
            model.LocationName = LocationName;
            model.Address = Address;
            model.MemberId = MemberId;
            model.ProcessState = ProcessState;
            model.EnrollType = EnrollType;
            model.TotalPrice = TotalPrice;
            model.PayPrice = PayPrice;
            model.DepositPrice = DepositPrice;
            model.IsPayCompletion = IsPayCompletion;
            model.PayCompletionTime = PayCompletionTime;
            model.AgentType = AgentType;
            model.AgentId = AgentId;
            model.AgentName = AgentName;
            model.ContactName = ContactName;
            model.ContactMobile = ContactMobile;
            model.CommentContent = CommentContent;
            model.CommentPlannerId = CommentPlannerId;
            model.CommentPlannerName = CommentPlannerName;
            model.CommentTime = CommentTime;
            return model;
        }
        
        public InCome CreateNewIncomeDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new InCome();
            //model.Id = Guid.NewGuid();
            model.OrderId = DataId;
            model.Type = 1;
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

        public Member CreateNewMemberDataModel()
        {
            if (MemberId == Guid.Empty) MemberId = Guid.NewGuid();
            var model = new Member();
            //model.Id = MemberId;
            model.AgentId = AgentId;
            model.LoginName = Mobile;
            model.Password = "123456".ToMd5();//默认密码
            model.Type = 1;
            model.Email = Email;
            model.Mobile = Mobile;
            model.BirthDate = BirthDate;
            model.RegisteredTime = DateTime.Now;
            model.State = Enum.EState.启用;
            return model;
        }

        public Student CreateNewStudentDataModel()
        {
            if (MemberId == Guid.Empty) MemberId = Guid.NewGuid();
            var model = new Student();
            //model.Id = Guid.NewGuid();
            model.MemberId = MemberId;
            model.Name = Name;
            model.Email = Email;
            model.Mobile = Mobile;
            model.LocationId = LocationId;
            model.LocationPathName = LocationName;
            return model;
        }
        #endregion
    }
}
