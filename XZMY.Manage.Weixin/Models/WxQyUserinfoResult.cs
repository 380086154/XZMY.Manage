using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models
{
    /// <summary>
    /// 企业号授权用户
    /// </summary>
    public class WxQyUserinfoResult : WxBaseResult
    {
        /// <summary>
        /// 企业成员UserId 
        /// 注意:如果 UserId 为空则表示当前授权用户不是企业成员
        /// </summary>
        public string UserId { get; set; }

        //手机设备ID
        public string DeviceId { get; set; }

        /// <summary>
        /// 成员票据 
        /// </summary>
        public string user_ticket { get; set; }

        /// <summary>
        /// 过期时间(秒) 2小时
        /// </summary>
        public int expires_in { get; set; }
    }
}
