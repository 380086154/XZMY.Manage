using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ViewModel.Order
{
    [Serializable]
    public class VmOrderProjectDetails : VmOrderProject
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public String ProjectCode { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        //[EntAttributes.DBColumn("LoginName")] 
        //[DisplayName("登录名")] 
        public String LoginName { get; set; }

        public String AgentContactName { get; set; }

        public String AgentContactMobile { get; set; }
    }
}
