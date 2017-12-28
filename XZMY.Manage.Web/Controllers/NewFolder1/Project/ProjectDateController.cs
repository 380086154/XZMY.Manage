using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Project;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel;
using T2M.Common.DataServiceComponents.Data.Query;
namespace XZMY.Manage.Web.Controllers.Project
{
    public class ProjectDateController : ControllerBase
    {
        //列表 Ajax 获取数据
        public ActionResult AjaxList(Guid ProjectId)
        {
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.Project.ProjectDate>()
            {
                ColumnMember = m => m.ProjectId,
                ColumnValue = ProjectId
            };
            var result = service.Invoke();

            return Json(new { success = true, rows = result, errors = GetErrors() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AjaxEdit(VmProjectDate model)
        {
            //if (ModelState.IsValid)
            //{
                if (model.DataId == Guid.Empty)
                {
                    var handler = new BaseCreateHandler<ProjectDate>(model);
                    var res = handler.Invoke();
                    if (res.Code != 0)
                    {
                        return Json(new { success = false, errors = GetErrors() });
                    }

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
                else
                {
                    var handler = new BaseModifyHandler<ProjectDate>(model);
                    var res = handler.Invoke();
                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }


        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmProjectDate GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<ProjectDate>(Id);
            var entity = service.Invoke() ?? new ProjectDate();
            return entity.CreateViewModel<ProjectDate, VmProjectDate>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmProjectDate model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<ProjectDate>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<ProjectDate>(model);
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
            var service = new BaseDeleteService<ProjectDate>(Id);
            service.Invoke();
        }

        public List<VmProjectDate> GetList(VmProjectDate model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<ProjectDate>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<ProjectDate>>
                    {
                        new CustomConditionPlus<ProjectDate>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<ProjectDate, object>>[]
                            {
                                m => m.DepartureCity
                            },
                        }
                    },
                SortMember = new Expression<Func<ProjectDate, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProjectDate>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProjectDate, object>>[] { x => x.DataId }
                });
            }
            if (model.ProjectId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProjectDate>
                {
                    Value = model.ProjectId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProjectDate, object>>[] { x => x.ProjectId }
                });
            }


            var result = service.Invoke();
            List<VmProjectDate> list = new List<VmProjectDate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProjectDate, VmProjectDate>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}