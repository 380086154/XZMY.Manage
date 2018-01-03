using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    [Serializable]
    public class QyTextMsg : QyMsgBase
    {
        public QyTextMsg()
        {
            text = new MsgText();
        }

        public QyTextMsg(string content)
        {
            msgtype = MsgType.Text;
            text = new MsgText() {content = content};
        }

        
        public MsgText text { get; set; }

        /// <summary>
        /// 检验消息内容长度是否符合要求
        /// </summary>
        /// <returns></returns>
        public bool CheckContentLength()
        {
            var count = Encoding.Default.GetByteCount(text.content);
            if (count <= 2048)
                return true;
            return false;
        }
    }

    [Serializable]
    public class MsgText
    {
        /// <summary>
        /// 消息内容，最长不超过2048个字节
        /// 支持换行、以及A标签，即可打开自定义的网页(注意：换行符请用转义过的\n)
        /// </summary>
        public string content { get; set; }
    }
}
