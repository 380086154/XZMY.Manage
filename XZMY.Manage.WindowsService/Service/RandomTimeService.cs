using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    public class RandomTimeService
    {
        #region Constructor

        /// <summary>
        /// 随机分钟数
        /// </summary>
        public RandomTimeService()
        {
            Init();
        }

        #endregion

        #region Field

        private List<int> DefaultList = new List<int> { 9, 1, 8, 2, 7, 3, 6, 4, 5, 10 };

        /// <summary>
        /// 随机数
        /// </summary>
        private Dictionary<int, bool> RandomList { get; set; }

        #endregion

        /// <summary>
        /// 获取分钟数
        /// </summary>
        public int Minute
        {
            get { return Get(RandomList); }
        }

        #region Private method

        /// <summary>
        /// 初始化随机分钟数
        /// </summary>
        private void Init()
        {
            RandomList = new Dictionary<int, bool>();
            for (int i = 0; i < DefaultList.Count; i++)
            {
                RandomList.Add(DefaultList[i], true);
            }
        }

        /// <summary>
        /// 获取分钟数，确保每次邮件发送都不在同一个时间点
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private int Get(Dictionary<int, bool> dict)
        {
            var obj = dict.FirstOrDefault(x => x.Value == true);
            if (obj.Key != 0)
            {
                dict[obj.Key] = false;
                return obj.Key;
            }

            dict.Clear();
            var list = DefaultList.GetRandomList();
            foreach (var item in list)
            {
                dict.Add(item, true);
            }

            return Get(dict);

            //RandomList = new Dictionary<int, bool>();
            //var list = DefaultList.GetRandomList();
            //foreach (var item in list)
            //{
            //    RandomList.Add(item, true);
            //}

            //return Get(RandomList);
        }

        #endregion
    }
}
