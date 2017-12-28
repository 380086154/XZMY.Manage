using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.ViewModel;

using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Model.ViewModel.Question;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Question
{
    /// <summary>
    /// 问题管理
    /// </summary>
    public class QuestionController : ControllerBase
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        [AutoCreateAuthAction(Name = "问题管理", Code = "QuestionList", ModuleCode = "PLANNER", Url = "/Question/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        //[AutoCreateAuthAction(Name = "问答管理", Code = "QuestionAnswer", ModuleCode = "PLANNER", Url = "/Question/Answer", Visible = true)]
        public ActionResult Answer(Guid? id)
        {
            var entity = new ProblemPlanner();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<ProblemPlanner>(id.Value);
                entity = service.Invoke();
            }
            else
            {
                RedirectToAction("List");
            }

            return View(entity.CreateViewModel<ProblemPlanner, VmProblemPlannerEdit>());
        }

        [HttpPost]
        public ActionResult Answer(VmProblemPlannerEdit model)
        {
            if (ModelState.IsValid)
            {
                model.AnswerTime = DateTime.Now;
                var handler = new BaseModifyHandler<ProblemPlanner>(model);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });

            }
            return Json(new { status = false, errors = GetErrors() });
        }

        public ActionResult AjaxList(VmProblemPlannerEdit model)
        {
            var service = new CustomSearchWithPaginationService<ProblemPlanner>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<ProblemPlanner>>
                {
                    new CustomConditionBase<ProblemPlanner>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = x => x.MemberName
                    }
                },
                SortMember = new Expression<Func<ProblemPlanner, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
    }
}