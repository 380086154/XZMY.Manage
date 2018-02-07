using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// 将 List 随机排序并返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(this List<T> list)
        {
            var copyArray = new T[list.Count];
            list.CopyTo(copyArray);

            var copyList = new List<T>();
            copyList.AddRange(copyArray);

            var outputList = new List<T>();
            var r = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
                var index = r.Next(0, copyList.Count);
                var item = copyList[index];

                copyList.Remove(item);
                outputList.Add(item);
            }

            return outputList;
        }
    }
}
