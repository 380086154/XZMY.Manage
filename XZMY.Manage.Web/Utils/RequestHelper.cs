using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XZMY.Manage.Web.Utils
{

    public static class RequestHelper
    {
        public static string GetActionName(this Controller controller)
        {
            return (string)controller.RouteData.Route.GetRouteData(controller.HttpContext).Values["action"];
        }
        public static string GetControllerName(this Controller controller)
        {
            return (string)controller.RouteData.Route.GetRouteData(controller.HttpContext).Values["controller"];
        }
        public static string GetActionName(this Controller controller,HttpContextBase context)
        {
            return (string)controller.RouteData.Route.GetRouteData(context).Values["action"];
        }
        public static string GetControllerName(this Controller controller, HttpContextBase context)
        {
            return (string)controller.RouteData.Route.GetRouteData(context).Values["controller"];
        }
    }
}