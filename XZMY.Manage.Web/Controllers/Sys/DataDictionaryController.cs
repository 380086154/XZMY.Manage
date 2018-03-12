using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Program;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Model.ViewModel.Sys;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Sys;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Service.Weixin;
using XZMY.Manage.Web.Controllers.Program;
using XZMY.Manage.Web.Controllers.SiteSetting;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class DataDictionaryController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "系统设置", Code = "DataDictionaryList", ModuleCode = "SYSTEM", Url = "/DataDictionary/Index", Visible = true)]
        public ActionResult Index()
        {
            var model = new VmDataDictionaryIndex();

            //数据备份收发邮件管理
            var backupEmailManageService = new BackupEmailManageService();
            var arr = backupEmailManageService.GetValue().Split('|');
            model.FromEmail = arr[0] ?? string.Empty;
            model.ToEmail = arr.Length > 1 ? arr[1] : string.Empty;

            //分店数据库
            var branchService = new BranchService();
            model.BranchList = branchService.GetAll();

            return View(model);
        }

        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 数据字典分类
        /// </summary>
        /// <returns></returns>
        public ActionResult CatagoriesList()
        {
            return View();
        }
        /// <summary>
        /// 数据字典页面
        /// </summary>
        /// <param name="CatagoryKey"></param>
        /// <returns></returns>
        public ActionResult DataDictionaryItemList(String CatagoryKey)
        {

            return View();
        }

        #region Ajax method

        public ActionResult AjaxSaveEmailList(VmDataDictionaryIndex model)
        {
            var fromEmail = model.FromEmail.Trim();
            var toEmail = model.ToEmail.Trim();

            var service = new BackupEmailManageService();
            service.SingleItem.Value = fromEmail + "|" + toEmail;
            service.SaveOrUpdate();

            return Json(new { success = true, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// AJAX 获取数据字典
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CatagoryKey">数据字典类型KEY</param>
        /// <returns></returns>
        public ActionResult AjaxDataDictionaryItemList(VmSearchBase model, String CatagoryKey, Int32? State)//VmDataDictionaryList
        {
            int iState = 1;
            if (State.HasValue)
            {
                iState = State.Value;
            }
            List<DataDictionaryItem> listItem = new List<DataDictionaryItem>();
            var listCatagory = DataDictionaryManager.GetCatagories(m => m.Key == CatagoryKey);
            Dictionary<string, List<DataDictionaryItem>> list = new Dictionary<string, List<DataDictionaryItem>>();
            foreach (var file in listCatagory)
            {
                var dict = file.Value.Select(x => x.Value).ToList().Where(x => x.State == iState).ToList();
                list.Add(file.Key, dict);
                listItem = dict;
            }
            return Json(new { success = true, total = listItem.Count, rows = listItem, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加或编辑数据字典 页面
        /// </summary>
        /// <param name="CatagoryKey"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DataDictionaryItemEdit(String CatagoryKey, Guid? Id)
        {
            DataDictionaryItem model = new DataDictionaryItem();
            if (Id.HasValue)
            {
                model = DataDictionaryManager.GetDataById(CatagoryKey, Id.Value);
            }
            else
            {
                //model.Id = Guid.NewGuid();
            }
            return View(model);
        }

        public ActionResult AjaxDataDictionaryItem(DataDictionaryItem model)
        {
            String CatagoryKey = "";
            int State = 1;
            if (Request.Params["CatagoryKey"] != null)
            {
                CatagoryKey = Request.Params["CatagoryKey"].ToString();
            }
            if (Request.Params["State"] != null)
            {
                int.TryParse(Request.Params["State"].ToString(), out State);
            }
            model.IsDefault = true;
            model.IsSystem = true;
            model.State = State;
            model.Sort = 0;
            bool b = DataDictionaryManager.SaveOrUpdateData(CatagoryKey, model);

            return Json(new { success = b, Id = model.DataId, errors = GetErrors() });
        }

        #endregion

        public ActionResult DefaultDataPage()
        {
            DefaultData();
            return Redirect("/DataDictionary/Index/");
        }

        /// <summary>
        /// 恢复默认数据 初始化数据
        /// </summary>
        public void DefaultData()
        {
            return;
        }

    }
}