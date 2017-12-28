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
    public class GradeRankingController : ControllerBase
    {
        // GET: SchoolLevel
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(Guid? id)
        {
            var entity = new GradeRanking();
            if (id.HasValue)
            {
                var server = new GetEntityByIdService<GradeRanking>(id.Value);
                entity = server.Invoke();
            }

            return View(entity.CreateViewModel<GradeRanking, VmGradeRanking>());
        }

        #region Ajax
        public ActionResult AjaxList(VmGradeRanking model)
        {
            var service = new GetEntityListService<GradeRanking>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = 999,
                SortMember = new Expression<Func<GradeRanking, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();
            List<VmGradeRanking> list = new List<VmGradeRanking>();
            if (result.TotalCount > 0)
            {
                foreach (var item in result.Results)
                {
                    list.Add(item.CreateViewModel<GradeRanking, VmGradeRanking>());
                }
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AjaxEdit(VmGradeRanking model)
        {
            if (model.DataId == Guid.Empty)
            {
                if (isVilidateName(model.Name, null))
                    return Json(new { success = false, errors = "不能添加已经存在的排名数据" });
                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<GradeRanking>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                else
                {
                    #region 调整英语规划默认成绩 EnglishScoreDefault
                    EnglishScoreDefaultController bllEnglishScoreDefault = new EnglishScoreDefaultController();
                    bllEnglishScoreDefault.CreateDefaultData();
                    #endregion
                    #region 调整学术规划默认成绩 LearnScoreDefault
                    LearnScoreDefaultController bllLearnScoreDefault = new LearnScoreDefaultController();
                    bllLearnScoreDefault.CreateDefaultData();
                    #endregion
                    OutputId = res.Output;
                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            }
            else
            {
                if (isVilidateName(model.Name, null))
                    return Json(new { success = false, errors = "不能修改为已经存在的排名数据" });
                var handler = new BaseModifyHandler<GradeRanking>(model);
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
        /// 验证名称是否存在 true表示存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id">在修改时调用</param>
        /// <returns></returns>
        public bool isVilidateName(string name, Guid? id)
        {
            var service = new CustomSearchWithPaginationService<GradeRanking>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<GradeRanking>>()
                {
                    new CustomConditionPlus<GradeRanking>()
                    {
                        Value = name,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<GradeRanking, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<GradeRanking, object>>[] { x => x.CreatedTime }
            };
            if (id.HasValue)
            {
                 service.CustomConditions.Add(new CustomConditionPlus<GradeRanking>()
                 {
                     Value = id.Value,
                     Operation = SqlOperation.NotEquals,
                     Member = new Expression<Func<GradeRanking, object>>[] { x => x.DataId }
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
        public Guid CreateEdit(VmGradeRanking model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<GradeRanking>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<GradeRanking>(model);
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
            var service = new BaseDeleteService<GradeRanking>(Id);
            service.Invoke();
        }

        public List<VmGradeRanking> GetList()
        {

            var service = new GetEntityListService<GradeRanking>
            {
                PageIndex =1,
                PageSize = 99999,
                SortMember = new Expression<Func<GradeRanking, object>>[] { x => x.CreatedTime }
            };
          
            var result = service.Invoke();
            List<VmGradeRanking> list = new List<VmGradeRanking>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<GradeRanking, VmGradeRanking>());
            };
            return list;
        }
        #endregion

    }
}