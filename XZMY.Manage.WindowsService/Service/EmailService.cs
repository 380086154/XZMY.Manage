using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailService
    {
        #region Constructor

        public EmailService()
        {
            Init();
        }

        #endregion

        #region Field

        /// <summary>
        /// 发件人列表
        /// </summary>
        private Dictionary<string, bool> FromEmailList { get; set; }

        /// <summary>
        /// 收件人列表
        /// </summary>
        private Dictionary<string, bool> ToEmailList { get; set; }

        #endregion

        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <returns></returns>
        public string FromEmail
        {
            get { return Get(FromEmailList); }
        }

        /// <summary>
        /// 获取收件人
        /// </summary>
        /// <returns></returns>
        public string ToEmail
        {
            get { return Get(ToEmailList); }
        }

        #region Private method

        /// <summary>
        /// 初始化从服务器中获取邮件地址
        /// </summary>
        private void Init()
        {
            var url = "http://www.xzmy.site/api/Sys/GetEmailList";
            var str = HttpRequestUtil.RequestUrl(url, "GET");
            var arr = HttpRequestUtil.GetJsonValue(str, "Value").Split('|');

            FromEmailList = new Dictionary<string, bool>();
            ToEmailList = new Dictionary<string, bool>();

            var fromEmailArray = arr[0].Split("\\r\\n");
            //发件人
            foreach (var item in fromEmailArray)
            {
                if (FromEmailList.Keys.Contains(item)) continue;
                FromEmailList.Add(item, true);
            }

            if (arr.Length > 1)
            {
                //收件人
                foreach (var item in arr[1].Split("\\r\\n"))
                {
                    if (ToEmailList.Keys.Contains(item)) continue;
                    ToEmailList.Add(item, true);
                }
            }

            //将发件人加入收件人列表
            foreach (var item in fromEmailArray)
            {
                if (ToEmailList.Keys.Contains(item)) continue;
                ToEmailList.Add(item, true);
            }
        }

        /// <summary>
        /// 获取 Email 确保邮箱地址按照队列一个一个的使用。
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private string Get(Dictionary<string, bool> dict)
        {
            var obj = dict.FirstOrDefault(x => x.Value == true);
            if (obj.Key != null)
            {
                dict[obj.Key] = false;
                return obj.Key;
            }

            var list = dict.Keys.ToList().GetRandomList();
            dict.Clear();
            foreach (var item in list)
            {
                dict.Add(item, true);
            }

            return Get(dict);

            //var dictCount = dict.Keys.Count;
            //var strKey = new string[dictCount];
            //dict.Keys.CopyTo(strKey, 0);
            //for (int i = 0; i < dict.Count; i++)
            //{
            //    dict[strKey[i]] = true;
            //}
            //obj = dict.FirstOrDefault(x => x.Value == true);
            //dict[obj.Key] = false;
            //return obj.Key;
        }

        #endregion
    }
}
