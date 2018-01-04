using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XZMY.Manage.Service.Weixin.Tools;

namespace XZMY.Manage.Service.Weixin
{
    /// <summary>
    /// 自动回复消息 服务
    /// </summary>
    public class AutoReplyMessageService
    {
        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string Reply(XmlDocument doc)
        {
            var content = "";
            var text = WeixinXml.GetFromXml(doc, "Content");

            switch (text)
            {
                case "谁":
                case "是谁":
                case "你是谁":
                case "你是小钟吗":
                    content = "请叫我：机器人 :)";
                    break;
                case "余额":
                case "查余额":
                    content = "余额查询中";
                    break;
                case "时间":
                case "几点了":
                    content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "今日营业额":
                case "本周营业额":
                case "本月营业额":
                case "当月营业额":
                case "今年营业额":
                case "去年年营业额":
                case "累计营业额":
                case "猜数字":
                    content = "功能开发中。。。";
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
