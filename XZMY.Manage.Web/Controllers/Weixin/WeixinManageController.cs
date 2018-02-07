using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel.Sys;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Service.Sys;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers.Sys;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Service.Weixin.Manage;
using XZMY.Manage.Service.Weixin.Tools;
using XZMY.Manage.Model.ViewModel.WeixinManage;
using XZMY.Manage.Service.Weixin;
using System.Web;

namespace XZMY.Manage.Web.Controllers.Weixin
{
    /// <summary>
    /// 微信设置
    /// </summary>
    public class WeixinManageController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "微信设置", Code = "WeixinIndex", ModuleCode = "WeixinIndex", Url = "/Weixin/Index", Visible = true, Remark = "")]
        public ActionResult Index()
        {
            var model = new VmWeixinManageIndex();
            model.AccessToken = AccessTokenService.GetAccessToken();
            model.AccessTokenExpired = AccessTokenService.GetAccessTokenExpired().ToString("yyyy-MM-dd HH:mm:ss");
            model.AutoResponseContent = new AutoResponseService().GetValue();
            return View(model);
        }

        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEditAutoResponse(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return Json(new { success = false, errors = "不能为空" });

            var service = new AutoResponseService();
            service.SingleItem.Value = content.Trim().Replace("&#xA;", "\\r\\n");
            service.SaveOrUpdate();

            return Json(new { success = true, errors = GetErrors() });
        }

        //实时预览
        [HttpPost]
        public ActionResult AjaxPreview(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return Json(new { success = false, errors = "不能为空" });

            var result = WeixinXml.CreateTextMessage("oYVeUwOSNj7wCFrvZMPbW8SBA-Y8", "gh_c83167b279b7", content);
            Response.Write(result);
            Response.Flush();

            return Json(new { success = true, errors = GetErrors() });
        }
    }
}
