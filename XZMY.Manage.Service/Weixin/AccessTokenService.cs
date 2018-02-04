using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.Helper;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Service.Weixin.Tools;

namespace XZMY.Manage.Service.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccessTokenService
    {
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        //AppSecret - 1f79db43858fa28f00d60d9003f43d64
        //令牌(Token) - E17680A936674932B358
        //消息加解密密钥(EncodingAESKey) - kHYlTj3tNzB2hprUeUf5bd6K8MMJfEa1wztACFyQJAr
        //const string Token = "E17680A936674932B358";

        /// <summary>
        /// Access_Token 数据 Id
        /// </summary>
        private static Guid DataId = Guid.Parse("9ADBE702-88FC-4C65-83CD-752BA7578FDE");//
        private static string Key = "AccessToken";

        private static string AccessToken = string.Empty;
        private static string AppID = "wxdfadf3e2ae2aeb01";
        private static string AppSecret = "1f79db43858fa28f00d60d9003f43d64";

        /// <summary>
        /// 监控 Access_Token
        /// </summary>
        public static void Watch()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    CheckStatus();

                    Thread.Sleep(60000);//每1分钟检查一次是否过期
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// 获取 access_token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            var current = HttpContext.Current;
            if (current != null)
            {//如果是在本地调试，则尝试获取远程服务器中的 access_token
                var request = current.Request;
                if (request != null && !request.Url.Host.Contains("xzmy.site"))
                {
                    var url = "http://www.xzmy.site/api/Weixin/GetAccessToken";
                    return GetServerAccessTokenInfo(current, url);
                }
            }

            return TokenExpired() ? GetNewAccessToken() : AccessToken.Split('#')[0];
        }

        /// <summary>
        /// 获取 access_token 过期时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetAccessTokenExpired()
        {
            var current = HttpContext.Current;
            if (current != null)
            {//如果是在本地调试，则尝试获取远程服务器中的 access_token
                var request = current.Request;
                if (request != null && !request.Url.Host.Contains("xzmy.site"))
                {
                    var url = "http://www.xzmy.site/api/Weixin/GetAccessTokenExpired";
                    var date = GetServerAccessTokenInfo(current, url).ToDateTime();
                    return date.HasValue ? date.Value : DateTimePlus.GetMinDateTime;
                }
            }
            
            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                return AccessToken.Split("#")[1].ToDateTime().Value;
            }
            return DateTimePlus.GetMinDateTime;
        }

        #region Private method

        private static void CheckStatus()
        {
            var item = DataDictionaryManager.GetDataById(Key, DataId);
            if (item == null)
            {//数据不存在，创建
                GetNewAccessToken();
                Create();
            }
            else
            {//更新
                AccessToken = item.Name;
                if (AccessToken.Split('#')[1].ToDateTime() > DateTime.Now) return;

                GetNewAccessToken();
                item.Name = AccessToken;
                Update(item);
            }
        }

        /// <summary>
        /// 创建 AccessToken
        /// </summary>
        /// <returns></returns>
        private static void Create()
        {
            var item = new DataDictionaryItem
            {
                DataId = DataId,
                Name = AccessToken,
                EName = "AccessToken",
                IsDefault = false,
                IsSystem = true,
                Sort = 0,
                State = 1,
                Descr = "微信 Access_Token，站点启动后会定期检查并更新"
            };

            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        /// <summary>
        /// 更新 AccessToken
        /// </summary>
        /// <param name="item"></param>
        private static void Update(DataDictionaryItem item)
        {
            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        private static bool TokenExpired()
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", AccessToken);
            var str = HttpRequestUtil.RequestUrl(url, "GET");

            return HttpRequestUtil.GetJsonValue(str, "errcode") == "42001";
        }

        /// <summary>
        /// 获取新 access_token
        /// </summary>
        /// <returns></returns>
        private static string GetNewAccessToken()
        {
            var token = string.Empty;
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);
            var str = HttpRequestUtil.RequestUrl(url, "GET");

            token = HttpRequestUtil.GetJsonValue(str, "access_token");

            var date = DateTime.Now.AddSeconds(HttpRequestUtil.GetJsonValue(str, "expires_in").ToInt32(0) - 200);
            AccessToken = token + "#" + date;

            LogHelper.Log(AccessToken, "获取新 access_token：" + date);
            LogHelper.Commit();

            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetServerAccessTokenInfo(HttpContext current, string url)
        {
            var str = HttpRequestUtil.RequestUrl(url, "GET");
            return HttpRequestUtil.GetJsonValue(str, "Value");
        }

        #endregion
    }
}
