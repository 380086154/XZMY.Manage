using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.Utils
{
    public class ViewBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid DataId { get; set; }

        /// <summary>
        /// 错误信息提示
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页总数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 关键字搜索
        /// </summary>
        public string Keyword { get; set; }

      
    }
}
