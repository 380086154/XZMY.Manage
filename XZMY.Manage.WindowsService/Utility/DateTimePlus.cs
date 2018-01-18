
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Utility
{
    /// <summary>
    /// 储存常用日期数值的工具类
    /// </summary>
    public class DateTimePlus
    {
        /// <summary>
        /// 获取SQL数据库允许的最小时间值，用于将没有对时间属性赋值的数据库模型实例持久化到数据库
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMinDateTime
        {
            get
            {
                return DateTime.Parse("1/1/1900 12:00:00");
            }
        }

        /// <summary>
        /// 获取SQL数据库允许的最大时间值，用于将没有对时间属性赋值的数据库模型实例持久化到数据库
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMaxDateTime
        {
            get
            {
                return DateTime.Parse("12/31/9999 12:00:00");
            }
        }
    }
}
