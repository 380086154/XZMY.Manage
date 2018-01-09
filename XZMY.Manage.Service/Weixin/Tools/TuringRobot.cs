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

            var code = HttpRequestUtil.GetJsonValue(jsonStr, "code");
            if (code == "40002")
            {
                return HttpRequestUtil.GetJsonValue(jsonStr, "text");
            }
            if (code == "200000")
            {
                return HttpRequestUtil.GetJsonValue(jsonStr, "text") + HttpRequestUtil.GetJsonValue(jsonStr, "url");
            }
            return "我饿了，需要去充电，晚点才能和你聊。";
        }

        public static string RequestTuring(string info)
        {
            var current = HttpContext.Current;

            if (current == null)
                return string.Empty;

            info = current.Server.UrlEncode(info);
            var url = "http://www.tuling123.com/openapi/api?key=a403c38c977042ef8fcddeff061539aa";
            return HttpRequestUtil.RequestUrl(url, "GET");
        }        
    }
}
