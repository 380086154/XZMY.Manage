using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 错误页面
    /// </summary>
    public class ErrorsController : Controller
    {
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                Response.StatusCode = id.Value;
            }
            return View();
        }
    }
}