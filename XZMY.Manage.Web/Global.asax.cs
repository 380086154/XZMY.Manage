using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Service.Weixin;
using XZMY.Manage.Web.App_Start;

namespace XZMY.Manage.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 站点启动
        /// </summary>
        protected void Application_Start()
        {
            #region 创建公共变量 JavaScript 文件

            FileStream fs = null;
            try
            {
                var path = Server.MapPath("/Content/Custom/");
                var fileName = "share.js";

                if (!File.Exists(path + fileName))
                {
                    fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write);
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        //sw.WriteLine("window.locationHost = '{0}';", GetWebSite(idsLocationHost));
                        //sw.WriteLine("window.orderWebSiteLocationHost = '{0}';", GetWebSite(orderWebSiteLocationHost));

                        var list = DataDictionaryManager.GetAll();
                        //var sb = list.Select(item => $@"{{DataId:'{item.DataId}',Name:'{item.Name}'}}").ToList();
                        var sb = list.Select(x=>string.Format("{DataId:'{0}',Name:'{1}'}", x.DataId, x.Name)).ToList();

                        sw.Write("var dataDictionary =[");
                        sw.Write(string.Join(",", sb));
                        sw.Write("];var getDataDictionary = function (value, row, index) {var name = '';$.each(dataDictionary, function (i, item) {if (item.DataId == value) {name = item.Name;return false;}});return name;}");

                        sw.Flush();
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if(fs!=null) fs.Close();
            }

            #endregion

            AccessTokenService.Watch();//定期检查 Access_Token

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //CustomViewEngine.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 站点停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            try
            {
                //应用程序结束后，执行此代码将会触发一次请求，以防止IIS自动回收后停掉
                Thread.Sleep(10000);
                //System.Diagnostics.Debugger.Break();
                var url = (ConfigurationManager.AppSettings["SiteUrl"] ?? "") + "/index.aspx";
                var req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                var rsp = (System.Net.HttpWebResponse)req.GetResponse();

                var status = rsp.StatusDescription;

                if (DateTime.Now.Hour == 4)
                {
                    LogHelper.Log("Application_End", "每日自动站点回收" + url);
                }
                else
                {
                    LogHelper.Log("Application_End", "非计划内的站点回收" + url);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Application_End", "站点回收时逻辑异常", ex);
            }
        }
    }
}
