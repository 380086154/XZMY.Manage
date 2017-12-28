using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Web.Controllers.Project;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Order
{
    public class InComeController : ControllerBase
    {
        #region 页面
        /// <summary>
        /// 支付页面编辑和创建
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid? id)
        {
            VmInCome model = new VmInCome();
            if (id.HasValue)
            {
                model = GetModelInCome(id.Value);
            }
            return View(model);
        }
        #endregion

        #region Ajax
        public ActionResult AjaxEdit(VmInCome model)
        {
            model.PayType=(EOrderPayType)Request.Params["PayType"].ToInt32(1);
            model.PayMode = Request.Params["PayMode"].ToGuid(Guid.Empty);
            model.PayModeName= DataDictionaryManager.GetDataById("PayMode", model.PayMode).Name;
            model.PayTime = DateTime.Now;
            Guid Id = EditInCome(model);
            if (Id != Guid.Empty)
            {
                if (model.Type == 1)
                {
                    OrderProjectController bllOrderProject = new OrderProjectController();
                    var modelProject = bllOrderProject.GetModel(model.OrderId);
                    modelProject.PayPrice += model.PayPrice;
                    if (modelProject.PayPrice >= modelProject.TotalPrice)
                    {
                        modelProject.IsPayCompletion = EOrderIsPayCompletion.是;
                    }
                    else
                    {
                        modelProject.IsPayCompletion = EOrderIsPayCompletion.否;
                    }
                    bllOrderProject.CreateEdit(modelProject);
                }
                else
                {
                    OrderCourseController bllOrderCourse = new OrderCourseController();
                    var modelCourse = bllOrderCourse.GetModel(model.OrderId);
                    modelCourse.PayPrice += model.PayPrice;
                    if (modelCourse.PayPrice >= modelCourse.TotalPrice)
                    {
                        modelCourse.IsPayCompletion =1;
                    }
                    else
                    {
                        modelCourse.IsPayCompletion = 2;
                    }
                    bllOrderCourse.CreateEdit(modelCourse);
                }
                return Json(new { success = true, Id = Id, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = false, Id = Id, errors = GetErrors() }); 
            }
        }
        public ActionResult AjaxList(VmInCome model)
        {
            var list = GetListInCome(model);
            
            return Json(new { success = true, total = list.Count, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 功能
        /// <summary>
        /// 添加或修改支付数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid EditInCome(VmInCome model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<InCome>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = res.Output;
            }
            else
            {
                var handler = new BaseModifyHandler<InCome>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = model.DataId;
            }
            return returnId;
        }
        /// <summary>
        /// 通过ID获取支付数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmInCome GetModelInCome(Guid Id)
        {
            var service = new GetEntityByIdService<InCome>(Id);
            var entity = service.Invoke();
            return entity.CreateViewModel<InCome, VmInCome>();
        }

        /// <summary>
        /// 获取支付列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<VmInCome> GetListInCome(VmInCome model)
        {
            List<VmInCome> list = new List<VmInCome>();
            var service = new CustomSearchWithPaginationService<InCome>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<InCome>>()
                {
                    new CustomConditionPlus<InCome>()
                    {
                        Value = model.Keyword??String.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<InCome, object>>[] { x => x.SerialNumber}
                    }
                }
            };
            if (model.OrderId!=Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<InCome>
                {
                    Value = model.OrderId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<InCome, object>>[] { x => x.OrderId }
                });
            }
            var result = service.Invoke();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<InCome, VmInCome>());
            }
            return list;
        }
        #endregion
    }
}