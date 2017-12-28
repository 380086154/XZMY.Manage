using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XZMY.Manage.Service.Handlers.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinHandler
    {
        public HttpRequestBase currentRequest = null;

        public WeixinHandler(HttpRequestBase cr)
        {
            currentRequest = cr;
        }
    }
}
