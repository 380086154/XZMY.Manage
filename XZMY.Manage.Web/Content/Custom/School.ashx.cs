using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2M.Common.DataServiceComponents.Service;
using Newtonsoft.Json;
namespace XZMY.Manage.Web.Content.Custom
{
    /// <summary>
    /// School 的摘要说明
    /// </summary>
    public class School : IHttpHandler
    {
        public string result = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["action"];

            switch (type)
            {
                case "GetSchoolCategoryLevel":
                    GetSchoolCategoryLevel(context);
                    break;
                case "GetSchoolCategoryParentId":
                    GetSchoolCategoryParentId(context);
                    break;
                default:
                    //Uploader(context);
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }
        /// <summary>
        /// 查询一级
        /// </summary>
        /// <param name="context"></param>
        private void GetSchoolCategoryLevel(HttpContext context)
        {
            string Level = context.Request["Level"].ToString();

            //List<SelectListItem> dllCountry = new List<SelectListItem>();
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.School.SchoolCategory>()
            {
                ColumnMember = m => m.Level,
                ColumnValue = Level
            };
            //dllCountry = service.Invoke().Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }).ToList();
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        /// <summary>
        /// 查询父级Id
        /// </summary>
        /// <param name="context"></param>
        private void GetSchoolCategoryParentId(HttpContext context)
        {
            string ParentId = context.Request["ParentId"].ToString();

            //List<SelectListItem> dllCountry = new List<SelectListItem>();
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.School.SchoolCategory>()
            {
                ColumnMember = m => m.ParentId,
                ColumnValue = ParentId
            };
            //dllCountry = service.Invoke().Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }).ToList();
            result = JsonConvert.SerializeObject(service.Invoke());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}