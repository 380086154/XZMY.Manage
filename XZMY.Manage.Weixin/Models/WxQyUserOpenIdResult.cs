using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models
{
    public class WxQyUserOpenIdResult : WxBaseResult
    {

        public string openid { get; set; }

        /// <summary>
        /// 该appid在使用微信红包时会用到 
        /// </summary>
        public string appid { get; set; }
    }
}
