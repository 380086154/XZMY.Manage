using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    /// <summary>
    /// 留学预算
    /// </summary>
    public class StudyBudgetController : ControllerBase
    {
        #region 页面
        public ActionResult Edit(Guid? Id)
        {
            VmStudyBudget model = new VmStudyBudget();
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
        public ActionResult AjaxList(VmStudyBudget model)
        {
            int TotalCount = 0;
            var list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxEdit(VmStudyBudget model)
        {
            model.State = (EState)Request.Params["State"].ToInt32(0);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
            Guid Id = CreateEdit(model);
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
        public VmStudyBudget GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<StudyBudget>(Id);
            var entity = service.Invoke() ?? new StudyBudget();
            return entity.CreateViewModel<StudyBudget, VmStudyBudget>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmStudyBudget model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<StudyBudget>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<StudyBudget>(model);
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
            var service = new BaseDeleteService<StudyBudget>(Id);
            service.Invoke();
        }

        public List<VmStudyBudget> GetList(VmStudyBudget model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudyBudget>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudyBudget>>
                    {
                        new CustomConditionPlus<StudyBudget>
                        {
                            Value = model.Keywords??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<StudyBudget, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<StudyBudget, object>>[] { m => m.CreatedTime },
            };
            if ((int)model.State>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudyBudget>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudyBudget, object>>[] { x => x.State }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudyBudget>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudyBudget, object>>[] { x => x.DataId }
                });
            }
           
            var result = service.Invoke();
            List<VmStudyBudget> list = new List<VmStudyBudget>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudyBudget, VmStudyBudget>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}