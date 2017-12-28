using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Plan;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class PlanningNoteController : ControllerBase
    {
        #region  页面
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "年级列表", Code = "PlanningNotelList", ModuleCode = "SYSTEM", Url = "/PlanningNote/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Details(Guid Id)
        {
            VmPlanningNote model = new VmPlanningNote();
            List<VmPlanningNote> list = PlanningNoteGetList(new VmPlanningNote() { DataId = Id });
            if (list.Count > 0)
            {
                model = list[0];
            }
            return View(model);
        }
        #endregion
        #region AJAX
        [HttpPost]
        public ActionResult AjaxList(VmPlanningNote model)
        {
            List<VmPlanningNote> list = PlanningNoteGetList(model);
            list = list.OrderBy(x => x.SchoolPlace).OrderBy(x => x.SchoolTypeId).OrderBy(x => x.Sort).ToList();
            return Json(new { success = true, total = list.Count, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxEdit(VmPlanningNote model)
        {
            if (model.DataId != Guid.Empty)
            {
                List<VmPlanningNote> listPlanningNote = PlanningNoteGetList(new VmPlanningNote() { DataId = model.DataId });
                if (listPlanningNote.Count>0)
                {
                    VmPlanningNote modelPlanningNote = listPlanningNote[0];
                    modelPlanningNote.Fee = model.Fee;
                    modelPlanningNote.EnglishScore = model.EnglishScore;
                    modelPlanningNote.LearnScore = model.LearnScore;
                    modelPlanningNote.QualityScore = model.QualityScore;
                    modelPlanningNote.AddEnglishScore = model.AddEnglishScore;
                    modelPlanningNote.AddLearnScore = model.AddLearnScore;
                    modelPlanningNote.AddQualityScore = model.AddQualityScore;
                    modelPlanningNote.EnrollmentRate = model.EnrollmentRate;
                    modelPlanningNote.Description = model.Description;
                    Guid rid = PlanningNoteAddEdit(modelPlanningNote);
                    if (rid == Guid.Empty)
                    {
                        return Json(new { success = false, errors = GetErrors() });
                    }
                    else
                    {
                        return Json(new { success = true, Id = rid, errors = GetErrors() });
                    }
                }
            }
            return Json(new { success = false, errors = GetErrors() });
        }

        #endregion
        #region 功能
        public Guid PlanningNoteAddEdit(VmPlanningNote model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                #region 创建
                var handler = new BaseCreateHandler<PlanningNote>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = res.Output;
                #endregion
            }
            else
            {
                #region 修改
                var handler = new BaseModifyHandler<PlanningNote>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = model.DataId;
                #endregion
            }
            return returnId;
        }
        public List<VmPlanningNote> PlanningNoteGetList(VmPlanningNote model)
        {
            List<VmPlanningNote> list = new List<VmPlanningNote>();
            var service = new CustomSearchWithPaginationService<PlanningNote>
            {
                PageIndex = 1,
                PageSize = 9999,
                CustomConditions = new List<CustomCondition<PlanningNote>>
                {
                       new CustomConditionPlus<PlanningNote>
                       {
                           Value = model.Grade??string.Empty ,
                           Operation = SqlOperation.Like,
                            Member   = new Expression<Func<PlanningNote, object>>[] { x => x.Grade }
                       }
                },
                SortMember = new Expression<Func<PlanningNote, object>>[] { x => x.CreatedTime }
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanningNote>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanningNote, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.SchoolPlace))
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanningNote>
                {
                    Value = model.SchoolPlace,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolPlace }
                });
            }
            if (!String.IsNullOrEmpty(model.SchoolType))
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanningNote>
                {
                    Value = model.SchoolType,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolType }
                });
            }
            if (model.SchoolTypeId>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanningNote>
                {
                    Value = model.SchoolTypeId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolTypeId }
                });
            }
            if (model.Sort > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanningNote>
                {
                    Value = model.Sort,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanningNote, object>>[] { x => x.Sort }
                });
            }
            var result = service.Invoke();

            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<PlanningNote, VmPlanningNote>());
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 获取单个年级对象
        /// </summary>
        /// <param name="PlanningNoteId"></param>
        /// <returns></returns>
        public SmPlanningNote GetPlanningNote(Guid PlanningNoteId)
        {
            var entity = new PlanningNote();
            var service = new GetEntityByIdService<PlanningNote>(PlanningNoteId);
            entity = service.Invoke();
            return entity.CreateViewModel<PlanningNote, SmPlanningNote>();
        }

        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Grade">年级名称</param>
            /// <param name="SchoolType">学校类型  1普通学校 2重点学校  3国际学校</param>
            /// <param name="SchoolPlace">学校地点  国外 国内</param>
            /// <returns></returns>
        public List<SmPlanningNote> GetPlanningNoteList(String Grade , String SchoolType, String SchoolPlace)
        {
            List<SmPlanningNote> list = new List<SmPlanningNote>();
            var service = new CustomSearchWithPaginationService<PlanningNote>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<PlanningNote>>
                  {
                      new CustomConditionPlus<PlanningNote>
                      {
                          Value = "",
                          Operation = SqlOperation.Equals,
                          Member = new Expression<Func<PlanningNote, object>>[] { x => x.Grade}
                      }
                  }
            };
         
            if (!String.IsNullOrEmpty(Grade))
            {
                service.CustomConditions.Add(
                    new CustomConditionPlus<PlanningNote>
                    {
                        Value = Grade,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<PlanningNote, object>>[] { x => x.Grade }
                    });
            }
            if (!String.IsNullOrEmpty(SchoolType))
            {
                service.CustomConditions.Add(
                    new CustomConditionPlus<PlanningNote>
                    {
                        Value = SchoolType,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolType }
                    });
            }
            if (!String.IsNullOrEmpty(SchoolPlace))
            {
                service.CustomConditions.Add(
                    new CustomConditionPlus<PlanningNote>
                    {
                        Value = SchoolPlace,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolPlace }
                    });
            }
            var result = service.Invoke();
            return list;
        }
    }
}