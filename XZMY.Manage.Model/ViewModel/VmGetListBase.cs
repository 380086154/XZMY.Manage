using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ViewModel
{
    public class VmGetListBase<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class VmSearchBase<T>: VmGetListBase<T>
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
    }
    public class VmGetListBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class VmSearchBase : VmGetListBase
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }

        public string Keyword { get; set; }
    }
}
