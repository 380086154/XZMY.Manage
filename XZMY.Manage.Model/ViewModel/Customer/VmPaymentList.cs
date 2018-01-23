using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Assessment
{
    /// <summary>
    /// 会员信息
    /// </summary>
    [Serializable]
    public class VmPaymentList : XfxxDto
    {
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string hyxm { get; set; }
    }
}