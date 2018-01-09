using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Weixin.Tools
{
    public class HttpRequestUtil
    {
        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            var request = WebRequest.Create(url) as HttpWebRequest;
            var cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                using (var responseStream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        //返回结果网页（html）代码
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
    }
}
