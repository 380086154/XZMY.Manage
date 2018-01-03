using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models
{
    /// <summary>
    /// 获取微信基础 AccessToken 响应实体
    /// </summary>
    public class WxBaseAccessTokenResult : WxBaseResult
    {
        /// <summary>
        /// 基础支持access_token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
