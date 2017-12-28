using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Web.Modules
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogModule : IHttpModule
    {
        /// <summary>
        /// /
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_EndRequest(object sender, EventArgs e)
        {
            LogHelper.Commit();
        }
    }
}