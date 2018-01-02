using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.DataModel
{
    public class WxUserLogon
    {
        public Guid DataId { get; set; }

        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string Scope { get; set; }

        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 上次刷新access_token的时间
        /// </summary>
        public DateTime? LastRefreshTokenTime { get; set; }

        public virtual WxUser WxUser { get; set; }
    }
}
