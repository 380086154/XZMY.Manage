using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Weixin.Models;

namespace XZMY.Manage.Weixin.Service.Implemented
{
    public class WxCorporationApiService : IWxCorporationApiService, IWxMessageApiService
    {
        public static readonly WxCorporationOauthApi AuthApi = new WxCorporationOauthApi();

        public static readonly WxCorporationMailListApi MailListApi = new WxCorporationMailListApi();



        public string WxcptVerify(string signature, string timestamp, string nonce, string echostr)
        {
            //todo=>jsc 
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(AuthApi.ApiConfig.AgentList["TCLCollege"].Token,
                AuthApi.ApiConfig.AgentList["TCLCollege"].EncodingAESKey,
                AuthApi.ApiConfig.CorpId);
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(signature, timestamp, nonce, echostr, ref sEchoStr);
            if (ret != 0)
            {
                //微信URL 验证失败
                LogFactory.GetLogger().Info("ERR: VerifyURL fail, ret: " + ret);
                //System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
            }
            return sEchoStr;
        }

        public string GetAccessToken()
        {
            var wxBaseAccessTokenResult = CacheManager.Get<WxBaseAccessTokenResult>("Wx.BaseAccessTokenResult");

            if (wxBaseAccessTokenResult == null)
            {
                wxBaseAccessTokenResult = AuthApi.GetBaseAccessTokenAsync();
                if (wxBaseAccessTokenResult == null || wxBaseAccessTokenResult.errcode != 0)
                {
                    LogFactory.GetLogger().Info("获取AccessToken失败");
                    return null;
                }
                //微信 服务器的AccessToken有效期为 2小时，此处我们缓存过期时间设置为 1.5小时
                CacheManager.Set("Wx.BaseAccessTokenResult", wxBaseAccessTokenResult, 90);
            }
            return wxBaseAccessTokenResult.access_token;
        }

        public string GetAuthorizeUrl(string redirectUri)
        {
            //LogFactory.GetLogger().Info("微信授权回调地址：" + AuthApi.ApiConfig.ApiDomainUrl + redirectUri);
            return AuthApi.GetAuthorizeUrl(AuthApi.ApiConfig.ApiDomainUrl + redirectUri);
        }

        public WxQyUserinfoResult GetWxUserInfo(string code)
        {
            var accessToken = GetAccessToken();
            return AuthApi.GetQyWxAuthUserInfoAsync(accessToken, code);
        }

        public WxQyUserinfo GetWxUserInfoDetails(string userTicket)
        {
            var accessToken = GetAccessToken();
            return AuthApi.GetQyWxAuthUserInfoDetails(accessToken, userTicket);
        }

        public string GetAuthorizerOpenId(string wxUserId)
        {
            var accessToken = GetAccessToken();
            var userIdToOpenIdResult = AuthApi.GetOpenId(accessToken, wxUserId);
            return userIdToOpenIdResult.openid;
        }


        //public IList<WxDepartment> GetWxDepartments(long? departmentId = null)
        //{
        //    //review
        //    IList<WxDepartment> departmentList = CacheManager.Get<IList<WxDepartment>>("Wx.DepartmentList");
        //    if (departmentId != null)
        //    {
        //        IList<WxDepartment> childList = new List<WxDepartment>();
        //        return GetDepartmentsById(departmentId.Value, childList);
        //    }

        //    if (departmentList == null)
        //    {
        //        var accessToken = GetAccessToken();
        //        var wxDepartmentResult = MailListApi.GetDepartmentList(accessToken, null);
        //        if (wxDepartmentResult.errcode == 0)
        //        {
        //            departmentList = wxDepartmentResult.department;

        //            // 只有查询所有部门时才缓存
        //            //缓存微信所有部门  过期时间为5小时
        //            CacheManager.Set("Wx.DepartmentList", departmentList, 300);
        //        }
        //    }
        //    return departmentList;
        //}

