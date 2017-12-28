using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    public class VmDataDictionaryItem 
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String EName { get; set; }
        public Boolean IsDefault { get; set; }
        public Boolean IsSystem { get; set; }
        public Int32 Sort { get; set; }
        public Int32 State { get; set; }
        public String Descr { get; set; }
        public String CatagoryName { get; set; }
    }
}
