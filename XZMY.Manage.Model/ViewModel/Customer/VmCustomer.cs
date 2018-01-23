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
    public class VmCustomer : ViewBase, IActionViewModel<XfxxDto>
    {
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分店Id
        /// </summary>
        public Guid BranchDataId { get; set; }     

        public XfxxDto CreateNewDataModel()
        {
            throw new NotImplementedException();
        }

        public XfxxDto MergeDataModel(XfxxDto model)
        {
            throw new NotImplementedException();
        }
    }
}