using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    /// <summary>
    /// 规划访客记录
    /// </summary>
    public class PlanVisitorController : ControllerBase
    {
        #region 页面展示
        /// <summary>
        /// 规划访客记录列表展示页面
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "规划访客记录列表", Code = "PlanVisitorList", ModuleCode = "PLANNER", Url = "/PlanVisitor/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        #endregion
        #region Ajax 方法
        /// <summary>
        /// 规划访客记录列表查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new CustomSearchWithPaginationService<PlanVisitor>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<PlanVisitor>>
                  {
                      new CustomConditionPlus<PlanVisitor>
                      {
                          Value = model.Keyword??String.Empty,
                          Operation = SqlOperation.Like,
                          Member = new Expression<Func<PlanVisitor, object>>[] { x => x.Name}
                      }
                  },
                SortMember = new Expression<Func<PlanVisitor, object>>[] { x => x.CreatedTime }
            };
            var result = service.Invoke();
            List<VmPlanVisitor> list = new List<VmPlanVisitor>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<PlanVisitor, VmPlanVisitor>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 功能操作
        /// <summary>
        /// 新增修改规划访客记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid AddEdit(VmPlanVisitor model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<PlanVisitor>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = model.DataId;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<PlanVisitor>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        #endregion
    }
}