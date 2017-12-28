using System.Web;
using Newtonsoft.Json;
using T2M.Common.DataServiceComponents.Service;
using System;

namespace XZMY.Manage.Web.Content.Custom
{
    /// <summary>
    /// Location 的摘要说明
    /// </summary>
    public class Location : IHttpHandler
    {
        public string result = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.Params["action"] ?? "";

            switch (type)
            {
                case "GetLocationParentId":
                    GetLocationParentId(context);
                    break;
                case "GetLocationLevel":
                    GetLocationLevel(context);
                    break;
                default:
                    //Uploader(context);
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }

        //
        private void GetLocationLevel(HttpContext context)
        {
            var level = context.Request.Params["Level"].ToInt32(1);

            //List<SelectListItem> dllCountry = new List<SelectListItem>();
            var service = new GetEntityBySingleColumnService<Model.DataModel.Location.Location>()
            {
                ColumnMember = m => m.Level,
                ColumnValue = level
            };
            //dllCountry = service.Invoke().Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }).ToList();
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        /// <summary>
        /// 地区上级Id查询数据
        /// </summary>
        /// <param name="context"></param>
        private void GetLocationParentId(HttpContext context)
        {
            string parentId = context.Request.Params["ParentId"] ?? "";
            Guid parentIdGuid= Guid.Empty;
            if (parentId.ToGuid(Guid.Empty) == Guid.Empty)
            {
                parentIdGuid = Guid.Empty;
            }
            else {
                parentIdGuid = parentId.ToGuid(Guid.Empty);
            }
             // parentId = (parentId == "" || parentId == "请选择" || parentId == "--请选择--") ? Guid.Empty.ToString() : "";
            //List<SelectListItem> dllCountry = new List<SelectListItem>();
            var service = new GetEntityBySingleColumnService<Model.DataModel.Location.Location>
            {
                ColumnMember = m => m.ParentId,
                ColumnValue = parentIdGuid
            };
            //dllCountry = service.Invoke().Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }).ToList();
            result = JsonConvert.SerializeObject(service.Invoke());
        }

        public bool IsReusable { get; } = false;
    }
}