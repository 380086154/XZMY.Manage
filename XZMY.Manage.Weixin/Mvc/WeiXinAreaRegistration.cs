using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XZMY.Manage.Weixin.Mvc
{
    public class WeiXinAreaRegistration : AreaRegistration
    {
        public const string Name = "WeiXin";

        //public override string AreaName => Name;
        public override string AreaName
        {
            get { return Name; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var ns = new[] {"XZMY.Manage.Weixin.Mvc.Controllers.*"};
            context.MapRoute(
                "WeiXinDefault",
                "module-weixin/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}, ns
                );
        }
    }
}
