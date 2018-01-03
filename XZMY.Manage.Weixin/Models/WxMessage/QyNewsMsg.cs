using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    /// <summary>
    /// 图文消息
    /// </summary>
    [Serializable]
    public class QyNewsMsg : QyMsgBase
    {
        public QyNewsMsg()
        {
            msgtype = MsgType.News;
            news = new WxNews();
        }

        public QyNewsMsg(string title, string description, string url, string picurl)
        {
            msgtype = MsgType.News;

            news = new WxNews();
            news.articles.Add(new Article() { title = title, description = description, url = url, picurl = picurl });
        }

        public WxNews news { get; set; }
    }

    [Serializable]
    public class WxNews
    {
        public WxNews()
        {
            articles = new List<Article>();
        }

        /// <summary>
        /// 图文消息 最多支持1到8条图文
        /// </summary>
        public List<Article> articles { get; set; }
    }

    [Serializable]
    public class Article
    {
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640X320，小图80x80。
        /// </summary>
        public string picurl { get; set; }
    }
}
