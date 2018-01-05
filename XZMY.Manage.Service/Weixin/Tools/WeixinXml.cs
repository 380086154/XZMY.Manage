using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace XZMY.Manage.Service.Weixin.Tools
{
    public static class WeixinXml
    {
        /// <summary>
        /// 返回消息 Xml
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateTextMessage(XmlDocument xml, string content)
        {
            var sb = new StringBuilder();
            sb.Append("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", GetFromXml(xml, "FromUserName"));
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", GetFromXml(xml, "ToUserName"));
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime2Int(DateTime.Now));
            sb.Append("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
            sb.Append("</xml>");

            return sb.ToString();
        }

        public static int DateTime2Int(DateTime dt)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(dt - startTime).TotalSeconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFromXml(XmlDocument xmlDoc, string name)
        {
            var node = xmlDoc.SelectSingleNode("xml/" + name);
            if (node != null && node.ChildNodes.Count > 0)
            {
                return node.ChildNodes[0].Value;
            }
            return "";
        }
    }
}
