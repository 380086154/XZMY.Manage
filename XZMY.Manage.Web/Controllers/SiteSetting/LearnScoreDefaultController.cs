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
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class LearnScoreDefaultController : ControllerBase
    {
        // GET: SchoolLevel
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(Guid? id)
        {
            var entity = new LearnScoreDefault();
            if (id.HasValue)
            {
                var server = new GetEntityByIdService<LearnScoreDefault>(id.Value);
                entity = server.Invoke();
            }
            return View(entity.CreateViewModel<LearnScoreDefault, VmLearnScoreDefault>());
        }

        #region Ajax

        public ActionResult AjaxList(VmLearnScoreDefault model)
        {
            int TotalCount = 0;
            var list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AjaxEdit(VmLearnScoreDefault model)
        {

            Guid id = IsVilidateSLvIdAndGid(model.SchoolLevelId, model.GradeRankingId);
            //model.Id = id;
            if (model.DataId == Guid.Empty)
            {

                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<LearnScoreDefault>(model);
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
                var handler = new BaseModifyHandler<LearnScoreDefault>(model);
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
            var server = new GetEntityListService<GradeRanking>() { PageIndex = 1, PageSize = 999 };
            var result = server.Invoke();
            if (result.TotalCount > 0)
            {

                mr = new { list = result.Results };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxSchoolLevel()
        {
            object mr = new { list = new List<int>() };
            var service = new GetEntityListService<SchoolLevel>() {
                PageIndex = 1, PageSize = 999,
                SortMember = new Expression<Func<SchoolLevel, object>>[] { x => x.CreatedTime },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };

            var result = service.Invoke();


            if (result.TotalCount > 0)
            {
                mr = new { list = result.Results.Select(m => new { Id = m.DataId, Name = m.Name }).ToList<object>() };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 功能


        public Guid IsVilidateSLvIdAndGid(Guid SchoolLevelId, Guid GradeRankingId)
        {
            var service = new CustomSearchWithPaginationService<LearnScoreDefault>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<LearnScoreDefault>>()
                {
                    new CustomConditionPlus<LearnScoreDefault>()
                    {
                        Value = SchoolLevelId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<LearnScoreDefault, object>>[] { x => x.SchoolLevelId }
                    },
                     new CustomConditionPlus<LearnScoreDefault>()
                    {
                        Value = GradeRankingId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<LearnScoreDefault, object>>[] { x => x.GradeRankingId }
                    }
                },
                SortMember = new Expression<Func<LearnScoreDefault, object>>[] { x => x.CreatedTime }
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
        public Guid CreateEdit(VmLearnScoreDefault model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<LearnScoreDefault>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<LearnScoreDefault>(model);
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
            var service = new BaseDeleteService<LearnScoreDefault>(Id);
            service.Invoke();
        }

        public List<VmLearnScoreDefault> GetList()
        {

            var service = new GetEntityListService<LearnScoreDefault>
            {
                PageIndex = 1,
                PageSize = 99999,
                SortMember = new Expression<Func<LearnScoreDefault, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();
            List<VmLearnScoreDefault> list = new List<VmLearnScoreDefault>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<LearnScoreDefault, VmLearnScoreDefault>());
            };
            return list;
        }
        public List<VmLearnScoreDefault> GetList(VmLearnScoreDefault model, out int TotalCount)
        {

            var service = new CustomSearchWithPaginationService<LearnScoreDefault>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<LearnScoreDefault>>
                    {
                        new CustomConditionPlus<LearnScoreDefault>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<LearnScoreDefault, object>>[]
                            {
                                m => m.SchoolLevelName
                            },
                        }
                    },
                SortMember = new Expression<Func<LearnScoreDefault, object>>[] { m => m.CreatedTime },
            };
            
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<LearnScoreDefault>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<LearnScoreDefault, object>>[] { x => x.DataId }
                });
            }
            if (model.SchoolLevelId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<LearnScoreDefault>
                {
                    Value = model.SchoolLevelId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<LearnScoreDefault, object>>[] { x => x.SchoolLevelId }
                });
            }
            if (model.GradeRankingId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<LearnScoreDefault>
                {
                    Value = model.GradeRankingId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<LearnScoreDefault, object>>[] { x => x.GradeRankingId }
                });
            }

            var result = service.Invoke();
            List<VmLearnScoreDefault> list = new List<VmLearnScoreDefault>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<LearnScoreDefault, VmLearnScoreDefault>());
            }
            TotalCount = result.TotalCount;
            return list;
        }



        /// <summary>
        /// 创建默认数据
        /// </summary>
        public void CreateDefaultData()
        {
            SchoolLevelController bllSchoolLevel = new SchoolLevelController();
            GradeRankingController bllGradeRanking = new GradeRankingController();
            var listLearnScoreDefault = GetList();

            var listLearnScoreDefaultSchoolLevel = bllSchoolLevel.GetList();
            var listLearnScoreDefaultGradeRanking = bllGradeRanking.GetList();
            foreach (var m in listLearnScoreDefaultSchoolLevel)
            {
                foreach (var n in listLearnScoreDefaultGradeRanking)
                {
                    int ListCount = 0;
                    var list = GetList(new VmLearnScoreDefault() { SchoolLevelId = m.DataId, GradeRankingId = n.DataId }, out ListCount);
                    if (ListCount == 0)
                    {
                        VmLearnScoreDefault modelLearnScoreDefault = new VmLearnScoreDefault();
                        modelLearnScoreDefault.SchoolLevelId = m.DataId;
                        modelLearnScoreDefault.SchoolLevelName = m.Name;
                        modelLearnScoreDefault.GradeRankingId = n.DataId;
                        modelLearnScoreDefault.GradeRankingName = n.Name;
                        modelLearnScoreDefault.LearnScore = 20;
                        CreateEdit(modelLearnScoreDefault);
                    }
                }
            }
        }
        #endregion
    }
}