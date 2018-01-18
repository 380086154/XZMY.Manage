using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 字符串进行处理
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="str">分割标识</param>
        /// <returns></returns>
        public static String[] Split(this String source, String str)
        {
            var list = new List<String>();
            while (true)
            {
                var index = source.IndexOf(str);
                if (index < 0) { list.Add(source); break; }
                var rs = source.Substring(0, index);
                //if (!String.IsNullOrEmpty(rs))
                list.Add(rs);
                source = source.Substring(index + str.Length);
            }
            return list.ToArray();
        }
    }
}
