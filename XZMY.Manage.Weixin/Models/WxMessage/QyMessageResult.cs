using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    [Serializable]
    public class QyMessageResult : WxBaseResult
    {
        /// <summary>
        /// 发送失败的成员ID
        /// </summary>
        public string invaliduser { get; set; }

        /// <summary>
        /// 发送失败的部门
        /// </summary>
        public string invalidparty { get; set; }
    }
}
