using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ViewModel
{
    public class VmGetListBase<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class VmSearchBase<T>: VmGetListBase<T>
    {
        public string Keyword { get; set; }
    }
    public class VmGetListBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class VmSearchBase : VmGetListBase
    {
        public Guid Id { get; set; }
        public string Keyword { get; set; }
    }
}
