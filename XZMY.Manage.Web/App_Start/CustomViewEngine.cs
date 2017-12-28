using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XZMY.Manage.Web.App_Start
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Auth", // Route name
                //"auth/{action}", // URL with parameters
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index" } // Parameter defaults
            );
        }
    }
}