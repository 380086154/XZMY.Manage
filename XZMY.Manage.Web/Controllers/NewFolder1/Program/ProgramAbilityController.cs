using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Program;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Program;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Program
{
    /// <summary>
    /// 能力值 学术能力 素质能力
    /// </summary>
    public class ProgramAbilityController : ControllerBase
    {
        #region 页面
        public ActionResult Edit(Guid? Id)
        {
            VmProgramAbility model = new VmProgramAbility();
            if (Id.HasValue)
            {
                model = GetModel(Id.Value);
            }
            return View(model);
        }
       

        public ActionResult List()
        {
            return View();
        }
        #endregion

        #region Ajax
        public ActionResult AjaxList(VmProgramAbility model)
        {
            int TotalCount = 0;
            var list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxEdit(VmProgramAbility model)
        {
            //model.Type =(EProgramType) Request.Params["ProgramAbilityType"].ToInt32(0);
            VmProgramAbility modelOld = new VmProgramAbility();
            if (model.DataId != Guid.Empty)
            {
                modelOld = GetModel(model.DataId);
                modelOld.Description = model.Description;
            }
            else {
                modelOld = model;
            }
            Guid Id = CreateEdit(modelOld);
            if (Id == Guid.Empty)
            {
                return Json(new { success = false, Id = Id, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = Id, errors = GetErrors() });
            }
        }
        public ActionResult AjaxDelete(Guid Id)
        {
            Delete(Id);
            return Json(new { success = true, Id = Id, errors = GetErrors() });
        }
        #endregion
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmProgramAbility GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<ProgramAbility>(Id);
            var entity = service.Invoke() ?? new ProgramAbility();
            return entity.CreateViewModel<ProgramAbility, VmProgramAbility>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmProgramAbility model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<ProgramAbility>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<ProgramAbility>(model);
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
            var service = new BaseDeleteService<ProgramAbility>(Id);
            service.Invoke();
        }

        public List<VmProgramAbility> GetList(VmProgramAbility model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<ProgramAbility>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<ProgramAbility>>
                    {
                        new CustomConditionPlus<ProgramAbility>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<ProgramAbility, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<ProgramAbility, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramAbility>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramAbility, object>>[] { x => x.DataId }
                });
            }
            if ((int)model.Type>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramAbility>
                {
                    Value = model.Type,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramAbility, object>>[] { x => x.Type }
                });
            }
           
            var result = service.Invoke();
            List<VmProgramAbility> list = new List<VmProgramAbility>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProgramAbility, VmProgramAbility>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}