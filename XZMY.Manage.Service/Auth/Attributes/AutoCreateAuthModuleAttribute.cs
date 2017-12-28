using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Auth.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoCreateAuthModuleAttribute : Attribute
    {
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}
