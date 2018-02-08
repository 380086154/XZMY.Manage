using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel;

namespace XZMY.Manage.Model.ViewModel.Sys
{
    public class VmDataDictionaryIndex
    {
        /// <summary>
        /// 发件人
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// 分店列表
        /// </summary>
        public IList<BranchDto> BranchList { get; set; }
    }
}
