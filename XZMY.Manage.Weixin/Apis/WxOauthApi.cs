using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Apis
{
     /// <summary>
    /// oauth 授权接口
    /// </summary>
    public class WxOauthApi : WxBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOauthApi(WxAppConfig config = null) : base(config)
        {
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="redirectUri">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string redirectUri)
        {
            return
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={ApiConfig.AppId}&redirect_uri={HttpUtility.UrlEncode(redirectUri)}&response_type=code&scope=snsapi_userinfo&state={HttpUtility.UrlEncode(ApiConfig.AppSource)}#wechat_redirect";
        }

        /// <summary>
        /// 获取授权access_token   (每个用户都是单独唯一)
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        public async Task<WxAccessTokenResult> GetAuthAccessTokenAsync(string code)
        {
            var url = $"{ApiUrl}/sns/oauth2/access_token?appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}&code={code}&grant_type=authorization_code";
            return await RequestWxApi<WxAccessTokenResult>(url);
        }

        /// <summary>
        ///   刷新当前用户授权Token
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <returns></returns>
        public async Task<WxAccessTokenResult> RefreshAuthAccessTokenAsync(string accessToken)
        {

            var url = $"{ApiUrl}/sns/oauth2/refresh_token?appid={ApiConfig.AppId}&grant_type=refresh_token&refresh_token={accessToken}";
            return await RequestWxApi<WxAccessTokenResult>(url);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WxAuthUserResult> GetWxAuthUserInfoAsync(string accessToken, string openId)
        {
            var url= $"{ApiUrl}/sns/userinfo?access_token={accessToken}&openid={openId}";
            return await RequestWxApi<WxAuthUserResult>(url);
        }

        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WxBaseResult> CheckAccessTokenAsync(string accessToken, string openId)
        {
            string url = $"{ApiUrl}/sns/auth?access_token={accessToken}&openid={openId}";
            return await RequestWxApi<WxBaseResult>(url);
        }
    }
}
