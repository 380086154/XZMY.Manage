using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.SiteSetting;
using XZMY.Manage.Service.Handlers.User;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class ScoreItemsController : ControllerBase
    {
        #region 页面
        [AutoCreateAuthAction(Name = "素质属性列表", Code = "ScoreItemsList", ModuleCode = "SYSTEM", Url = "/ScoreItems/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit(Guid? Id)
        {
            VmScoreItemsEdit model = new VmScoreItemsEdit();
            if (Id.HasValue)
            {
                model = ScoreItemsGetModel(Id.Value);
            }
            return View(model);
        }
        #endregion
        #region Ajax
        public ActionResult AjaxEdit(VmScoreItemsEdit model)
        {
            Guid rid = Guid.Empty;
            if (model.DataId != Guid.Empty)
            {
                VmScoreItemsEdit OldModel = ScoreItemsGetModel(model.DataId);
                OldModel.Name = model.Name;
                OldModel.Code = model.Code;
                model.Type = ScoreItemType.素质;
                rid = ScoreItemsAddEdit(OldModel);
            }
            else
            {
                model.Type = ScoreItemType.素质;
                rid = ScoreItemsAddEdit(model);
            }
            if (rid == Guid.Empty)
            {
                return Json(new { success = false, Id = rid, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = rid, errors = GetErrors() });
            }
        }
        public ActionResult AjaxList()
        {
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.SiteSetting.ScoreItems>()
            {
                ColumnMember = m => m.Type,
                ColumnValue = 3
            };
            var result = service.Invoke().OrderBy(x => x.Name).ToList();
            return Json(new { success = true, total = result.Count, rows = result, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        //列表 Ajax 获取数据
        public ActionResult AjaxList3(int Type)
        {
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.SiteSetting.ScoreItems>()
            {
                ColumnMember = m => m.Type,
                ColumnValue = Type
            };
            var result = service.Invoke().OrderBy(x => x.Name);

            return Json(new { success = true, rows = result, errors = GetErrors() }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region 功能
        /// <summary>
        /// 获取分值项目分类列表
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<ScoreItems> GetListScoreItems(int Type)
        {
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.SiteSetting.ScoreItems>()
            {
                ColumnMember = m => m.Type,
                ColumnValue = Type
            };
            return service.Invoke();
        }
        public ActionResult Create(VmScoreItemsEdit model)
        {
            var handler = new BaseCreateHandler<Model.DataModel.SiteSetting.ScoreItems>(model);
            handler.Invoke();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmScoreItemsEdit ScoreItemsGetModel(Guid Id)
        {
            var entity = new ScoreItems();
            var service = new GetEntityByIdService<ScoreItems>(Id);
            entity = service.Invoke();
            return entity.CreateViewModel<ScoreItems, VmScoreItemsEdit>();
        }
        public Guid ScoreItemsAddEdit(VmScoreItemsEdit model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                #region 创建
                var handler = new BaseCreateHandler<ScoreItems>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = res.Output;
                #endregion
            }
            else
            {
                #region 修改
                var handler = new BaseModifyHandler<ScoreItems>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = model.DataId;
                #endregion
            }
            return returnId;
        }
        #endregion
    }
}