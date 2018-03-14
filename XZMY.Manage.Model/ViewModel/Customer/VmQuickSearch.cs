using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Customer
{
    /// <summary>
    /// 快速查询
    /// </summary>
    public class VmQuickSearch : ViewBase
    {
        /// <summary>
        /// 分店Id
        /// </summary>
        public Guid BranchDataId { get; set; }
    }
}
