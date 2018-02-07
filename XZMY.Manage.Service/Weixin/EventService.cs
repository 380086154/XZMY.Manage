using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Sys;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Weixin.Manage;
using XZMY.Manage.Service.Weixin.Tools;

namespace XZMY.Manage.Service.Weixin
{
    /// <summary>
    /// 微信事件
    /// </summary>
    public class EventService
    {

        public string Do(XmlDocument doc)
        {
            var weixinUserInfoService = new WeixinUserInfoService();

            var eventType = WeixinXml.GetFromXml(doc, "Event");

            var content = string.Empty;
            if (eventType == "subscribe")
            {//订阅
                weixinUserInfoService.SaveOrUpdate(doc);

                LogHelper.Log("订阅", "Event：" + eventType, LogLevel.Debug);
                var autoResponseService = new AutoResponseService();
                content = autoResponseService.GetValue();

                if (string.IsNullOrWhiteSpace(content))
                    content = "亲爱的，终于等到你。\r\n最新优惠活动将在这里第一时间告诉你。\r\n还可以在这里查询余额，回复手机号即可。";
            }
            else if (eventType == "unsubscribe")
            {//取消订阅
                weixinUserInfoService.SaveOrUpdate(doc);

                LogHelper.Log("取消订阅", "Event：" + eventType, LogLevel.Debug);
                content = "亲爱的，期待您再次关注。";
            }
            else if (eventType == "LOCATION")
            {//获取地理位置

            }

            return content;
        }
    }
}