        ///// <summary>
        ///// 获取指定微信部门及其所有子集部门
        ///// </summary>
        ///// <param name="departmentId">部门ID</param>
        ///// <param name="childList">部门列表递归容器</param>
        ///// <returns></returns>
        //private IList<WxDepartment> GetDepartmentsById(long departmentId, IList<WxDepartment> childList)
        //{
        //    //review 

        //    IList<WxDepartment> departmentList = CacheManager.Get<IList<WxDepartment>>("Wx.DepartmentList");
        //    if (departmentList == null)
        //    {
        //        departmentList = GetWxDepartments();
        //    }

        //    if (departmentList != null)
        //    {
        //        foreach (var item in departmentList)
        //        {
        //            if (item.id == departmentId)
        //                childList.Add(item);

        //            if (item.parentid == departmentId) //递归获取子集
        //                GetDepartmentsById(item.id, childList);
        //        }
        //    }
        //    return childList;

        //}

        public bool QySendMessage<T>(T msgModel) where T : QyMsgBase
        {
            try
            {
                msgModel.agentid = Convert.ToInt32(AuthApi.ApiConfig.AgentList["TCLCollege"].AgentId.Trim());
                var accessToken = GetAccessToken();
                var messageService = new WxCorporationMessageApi();
                var msgResult = messageService.SendMessage(accessToken, msgModel);

                return msgResult.errcode == 0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //LogFactory.GetLogger(GetType()).Error("微信API=>企业消息推送程序执行异常:" + msg);
                return false;
            }
        }

        public void EventPushByEnterAgent(string xml, string sendMsg)
        {
            sendMsg = sendMsg ?? @"欢迎来到TCL学堂！
                                让学习和分享自由发生，这里是成就每个人的舞台。
                                点击【TCL学堂】选择您想参与的课程；
                                点击【讲师墙】了解更多大咖并进行预约互动；
                                点击【我的】助您在TCL学堂更好的成长与飞翔。
                                更多在线课程资源学习和获取，请登录TCL大学在线学习平台。
                                登录链接地址：
                                http://tcl.eceibs20.com/login/tcl/index.html";
            if (string.IsNullOrEmpty(sendMsg))
                return;
            sendMsg = sendMsg.Replace("_'_", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
            //.Replace("\n", "<br/>");
            var userId = "";
            int agentId = 0;
            try
            {
                var m_values = XMLHelper.FromXml(xml);

                var corpId = m_values["ToUserName"].ToString();
                var eventStr = m_values["Event"].ToString();
                agentId = Convert.ToInt32(m_values["AgentID"]);
                userId = m_values["FromUserName"].ToString();
                if (eventStr != "subscribe")//subscribe enter_agent
                    return;
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger(GetType())
                    .Error("\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                           "微信事件推送=>EventPushByEnterAgent 数据包XML解析出错:" + ex.Message);
                return;
            }
            //发送推送消息
            var textMsg = new QyTextMsg(sendMsg);
            textMsg.touser = userId;
            textMsg.agentid = agentId;
            QySendMessage(textMsg);
        }

        /// <summary>
        /// 获取企业号JS_SDK 签名数据包
        /// 例如{"appId":"wxf7ed638bb9d6b280","timestamp":1502962344,"nonceStr":"tF4XtH57Qkpfa2HC","signature":"155c2b626c67bd93a5b90b0ff0cd33dae47d41e6"}
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetQyJsapiTicket(string url)
        {
            var accessToken = GetAccessToken();
            var jsTicket = JSAPI.GetQyTickect(accessToken);
            if (string.IsNullOrEmpty(jsTicket))
                return null;

            var appId = AuthApi.ApiConfig.CorpId;
            var nonceStr = JSAPI.CreateRandCode(16);
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timestamp = Convert.ToInt64(ts.TotalSeconds);
            var has = "";
            var signature = JSAPI.GetSignature(jsTicket, nonceStr, timestamp, url, out has);
            if (string.IsNullOrEmpty(signature))
                return null;

            return (new { appId = appId, timestamp = timestamp, nonceStr = nonceStr, signature = signature }).ToJson();
        }
    }
}
