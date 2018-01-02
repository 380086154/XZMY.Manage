using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ViewModel.Sys
{
    public class VmLog : VmSearchBase
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 时间范围
        /// </summary>
        public DateTime[] CreatedTimeRange { get; set; }
    }
}
