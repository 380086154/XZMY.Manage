using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class SchoolLevelController : ControllerBase
    {
        // GET: SchoolLevel
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(Guid? id)
        {
            var entity = new SchoolLevel();
            if (id.HasValue)
            {
                var server = new GetEntityByIdService<SchoolLevel>(id.Value);
                entity = server.Invoke();
            }

            return View(entity.CreateViewModel<SchoolLevel, VmSchoolLevel>());
        }

        #region Ajax
        public ActionResult AjaxList(VmSchoolLevel model)
        {
            var service = new CustomSearchWithPaginationService<SchoolLevel>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = 999,
                CustomConditions = new List<CustomCondition<SchoolLevel>>()
                {
                    new CustomConditionPlus<SchoolLevel>()
                    {
                        Value = model.State,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<SchoolLevel, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<SchoolLevel, object>>[] { x => x.CreatedTime }
            };
            
            var result = service.Invoke();
            List<VmSchoolLevel> list = new List<VmSchoolLevel>();
            if (result.TotalCount > 0)
            {
                foreach (var item in result.Results)
                {
                    list.Add(item.CreateViewModel<SchoolLevel, VmSchoolLevel>());
                }
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxUpdateSLState(Guid id, int state)
        {
            var service = new GetEntityByIdService<SchoolLevel>(id);
            var entity = service.Invoke();
            if (entity != null && entity.DataId != Guid.Empty)
            {
                entity.State = (EState)state;
                var model = entity.CreateViewModel<SchoolLevel, VmSchoolLevel>();
                var handler = new BaseModifyHandler<SchoolLevel>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    return Json(new { success = true, Id = entity.DataId, errors = GetErrors() });
                }
            }
            return Json(new { success = false, errors = GetErrors() });
        }

        [HttpPost]
        public ActionResult AjaxEdit(VmSchoolLevel model)
        {
            if (model.DataId == Guid.Empty)
            {
                if(isVilidateName(model.Name, null))
                    return Json(new { success = false, errors = "不能添加已经存在的学校类型名称" });
                model.State = EState.启用;
                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<SchoolLevel>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                else
                {
                    OutputId = res.Output;
                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            }
            else
            {
                 if(isVilidateName(model.Name, null))
                    return Json(new { success = false, errors = "不能修改为已经存在的学校类型名称" });
                var handler = new BaseModifyHandler<SchoolLevel>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = true, Id = model.DataId, errors = GetErrors() });
            }
        }

        #endregion

        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmSchoolLevel GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<SchoolLevel>(Id);
            var entity = service.Invoke() ?? new SchoolLevel();
            return entity.CreateViewModel<SchoolLevel, VmSchoolLevel>();
        }
        /// <summary>
        /// 验证名称是否存在 true表示存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id">在修改时调用</param>
        /// <returns></returns>
        public bool isVilidateName(string name, Guid? id)
        {
            var service = new CustomSearchWithPaginationService<SchoolLevel>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<SchoolLevel>>()
                {
                    new CustomConditionPlus<SchoolLevel>()
                    {
                        Value = name,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<SchoolLevel, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<SchoolLevel, object>>[] { x => x.CreatedTime }
            };
            if (id.HasValue)
            {
                service.CustomConditions.Add(new CustomConditionPlus<SchoolLevel>()
                {
                    Value = id.Value,
                    Operation = SqlOperation.NotEquals,
                    Member = new Expression<Func<SchoolLevel, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            return result.TotalCount > 0;
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmSchoolLevel model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<SchoolLevel>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<SchoolLevel>(model);
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
            var service = new BaseDeleteService<SchoolLevel>(Id);
            service.Invoke();
        }

        public List<VmSchoolLevel> GetList()
        {

            var service = new GetEntityListService<SchoolLevel>
            {
                PageIndex = 1,
                PageSize = 99999,
                SortMember = new Expression<Func<SchoolLevel, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();
            List<VmSchoolLevel> list = new List<VmSchoolLevel>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<SchoolLevel, VmSchoolLevel>());
            };
            return list;
        }
        #endregion

    }
}