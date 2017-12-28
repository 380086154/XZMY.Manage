using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2M.Common.DataServiceComponents.Service;
using Newtonsoft.Json;
using XZMY.Manage.Service.Utils.DataDictionary;
using System.Web.UI.WebControls;
using System.Collections;
namespace XZMY.Manage.Web.Content.Custom
{
    /// <summary>
    /// DataDictionary 的摘要说明
    /// </summary>
    public class DataDictionary : IHttpHandler
    {
        public string result = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.Params["action"] ?? "";

            switch (type)
            {
                case "GetGetCatagory":
                    GetGetCatagory(context);
                    break;
                case "GetDataById":
                    GetDataById(context);
                    break;
                default:
                    //Uploader(context);
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }
        /// <summary>
        /// 分类获取数据字段
        /// </summary>
        /// <param name="context"></param>
        private void GetGetCatagory(HttpContext context)
        {
            var key = (context.Request.Params["key"] ?? "").Split(',');
            var listCatagory = DataDictionaryManager.GetCatagories(m => m.Key.IsIn(key));
            Dictionary<string, List<DataDictionaryItem>> list = new Dictionary<string, List<DataDictionaryItem>>();
            
            foreach (var file in listCatagory)
            {
                var dict = file.Value.Select(x => x.Value).ToList();
                list.Add(file.Key, dict);
            }

            result = JsonConvert.SerializeObject(list);
        }

        private void GetDataById(HttpContext context)
        {
            var key = context.Request.Params["key"] ?? "";
            var id = context.Request.Params["id"] ?? "";

            var list = DataDictionaryManager.GetDataById(key, Guid.Parse(id));
            result = JsonConvert.SerializeObject(list);
        }

        public bool IsReusable { get; } = false;
    }
}