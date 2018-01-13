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
        public string AccessToken { get; set; }

        public string AccessTokenExpired { get; set; }

        public IList<BranchDto> BranchList { get; set; }
    }
}
