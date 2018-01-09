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
        /// 获取服务器当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetAccessToken()
        {
            return Success("AccessToken", AccessTokenService.GetAccessToken());
        }
    }
}