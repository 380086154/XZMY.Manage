using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Auth.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class AutoCreateAuthActionAttribute : Attribute
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public string ModuleCode { get; set; }
        public bool Visible { get; set; }
    }
}
