using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Web.Controllers.Sys;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.ADONET.SQLServer;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class EnglishScoreDefaultController : ControllerBase
    {
        // GET: SchoolLevel
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(Guid? id)
        {
            var entity = new EnglishScoreDefault();
            if (id.HasValue)
            {
                var server = new GetEntityByIdService<EnglishScoreDefault>(id.Value);
                entity = server.Invoke();
            }
            return View(entity.CreateViewModel<EnglishScoreDefault, VmEnglishScoreDefault>());
        }

        #region Ajax

        public ActionResult AjaxList(VmEnglishScoreDefault model)
        {
            int TotalCount = 0;
            var list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
            
        }


        [HttpPost]
        public ActionResult AjaxEdit(VmEnglishScoreDefault model)
        {

            Guid id  =  IsVilidatePnIdAndGid(model.PlanningNoteId, model.GradeRankingId);
            //model.Id = id;
            if (model.DataId == Guid.Empty)
            {
               
                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<EnglishScoreDefault>(model);
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
                var handler = new BaseModifyHandler<EnglishScoreDefault>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = true, Id = model.DataId, errors = GetErrors() });
            }
        }




        public ActionResult AjaxGradeRanking()
        {
            object mr = new { list = new List<int>() };
            var server = new GetEntityListService<GradeRanking>() { PageIndex=1, PageSize=999 };
            var result = server.Invoke();
            if (result.TotalCount > 0)
            {

                mr = new { list = result.Results };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxPlanningNote()
        {
            object mr = new { list = new List<int>() };
            var service = new CustomSearchWithPaginationService<PlanningNote>
            {
                PageIndex = 1,
                PageSize = 999,
                CustomConditions = new List<CustomCondition<PlanningNote>>()
                {
                    new CustomConditionPlus<PlanningNote>()
                    {
                        Value = 1,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<PlanningNote, object>>[] { x => x.SchoolTypeId }
                    }
                },
                SortMember = new Expression<Func<PlanningNote, object>>[] { x => x.Sort },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };

            var result = service.Invoke();


            if (result.TotalCount > 0)
            {
                mr = new { list = result.Results.Select(m=> new { Id = m.DataId, Sort = m.Sort, Name = m.Grade }).ToList<object>() };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 功能


        public Guid IsVilidatePnIdAndGid(Guid PlanningNoteId, Guid GradeRankingId)
        {
            var service = new CustomSearchWithPaginationService<EnglishScoreDefault>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<EnglishScoreDefault>>()
                {
                    new CustomConditionPlus<EnglishScoreDefault>()
                    {
                        Value = PlanningNoteId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.PlanningNoteId }
                    },
                     new CustomConditionPlus<EnglishScoreDefault>()
                    {
                        Value = GradeRankingId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.GradeRankingId }
                    }
                },
                SortMember = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();
            if (result == null || result.TotalCount <= 0)
                return Guid.Empty;
            return result.Results[0].DataId;
        }

        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmEnglishScoreDefault model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<EnglishScoreDefault>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<EnglishScoreDefault>(model);
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
            var service = new BaseDeleteService<EnglishScoreDefault>(Id);
            service.Invoke();
        }

        public List<VmEnglishScoreDefault> GetList()
        {

            var service = new GetEntityListService<EnglishScoreDefault>
            {
                PageIndex = 1,
                PageSize = 99999,
                SortMember = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();
            List<VmEnglishScoreDefault> list = new List<VmEnglishScoreDefault>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<EnglishScoreDefault, VmEnglishScoreDefault>());
            };
            return list;
        }
        public List<VmEnglishScoreDefault> GetList(VmEnglishScoreDefault model, out int TotalCount)
        {

            var service = new CustomSearchWithPaginationService<EnglishScoreDefault>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<EnglishScoreDefault>>
                    {
                        new CustomConditionPlus<EnglishScoreDefault>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<EnglishScoreDefault, object>>[]
                            {
                                m => m.GradeRankingName
                            },
                        }
                    },
                SortMember = new Expression<Func<EnglishScoreDefault, object>>[] { m => m.CreatedTime },
            };

            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<EnglishScoreDefault>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.DataId }
                });
            }
            if (model.PlanningNoteId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<EnglishScoreDefault>
                {
                    Value = model.PlanningNoteId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.PlanningNoteId }
                });
            }
            if (model.GradeRankingId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<EnglishScoreDefault>
                {
                    Value = model.GradeRankingId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<EnglishScoreDefault, object>>[] { x => x.GradeRankingId }
                });
            }

            var result = service.Invoke();


            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            GradeRankingController bllGradeRanking = new GradeRankingController();
            var listEnglishScoreDefault = GetList();

            var listEnglishScoreDefaultPlanningNote = bllPlanningNote.PlanningNoteGetList(new Model.ViewModel.Plan.VmPlanningNote() { SchoolTypeId = 1 });
            var listEnglishScoreDefaultGradeRanking = bllGradeRanking.GetList();


            List<VmEnglishScoreDefault> list = new List<VmEnglishScoreDefault>();
            foreach (var m in result.Results)
            {
                var TempListPlanningNote = listEnglishScoreDefaultPlanningNote.Where(x => x.DataId == m.PlanningNoteId).ToList();
                if (TempListPlanningNote.Count > 0)
                {
                    m.GradeName = TempListPlanningNote[0].Grade;
                }
                var TempListGradeRanking = listEnglishScoreDefaultGradeRanking.Where(x => x.DataId == m.GradeRankingId).ToList();
                if (TempListGradeRanking.Count > 0)
                {
                    m.GradeRankingName = TempListGradeRanking[0].Name;
                }

                list.Add(m.CreateViewModel<EnglishScoreDefault, VmEnglishScoreDefault>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        /// <summary>
        /// 创建默认数据
        /// </summary>
        public void CreateDefaultData()
        {
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            GradeRankingController bllGradeRanking = new GradeRankingController();
            var listEnglishScoreDefault = GetList();

            var listEnglishScoreDefaultPlanningNote = bllPlanningNote.PlanningNoteGetList(new Model.ViewModel.Plan.VmPlanningNote() { SchoolTypeId = 1 });
            var listEnglishScoreDefaultGradeRanking = bllGradeRanking.GetList();
            foreach (var m in listEnglishScoreDefaultPlanningNote)
            {
                foreach (var n in listEnglishScoreDefaultGradeRanking)
                {
                    int ListCount = 0;
                    var list = GetList(new VmEnglishScoreDefault() { PlanningNoteId = m.DataId, GradeRankingId = n.DataId }, out ListCount);
                    if (ListCount == 0)
                    {
                        VmEnglishScoreDefault modelEnglishScoreDefault = new VmEnglishScoreDefault();
                        modelEnglishScoreDefault.PlanningNoteId = m.DataId;
                        modelEnglishScoreDefault.GradeName = m.Grade;
                        modelEnglishScoreDefault.Sort = m.Sort;
                        modelEnglishScoreDefault.GradeRankingId = n.DataId;
                        modelEnglishScoreDefault.GradeRankingName = n.Name;
                        modelEnglishScoreDefault.EnglishScore = 10 + m.Sort;
                        CreateEdit(modelEnglishScoreDefault);
                    }
                }
            }
        }
        #endregion
    }
}