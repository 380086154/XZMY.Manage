using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Project;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Attributes;
using T2M.Common.DataServiceComponents.Data.Query;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Web.Controllers.SiteSetting;
using XZMY.Manage.Model.ViewModel.SiteSetting;

namespace XZMY.Manage.Web.Controllers.Project
{
    /// <summary>
    /// 活动模版
    /// </summary>
    public class ProjectTemplateController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "活动模版列表", Code = "ProjectTemplateList", ModuleCode = "PROJECT", Url = "/ProjectTemplate/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 创建编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AutoCreateAuthAction(Name = "创建活动模版", Code = "ProjectTemplateEdit", ModuleCode = "PROJECT", Url = "/ProjectTemplate/Edit", Visible = true)]
        public ActionResult Edit(Guid? id)
        {
            var entity = new ProjectTemplate();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<ProjectTemplate>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<ProjectTemplate, VmProjectTemplate>());
        }

        //用户 创建/编辑 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmProjectTemplate model)
        {
            //if (ModelState.IsValid)
            //{
            bool bSuccess = false;
            Guid ReturnId = Guid.Empty;
            model.FitGrade = string.Format(",{0},", Request.Params["dllFitGrade[]"]);
            model.SuitablePerson = model.FitGrade;
            model.ScoreItemNames = Request.Form["ckScoreItemNames[]"];
            model.State = (EState)Request.Params["State"].ToInt32(1);
            model.DifficultyValue = Request.Params["dllDifficultyValue"].ToInt32(5);
            model.CompletionValue = 10 - model.DifficultyValue;
            model.FeeValue = Request.Params["dllFeeValue"].ToInt32(5);
            model.Service = Request.Form["chkService[]"];
            model.QualityScore = Request.Params["QualityScore"].ToDecimal(0M);

            if (Request.Params["dllCountry"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectTemplatePlaceLocationId = Request.Params["dllCountry"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllProvince"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectTemplatePlaceLocationId = Request.Params["dllProvince"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllCity"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectTemplatePlaceLocationId = Request.Params["dllCity"].ToGuid(Guid.Empty);
            }

            model.ProjectTemplatePlaceName = model.ProjectTemplatePlaceName.Replace(",--请选择--", "").Replace(",请选择", "");
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Project.ProjectTemplate>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                else {
                    bSuccess = true;
                    ReturnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Project.ProjectTemplate>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    bSuccess = false;
                    ReturnId = Guid.Empty;
                }
                else
                {
                    bSuccess = true;
                    ReturnId = model.DataId;
                }
            }
            #region 分值
            ScoresController bllScores = new ScoresController();
            String strScoreItemValueList = Request.Params["ScoreItemValueList"].ToString();
            var listItem = strScoreItemValueList.Split("|");
            foreach (var Item in listItem)
            {
                if (!String.IsNullOrEmpty(Item))
                {
                    var ItemValue = Item.Split(",");
                    VmScoresEdit modelScores = new VmScoresEdit();
                    modelScores.DataId = ItemValue[0].ToGuid(Guid.Empty);
                    modelScores.ScoreItemsId = ItemValue[2].ToGuid(Guid.Empty);
                    modelScores.ScoreItemsName = ItemValue[3];
                    modelScores.SourceId = ReturnId;
                    modelScores.SourceType = "ProjectTemplate";
                    modelScores.ScoreValue = ItemValue[1].ToDecimal(0M);
                    bllScores.CreateEdit(modelScores);
                }
            }
            #endregion


            return Json(new { success = bSuccess, Id = ReturnId, errors = GetErrors() });
        }
        public ActionResult AjaxList(VmProjectTemplate model)
        {
            string keyword = "";
            var service = new CustomSearchWithPaginationService<ProjectTemplate>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<ProjectTemplate>>
                {
                    new CustomConditionPlus<ProjectTemplate>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.Name,x=>x.Code }
                    }
                },
                SortMember = new Expression<Func<ProjectTemplate, object>>[] { x => x.CreatedTime }
            };
            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProjectTemplate>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.State }
                });
            }
            var result = service.Invoke();
            List<VmProjectTemplate> list = new List<VmProjectTemplate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProjectTemplate, VmProjectTemplate>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取活动模板信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmProjectTemplate GetVmProjectTemplate(Guid Id)
        {
            var service = new GetEntityByIdService<ProjectTemplate>(Id);
            var entity = service.Invoke();
            return entity.CreateViewModel<ProjectTemplate, VmProjectTemplate>();
        }
        /// <summary>
        /// 通过模板ID获取活动对象
        /// </summary>
        /// <param name="CourseTemplateId">活动模板ID</param>
        /// <returns></returns>
        public VmProjectEdit GetProject(Guid ProjectTemplateId)
        {
            var service = new CustomSearchWithPaginationService<Model.DataModel.Project.Project>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<Model.DataModel.Project.Project>>
                {
                    new CustomConditionPlus<Model.DataModel.Project.Project>
                    {
                        Value = ProjectTemplateId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Model.DataModel.Project.Project, object>>[] { x => x.ProjectTemplateId }
                    },
                    new CustomConditionPlus<Model.DataModel.Project.Project>
                    {
                        Value = Model.Enum.EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Model.DataModel.Project.Project, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<Model.DataModel.Project.Project, object>>[] { x => x.CreatedTime }
            };
            var result = service.Invoke();
            VmProjectEdit model = null;
            if (result.TotalCount > 0)
            {
                model = result.Results[0].CreateViewModel<Model.DataModel.Project.Project, Model.ViewModel.Project.VmProjectEdit>();
            }
            return model;
        }

        public List<VmProjectTemplate> GetListVmProjectTemplate(String TemplateName,String ItemName,String SuitablePerson)
        {
           


            var service = new CustomSearchWithPaginationService<ProjectTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<ProjectTemplate>>
                {
                    new CustomConditionPlus<ProjectTemplate>
                    {
                        Value = TemplateName,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<ProjectTemplate, object>>[] { x => x.CreatedTime }
            };
            #region ItemName
            //service.CustomConditions.AddRange(
            //new List<CustomCondition<ProjectTemplate>>
            //    {
            //    new CustomConditionPlus<ProjectTemplate>
            //    {
            //        Value = ItemName,
            //        Operation = SqlOperation.Like,
            //        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.ScoreItemNames}
            //    }
            //    });
            #endregion
            #region ItemName
            if (!String.IsNullOrEmpty(ItemName))
            {
                //service.CustomConditions.AddRange(
                //new List<CustomCondition<ProjectTemplate>>
                //    {
                //    new CustomConditionPlus<ProjectTemplate>
                //    {
                //        Value = ItemName,
                //        Operation = SqlOperation.Like,
                //        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.ScoreItemNames}
                //    }
                //    });
            }
            #endregion
            
            var result = service.Invoke();
            List<VmProjectTemplate> list = new List<VmProjectTemplate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProjectTemplate, VmProjectTemplate>());
            }
            return list;
        }
        /// <summary>
        /// 获取活动模板页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<VmProjectTemplate> GetList(VmProjectTemplate model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<ProjectTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<ProjectTemplate>>
                {
                    new CustomConditionPlus<ProjectTemplate>
                    {
                        Value = model.Name??String.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<ProjectTemplate, object>>[] { x => x.CreatedTime }
            };
            #region State
            if ((int)model.State > 0)
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<ProjectTemplate>>
                    {
                    new CustomConditionPlus<ProjectTemplate>
                    {
                        Value = model.State,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.State }
                    }
                    });
            }
            #endregion
            #region ItemName
            if (!String.IsNullOrEmpty(model.ScoreItemNames))
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<ProjectTemplate>>
                    {
                    new CustomConditionPlus<ProjectTemplate>
                    {
                        Value = model.ScoreItemNames,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.ScoreItemNames }
                    }
                    });
            }
            #endregion
            #region SuitablePerson
            if (!String.IsNullOrEmpty(model.SuitablePerson))
            {
                var sSorts = model.SuitablePerson.Split(",");
                foreach (var mSort in sSorts)
                {
                    if (mSort.ToInt32(0) != 0)
                    {
                        service.CustomConditions.AddRange(
                        new List<CustomCondition<ProjectTemplate>>
                        {
                            new CustomConditionPlus<ProjectTemplate>
                            {
                                Value =string.Format(",{0},",mSort.ToInt32(0)),
                                Operation = SqlOperation.Like,
                                Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.SuitablePerson }
                            }
                        });
                    }
                }
            }
            #endregion
            var result = service.Invoke();
            TotalCount = result.TotalCount;
            List<VmProjectTemplate> list = new List<VmProjectTemplate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProjectTemplate, VmProjectTemplate>());
            }
            return list;
        }
    }
}