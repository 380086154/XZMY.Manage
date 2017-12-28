using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Utils.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlobalUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Chain<T>(this T obj, Action<T> action)
        {
            action.Invoke(obj);
            return obj;
        }
    }
}
