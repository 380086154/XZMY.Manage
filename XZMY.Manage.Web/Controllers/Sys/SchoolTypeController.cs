using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.School;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class SchoolTypeController : ControllerBase
    {
        #region 页面
        [AutoCreateAuthAction(Name = "学校类型列表", Code = "SchoolTypeList", ModuleCode = "SCHOOL", Url = "/SchoolType/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit(Guid? Id)
        {
            VmSchoolType model = new VmSchoolType();
            if (Id.HasValue)
            {
                model = GetModel(Id.Value);
            }
            return View(model);
        }
        #endregion
        #region Ajax
        /// <summary>
        /// Ajax获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmSchoolType model)
        {
            int TotalCount = 0;
            List<VmSchoolType> list = new List<VmSchoolType>();
            list= GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmSchoolType model)
        {
            model.State = (EState)Request.Params["State"].ToInt32(1);
            Guid SchoolTypeId = CreateEdit(model);
            if (SchoolTypeId == Guid.Empty)
            {
                return Json(new { success = false, Id = SchoolTypeId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = SchoolTypeId, errors = GetErrors() });
            }
        }
        #endregion
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmSchoolType GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<SchoolType>(Id);
            var entity = service.Invoke() ?? new SchoolType();
            return entity.CreateViewModel<SchoolType, VmSchoolType>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmSchoolType model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<SchoolType>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<SchoolType>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 物理删除对象
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            var service = new BaseDeleteService<SchoolType>(Id);
            service.Invoke();
        }

        public List<VmSchoolType> GetList(VmSchoolType model,out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<SchoolType>
            {
                PageIndex = model.PageIndex==0?1: model.PageIndex,
                PageSize = model.PageSize==0?99999: model.PageSize,
                CustomConditions = new List<CustomCondition<SchoolType>>
                    {
                        new CustomConditionPlus<SchoolType>
                        {
                            Value = model.State,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<SchoolType, object>>[]
                            {
                                m => m.State
                            },
                        }
                    },
                SortMember = new Expression<Func<SchoolType, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<SchoolType>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<SchoolType, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<SchoolType>
                {
                    Value = model.Name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<SchoolType, object>>[] { x => x.Name }
                });
            }
            var result = service.Invoke();
            List<VmSchoolType> list = new List<VmSchoolType>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<SchoolType, VmSchoolType>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}