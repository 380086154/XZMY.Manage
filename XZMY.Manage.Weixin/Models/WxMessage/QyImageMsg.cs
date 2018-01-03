using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    /// <summary>
    /// 图片消息
    /// </summary>
    [Serializable]
    public class QyImageMsg : QyMsgBase
    {
        public QyImageMsg()
        {
            msgtype = MsgType.Image;
        }

        public QyImageMsg(string imgMediaId)
        {
            msgtype = MsgType.Image;
            image = new WxImage() { media_id = imgMediaId };
        }

        public WxImage image { get; set; }
    }

    [Serializable]
    public class WxImage
    {
        /// <summary>
        /// 图片媒体文件id
        /// </summary>
        public string media_id { get; set; }
    }
}
