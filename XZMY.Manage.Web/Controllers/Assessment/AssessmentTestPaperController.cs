using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Assessment;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Assessment
{
    public class AssessmentTestPaperController : ControllerBase
    {
        #region 页面
        [AutoCreateAuthAction(Name = "题库试卷", Code = "AssessmentTestPaperList", ModuleCode = "EVALUATION", Url = "/AssessmentTestPaper/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit(Guid? Id)
        {
            VmAssessment model = new VmAssessment();
            if (Id.HasValue)
            {
                model = GetModel(Id.Value);
            }
            return View(model);
        }
        #endregion
        #region AjaxList
        /// <summary>
        /// Ajax获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmAssessment model)
        {
            int TotalCount = 0;
            List<VmAssessment> list = new List<VmAssessment>();
            list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmAssessment model)
        {
             model.State = (EState)Request.Params["State"].ToInt32(1);
            Guid ReturnId = CreateEdit(model);
            if (ReturnId == Guid.Empty)
            {
                return Json(new { success = false, Id = ReturnId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = ReturnId, errors = GetErrors() });
            }
        }
        #endregion

        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmAssessment GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<Model.DataModel.Assessment.Assessment>(Id);
            var entity = service.Invoke() ?? new Model.DataModel.Assessment.Assessment();
            return entity.CreateViewModel<Model.DataModel.Assessment.Assessment, VmAssessment>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmAssessment model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Assessment.Assessment>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Assessment.Assessment>(model);
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
            var service = new BaseDeleteService<Model.DataModel.Assessment.Assessment>(Id);
            service.Invoke();
        }

        public List<VmAssessment> GetList(VmAssessment model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<Model.DataModel.Assessment.Assessment>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<Model.DataModel.Assessment.Assessment>>
                    {
                        new CustomConditionPlus<Model.DataModel.Assessment.Assessment>
                        {
                            Value = model.State,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<Model.DataModel.Assessment.Assessment, object>>[]
                            {
                                m => m.State
                            },
                        }
                    },
                SortMember = new Expression<Func<Model.DataModel.Assessment.Assessment, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Model.DataModel.Assessment.Assessment>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Model.DataModel.Assessment.Assessment, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<Model.DataModel.Assessment.Assessment>
                {
                    Value = model.Name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<Model.DataModel.Assessment.Assessment, object>>[] { x => x.Name }
                });
            }
            if (!String.IsNullOrEmpty(model.Code))
            {
                service.CustomConditions.Add(new CustomConditionPlus<Model.DataModel.Assessment.Assessment>
                {
                    Value = model.Code,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<Model.DataModel.Assessment.Assessment, object>>[] { x => x.Code }
                });
            }
            var result = service.Invoke();
            List<VmAssessment> list = new List<VmAssessment>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<Model.DataModel.Assessment.Assessment, VmAssessment>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}