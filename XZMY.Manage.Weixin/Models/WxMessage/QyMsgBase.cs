using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    /// <summary>
    /// 企业微信消息推送 基类
    /// </summary>
    [Serializable]
    public class QyMsgBase
    {
        public QyMsgBase()
        {
            touser = "@all";
        }

        /// <summary>
        /// 企业应用ID
        /// </summary>
        public int agentid { get; set; }

        public string msgtype { get; set; }

        /// <summary>
        /// 消息接收者(多个接收者用‘|’分隔，最多支持1000个) 
        /// 默认 关注该企业应用的全部成员
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string toparty { get; set; }

        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string totag { get; set; }
    }

    /// <summary>
    /// 微信消息类型
    /// </summary>
    public static class MsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        public static readonly string Text = "text";

        /// <summary>
        /// 图片消息
        /// </summary>
        public static readonly string Image = "image";

        /// <summary>
        /// 语音消息
        /// </summary>
        public static readonly string Voice = "voice";

        /// <summary>
        /// 视频消息
        /// </summary>
        public static readonly string Video = "video";

        /// <summary>
        /// 文件消息
        /// </summary>
        public static readonly string File = "file";

        /// <summary>
        /// 文本卡片消息
        /// </summary>
        public static readonly string TextCard = "textcard";

        /// <summary>
        /// 图文消息
        /// </summary>
        public static readonly string News = "news";

        /// <summary>
        /// 图文消息
        /// </summary>
        public static readonly string MPNews = "mpnews";
    }
}
