using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Apis
{
    /// <summary>
    /// http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
    /// 微信JS-SDK使用权限签名算法
    /// </summary>
    public class JSAPI
    {
        /// <summary>
        /// 获取企业号 jsapi_ticket
        /// jsapi_ticket是企业号用于调用微信JS接口的临时票据。
        /// 正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
        /// 由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string GetQyTickect(string access_token)
        {
            var jsTicketStr = CacheManager.Get<string>("Wx.QyJS_SDK_Tickect");
            if (!string.IsNullOrEmpty(jsTicketStr))
                return jsTicketStr;

            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
                access_token);
            string response = HttpService.Get(url);
            var jsTicket = response.ToJson<JsTicketResult>();
            if (jsTicket.errcode != 0)
                return string.Empty;
            //tickect 微信有效期为120分钟(7200秒)，此处缓存100分钟
            CacheManager.Set("Wx.QyJS_SDK_Tickect", jsTicket.ticket, 100);
            return jsTicket.ticket;
        }


        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url,
            out string string1)
        {
            url = url ?? "";
            var string1Builder = new StringBuilder();
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                .Append("noncestr=").Append(noncestr).Append("&")
                .Append("timestamp=").Append(timestamp).Append("&")
                .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            string1 = string1Builder.ToString();
            return GetSinature(string1);
        }

        private static string GetSinature(string sinature)
        {
            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(sinature);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception ex)
            {
                //LogFactory.GetLogger().Debug("微信JS_SDK生成签名失败: code= -40003" + "||msg=>" + ex.Message);
            }
            return hash;
        }

        public static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
    }
}
