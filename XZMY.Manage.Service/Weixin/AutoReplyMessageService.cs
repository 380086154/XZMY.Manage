using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Customer;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Weixin.Tools;

namespace XZMY.Manage.Service.Weixin
{
    /// <summary>
    /// 自动回复消息 服务
    /// </summary>
    public class AutoReplyMessageService
    {
        private static HyxxService hyxxService = new HyxxService();

        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string Reply(XmlDocument doc)
        {
            var content = "";//返回内容

            var text = WeixinXml.GetFromXml(doc, "Content").Trim();
            var fromUserName = WeixinXml.GetFromXml(doc, "FromUserName");

            LogHelper.Log("查询余额 text：", text, LogLevel.Debug);

            //查询余额
            if ((text.Length == 11 && text.ToInt64(0) > 0) ||
                (text.Length == 13 && text.Substring(0, 2).ToUpper() == "YE"))
            {
                var phoneNumber = text.Length == 13
                                ? text.Substring(2, 11)
                                : text;
                LogHelper.Log("查询余额 日志：", phoneNumber + " - FromUserName：" + fromUserName, LogLevel.Debug);

                hyxxService.PhoneControl = !hyxxService.PhoneControl;
                return hyxxService.GetDetailsByYddh(phoneNumber, fromUserName);
            }

            if (hyxxService.IsShopAssistant.Contains(fromUserName)
                && text.ToUpper().Contains("CX"))//查询客户信息
            {
                var keywords = text.Substring(2, text.Length - 2);
                return hyxxService.GetDetailsByKeywords(keywords, fromUserName);
            }

            switch (text.ToLower())
            {
                case "？":
                case "?":
                case "help":
                case "帮助":
                case "功能":
                    content = "功能清单\r\n1.回复手机号查询余额;";
                    break;
                case "谁":
                case "是谁":
                case "你是谁":
                case "你是小钟吗":
                    content = "请叫我：机器人 :)";
                    break;
                case "余额":
                case "查余额":
                    content = "告诉我您的手机号，我帮您查一查。";
                    break;
                case "时间":
                case "几点了":
                    content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "营业额":
                case "今日营业额":

                //break;
                case "本周营业额":
                case "本月营业额":
                case "当月营业额":
                case "今年营业额":
                case "去年年营业额":
                case "累计营业额":
                case "猜数字":
                    content = "功能准备中。。。";
                    break;
                case "作者":
                    content = "Xiaoping.Liu";
                    break;
                default://没有匹配 - 转到 图灵 机器人
                    content = TuringRobot.GetTuringMessage(text);
                    break;
            }
            return content;
        }
    }
}
