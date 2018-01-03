using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
 /// <summary>
    /// 文本卡片消息
    /// </summary>
    [Serializable]
    public class QyTextCardMsg : QyMsgBase
    {
        public QyTextCardMsg()
        {
            msgtype = MsgType.TextCard;
        textcard=    new MsgTextCard();
        }

        public QyTextCardMsg(string title, string url, string description)
        {
            msgtype = MsgType.TextCard;
                    textcard=    new MsgTextCard();

            textcard.title = title;
            textcard.url = url;
            textcard.description = description;
        }

        public MsgTextCard textcard { get; set; }
    }

    [Serializable]
    public class MsgTextCard
    {
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 点击后跳转的链接。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断
        /// 支持HTML代码
        /// 卡片消息的展现形式非常灵活，支持使用br标签或者空格来进行换行处理，也支持使用div标签来使用不同的字体颜色，
        /// 目前内置了3种文字颜色：灰色(gray)、高亮(highlight)、默认黑色(normal)，将其作为div标签的class属性即可，具体用法请参考上面的示例。
        /// </summary>
        public string description { get; set; }

        public string btntxt { get; set; }
    }
}
