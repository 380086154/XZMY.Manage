using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Configuration
{
    /// <summary>
    /// 微信公众号配置对象
    /// </summary>
    public class WxAppConfig
    {
        private string _apiDomainUrl;

        /// <summary>
        /// 应用来源,自定义部分
        /// 如果填写,授权回调时在state中会有赋值
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        /// 公众账号AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 公众账号AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// WebApi域名如:www.domain.com
        /// </summary>
        public string ApiDomain { get; set; }

        /// <summary>
        /// WebApi域名Url如:http://www.domain.com
        /// </summary>
        public string ApiDomainUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiDomainUrl))
                {
                    _apiDomainUrl = "http://" + ApiDomain;
                }
                return _apiDomainUrl;
            }
            set { _apiDomainUrl = value; }
        }

        /// <summary>
        /// 本地回调跳转的Url,一般跳转到我们自己的前端网页地址
        /// </summary>
        public string CallbackRedirectUrl { get; set; }
    }
}
