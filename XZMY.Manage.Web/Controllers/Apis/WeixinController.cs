using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XZMY.Manage.Model;
using XZMY.Manage.Service.Weixin;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 微信 Api
    /// </summary>
    public class WeixinController : ApiControllerBase
    {
        /// <summary>
        /// 获取 access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAccessToken()
        {
            return Success("AccessToken", AccessTokenService.GetAccessToken());
        }

        /// <summary>
        /// 获取 access_token 过期时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAccessTokenExpired()
        {
            return Success("Expired", AccessTokenService.GetAccessTokenExpired());
        }
    }
}