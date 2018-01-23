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
    public class VmSearch : HyxxDto
    {
        /// <summary>
        /// 分店名称
        /// </summary>
        public string BranchName { get; set; }
    }
}