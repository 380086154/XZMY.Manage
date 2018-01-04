using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Service.Weixin.Tools
{
    /// <summary>
    /// 图灵机器人
    /// </summary>
    public class TuringRobot
    {
        //API地址:http://www.tuling123.com/openapi/api
        //APIkey:a403c38c977042ef8fcddeff061539aa

        /// <summary>
        /// 获取 图灵 机器人自动回复信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetTuringMessage(string info)
        {
            var jsonStr = RequestTuring(info);

            LogHelper.Log("TuringRobot 日志：", jsonStr, LogLevel.Debug);

            if (GetJsonValue(jsonStr, "code") == "40002")
            {
                return GetJsonValue(jsonStr, "text");
            }
            if (GetJsonValue(jsonStr, "code") == "200000")
            {
                return GetJsonValue(jsonStr, "text") + GetJsonValue(jsonStr, "url");
            }
            return "Sorry, 我需要充电了，晚点才能和你聊。";
        }

        public static string RequestTuring(string info)
        {
            var current = HttpContext.Current;

            if (current == null)
                return string.Empty;

            info = current.Server.UrlEncode(info);
            var url = "http://www.tuling123.com/openapi/api?key=a403c38c977042ef8fcddeff061539aa";
            return RequestUrl(url, "GET");
        }

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
