using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Mvc.Controllers
{
    public class OAuth2Controller : Controller
    {
        private static readonly WxCorporationApiService WxCorpService = new WxCorporationApiService();


        /// <summary>
        /// 微信页面授权失败的客户端错误提示页面
        /// 参数：msg [string *] 授权失败的错误提示信息(一个UrlEncode编码过的字符串)
        /// </summary>//todo=>jsc 删除无用代码
        private static readonly string WxAuthErrorPage = Configs.Instance.GetValue("College.WxAuthErrorPage");

        #region 微信接收消息服务器配置  注意 Get  Post 的请求接口路由必须一致
        /// <summary>
        /// 获取企业号 JS_SDK签名
        /// 例如{"appId":"wxf7ed638bb9d6b280","timestamp":1502962344,"nonceStr":"tF4XtH57Qkpfa2HC","signature":"155c2b626c67bd93a5b90b0ff0cd33dae47d41e6"}
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetJS_SDK_Signature()
        {
            var uri = Request.UrlReferrer;
            var url = "";
            if (uri != null)
                url = uri.AbsoluteUri;

            var sEchoStr = WxCorpService.GetQyJsapiTicket(url);
            return Content(sEchoStr);
        }

        /// <summary>
        /// 微信接收消息服务器配置 
        /// 验证接口
        /// </summary>
        /// <param name="msg_signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string msg_signature, string timestamp, string nonce, string echostr)
        {
            var sEchoStr = WxCorpService.WxcptVerify(msg_signature, timestamp, nonce, echostr);
            return Content(sEchoStr);
        }

        /// <summary>
        /// 微信接收消息服务器配置 
        /// 由微信后台发起的消息接收接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            var s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            var bullder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                bullder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            try
            {
                var m_values = XMLHelper.FromXml(bullder.ToString());
                var encryptStr = m_values["Encrypt"].ToString();
                var corpId = m_values["ToUserName"].ToString();
                var encryptKey = WxCorporationApiService.AuthApi.ApiConfig.AgentList["TCLCollege"].EncodingAESKey;
                var dataXml = Cryptography.AES_decrypt(encryptStr, encryptKey, ref corpId); // 解密事件推送数据包
                //用户进入TCL大学应用的推送欢迎语
                var wxWelcomeMsg = Configs.Instance.GetValue("College.WxWelcomeMsg");
                //发送TCL大学应用推送欢迎语
                WxCorpService.EventPushByEnterAgent(dataXml, wxWelcomeMsg);
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger().Error("\n微信=>接收事件推送数据解析异常=>" + ex.Message);
            }

            return Content("");
        }
        #endregion

        /// <summary>
        /// 发起页面授权请求
        /// </summary>
        /// <param name="returnUrl">
        /// 授权成功需要跳转的页面[客户端需要进行UrlEncode编码]
        /// 如果值为空 则会跳转到配置的默认微信页面[配置ID：Wx.CallbackRedirectUrl]
        /// </param>
        /// <returns></returns>
        public ActionResult OAuth2(string returnUrl)
        {
            returnUrl = HttpUtility.UrlEncode(returnUrl);
            var wxAuthUrl = WxCorpService.GetAuthorizeUrl(Url.Action("CallBack") + "?returnUrl=" + returnUrl);
            return Redirect(wxAuthUrl);
        }

        /// <summary>
        /// 微信端回掉处理
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">授权成功需要跳转的页面[客户端需要进行UrlEncode编码]</param>
        /// <returns></returns>
        public ActionResult CallBack(string code, string state, string returnUrl)
        {
            LogFactory.GetLogger().Info("微信=>已经进入CallBack回调函数=>state=" + state);
            //本地回调跳转的Url,一般跳转到我们自己的前端网页地址
            var wxCallbackRedirectUrl = Configs.Instance.GetValue("College.WxCallbackRedirectUrl");
            //微网站警告页面 默认提示语：非企业内部成员，访问被拒绝！
            var wxAuthWarningPage = Configs.Instance.GetValue("College.WxAuthWarningPage");
            //微信网站错误页面
            var wxAuthErrorPage = Configs.Instance.GetValue("College.WxAuthErrorPage");
            try
            {
                returnUrl = string.IsNullOrEmpty(returnUrl) ? wxCallbackRedirectUrl : returnUrl;
                if (string.IsNullOrEmpty(code))
                {
                    return Redirect(wxAuthWarningPage + "您拒绝了授权！");
                }
                //todo: 由于设置信任域名白名单，所以已经不需要验证了 这里还需要验证是否是从微信Api端发起的回掉，防止请求伪造
                //if (state != WxCorporationApiService.AuthApi.ApiConfig.AppSource)
                //{
                //    //WxCorporationApiService.AuthApi.ApiConfig.CallbackRedirectWarningUrl;
                //    //这里只是做了一个简单的验证，后期会用Session或缓存方式来进行伪造验证
                //    //return Redirect(returnUrl + "?msg=" + "验证失败！");
                //}
                var authUserResult = WxCorpService.GetWxUserInfo(code);
                if (authUserResult.errcode != 0) //是否成功
                {
                    LogFactory.GetLogger().Info("微信授权=>授权失败：" + authUserResult.errmsg);
                    return Redirect(wxAuthWarningPage + "微信授权失败！");
                }

                if (string.IsNullOrEmpty(authUserResult.UserId)) // 非企业内部成员
                {
                    LogFactory.GetLogger().Info("微信授权=>授权失败：非企业内部成员");
                    // 跳转到拦截页面 并给与用户提示
                    return Redirect(wxAuthWarningPage);
                }

                //如果是企业内部成员 则需要拿到OpenId
                var openId = WxCorpService.GetAuthorizerOpenId(authUserResult.UserId);
                WxQyUserinfo wxUserinfo = WxCorpService.GetWxUserInfoDetails(authUserResult.user_ticket);
                wxUserinfo.openid = openId;
                //发布微信用户注册事件
                var memberAuthorizeEvent = new WxMemberAuthorizeEvent(wxUserinfo);
                Ioc.Resolve<IEventPublisher>().Publish(memberAuthorizeEvent);
                /*
                 * 后面将完成微信相关业务逻辑，如创建微信相关Service来保存AccessToken相关信息，大致步骤如下：
                 * 1、查询数据库中是否存在改微信用户WxUser,用openId去判断
                 * 2、如果存在：
                 *    1、那么就更新token以及其他属性，保存到数据库之前发布一个“微信授权登录”的事件，事件对象
                 *    中必须有WxUser实体，以及主业务中的token属性
                 *    2、在主体业务层中监听此事件，执行授权登录的操作，返回token给事件对象。
                 * 3、如果不存在：
                 *    1、调用微信Api获取用户的相关信息，保存到数据库之前发布一个“新增用户”的事件
                 *    2、在主体业务层中监听此事件，创建Member用户对象，关联WxUser中的RelatedUserId。
                 *    3、发布“微信授权登录”事件
                 *    4、在主体业务层中监听此事件，执行授权登录的操作，返回token给事件对象。
                 * 4、返回token
                 */
                //这里的token是用于前端调用我们主业务api的凭据，前端可以把此token保存到cookie中去
                //return Redirect(returnUrl + "?token=" + memberAuthorizeEvent.Token);

                LogFactory.GetLogger().Info("地址处理2：" + returnUrl + "&token=" + memberAuthorizeEvent.Token);
                return Redirect(returnUrl + "&token=" + memberAuthorizeEvent.Token);
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger().Error("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "  微信授权登录异常：" + ex);
                return Redirect(wxAuthErrorPage);
            }
        }
    }
}
