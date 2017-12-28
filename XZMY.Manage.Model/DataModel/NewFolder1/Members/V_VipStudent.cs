using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Members
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("V_VipStudent")]
    public class V_VipStudent_List : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        public string ParentsName { get; set; }
        /// <summary>
        /// 家长电话
        /// </summary>
        public string ParentsMobile { get; set; }
        /// <summary>
        /// 是否VIP
        /// </summary>
        public int IsVip { get; set; }
        /// <summary>
        /// 规划师Id
        /// </summary>
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师名称
        /// </summary>
        public string PlannerName { get; set; }
        /// <summary>
        /// 分配时间
        /// </summary>
        public Guid AssignId { get; set; }
        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AssignTime { get; set; }

        public string AssignTimeName
        {
            get
            {
                if (AssignTime == Convert.ToDateTime("1900-01-01 12:00:00"))
                    return " ";
                return AssignTime.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }

        /// <summary>
        /// 分配人名称
        /// </summary>
        public string AssignName { get; set; }
        /// <summary>
        /// VIP申请时间
        /// </summary>
        public DateTime RequestTime { get; set; }


        public string RequestTimeName
        {
            get
            {

                if (RequestTime == Convert.ToDateTime("1900-01-01 12:00:00"))
                    return " ";
                return RequestTime.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }

        /// <summary>
        /// 分配状态：已分配 未分配
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 学生规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 是否需要帮助
        /// </summary>
        public bool IsHelp { get; set; }
        /// <summary>
        /// 是否需要帮助
        /// </summary>
        public String IsHelpName
        {
            get
            {
                if (IsHelp)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }


        /// <summary>
        /// 需要翻译的申请集页
        /// </summary>
        public String IsHelpItemName { get; set; }
        public String HelpItemName
        {
            get
            {
                string str = "";
                string sHelpItemName = IsHelpItemName ?? "";
                if (sHelpItemName.IndexOf("个人信息") > -1)
                    str += string.Format("个人信息 ");
                if (sHelpItemName.IndexOf("联系信息") > -1)
                    str += string.Format("联系信息 ");
                if (sHelpItemName.IndexOf("家庭信息") > -1)
                    str += string.Format("家庭信息 ");
                if (sHelpItemName.IndexOf("留学意向") > -1)
                    str += string.Format("留学意向 ");
                if (sHelpItemName.IndexOf("高中信息") > -1)
                    str += string.Format("高中信息 ");
                if (sHelpItemName.IndexOf("高中课程") > -1)
                    str += string.Format("高中课程 ");
                if (sHelpItemName.IndexOf("高校信息") > -1)
                    str += string.Format("高校信息 ");
                if (sHelpItemName.IndexOf("高校课程") > -1)
                    str += string.Format("高校课程 ");
                if (sHelpItemName.IndexOf("荣誉证书") > -1)
                    str += string.Format("荣誉证书 ");
                if (sHelpItemName.IndexOf("学术兴趣") > -1)
                    str += string.Format("学术兴趣 ");
                if (sHelpItemName.IndexOf("课外活动") > -1)
                    str += string.Format("课外活动 ");
                if (sHelpItemName.IndexOf("个人陈述") > -1)
                    str += string.Format("个人陈述 ");
                if (sHelpItemName.IndexOf("推荐信") > -1)
                    str += string.Format("推荐信 ");
                if (sHelpItemName.IndexOf("申请资料") > -1)
                    str += string.Format("申请资料 ");

                return str;
            }
        }
    }
}
