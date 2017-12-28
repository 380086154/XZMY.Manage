using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms.Design;
using XZMY.Manage.Data.Impl.Query.Assessment;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Assessment;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Assessment;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Assessment
{
    /// <summary>
    /// 评估中心问题
    /// </summary>
    public class AssessmentController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "题库管理", Code = "AssessmentList", ModuleCode = "EVALUATION", Url = "/Assessment/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        [AutoCreateAuthAction(Name = "回收站", Code = "AssessmentRecycledList", ModuleCode = "EVALUATION", Url = "/Assessment/RecycledList", Visible = true, Remark = "")]
        public ActionResult RecycledList()
        {
            return View();
        }
        /// <summary>
        /// 评估成绩单
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "评估成绩单", Code = "AssessmentAchievementList", ModuleCode = "EVALUATION", Url = "/Assessment/AchievementList", Visible = true, Remark = "")]
        public ActionResult AchievementList()
        {
            return View();
        }
        //创建/编辑
        public ActionResult Edit(Guid? id)
        {
            var model = new AssessmentQuestions();
            ViewBag.AnswerCount = 0;
            if (id.HasValue)
            {
                {
                    var service = new GetEntityByIdService<AssessmentQuestions>(id.Value);
                    model = service.Invoke() ?? new AssessmentQuestions();
                }

                GetAnswerAndScore(model.DataId);
            }
            return View(model.CreateViewModel<AssessmentQuestions, VmAssessmentQuestion>());
        }

        //问题列表
        public ActionResult AjaxList(VmAssessmentQuestion model)
        {
            var service = new CustomSearchWithPaginationService<AssessmentQuestions>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<AssessmentQuestions>>
                {
                    new CustomConditionPlus<AssessmentQuestions>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<AssessmentQuestions, object>>[] { x => x.Title,x=>x.Description }
                    },
                    
                     new CustomConditionBase<AssessmentQuestions>
                    {
                        Value = EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = m=>m.State
                    }
                }
            };
            #region AssessmentId
            if (model.AssessmentId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Model.DataModel.Assessment.AssessmentQuestions>
                {
                    Value = model.AssessmentId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Model.DataModel.Assessment.AssessmentQuestions, object>>[] { x => x.AssessmentId }
                });
            }
            #endregion
            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //回收站
        public ActionResult AjaxRecycledList(VmAssessmentQuestion model)
        {
            var service = new CustomSearchWithPaginationService<AssessmentQuestions>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<AssessmentQuestions>>
                {
                    new CustomConditionPlus<AssessmentQuestions>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<AssessmentQuestions, object>>[] { x => x.Title,x=>x.Description }
                    },
                     new CustomConditionBase<AssessmentQuestions>
                    {
                        Value = EState.禁用,
                        Operation = SqlOperation.Equals,
                        Member = m=>m.State
                    }
                },
                SortMember = new Expression<Func<AssessmentQuestions, object>>[] { x => x.DisableTime }
            };
            #region AssessmentId
            if (model.AssessmentId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Model.DataModel.Assessment.AssessmentQuestions>
                {
                    Value = model.AssessmentId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Model.DataModel.Assessment.AssessmentQuestions, object>>[] { x => x.AssessmentId }
                });
            }
            #endregion
            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //分值项目
        public ActionResult AjaxScoreItemList(VmSearchBase model)
        {
            var result = GetScoreItems();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //必须统一调用这个方法，才能保证赋值的正确性
        private PagedResult<ScoreItems> GetScoreItems()
        {
            var service = new CustomSearchWithPaginationService<ScoreItems>
            {
                PageIndex = 1,
                PageSize = 20,//不可能再更多了
                CustomConditions = new List<CustomCondition<ScoreItems>>
                {
                    new CustomConditionPlus<ScoreItems>
                    {
                        Value = ScoreItemType.素质,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ScoreItems, object>>[] { x => x.Type}
                    }
                },
                SortMember = new Expression<Func<ScoreItems, object>>[] { x => x.Name },
                SortType = SortType.Asc
            };

            return service.Invoke();
        }

        //编辑 / 保存
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmAssessmentQuestion model)
        {
            #region 创建

            if (model.DataId == Guid.Empty)
            {
                {//问题保存
                    var handler = new BaseCreateHandler<AssessmentQuestions>(model);
                    var res = handler.Invoke();

                    if (res.Code != 0) return Json(new { success = false, errors = GetErrors() });
                }

                {//答案保存
                    var scoreItemList = GetScoreItems().Results;

                    var count = 0;
                    for (var i = 0; i < model.AnswerCount; i++)
                    {
                        var description = Request.Form["Description" + (i + 1)] ?? "";
                        var scoreValue = Request.Form["txtScoreValue" + (i + 1) + "[]"] ?? "";

                        var scoreValueList = scoreValue.Split(',');
                        var scoreList = new List<VmScore>();
                        for (var j = 0; j < scoreValueList.Length; j++)
                        {
                            var scoreItem = scoreItemList[j];
                            var vmScore = new VmScore
                            {
                                ScoreItemsId = scoreItem.DataId,
                                ScoreItemName = scoreItem.Name,
                                SourceType = "AssessmentAnswer",
                                ScoreValue = scoreValueList[j].ToDecimal(0)
                            };
                            scoreList.Add(vmScore);
                        }

                        var vmAssessmentAnswer = new VmAssessmentAnswer
                        {
                            QuestionsId = model.DataId,
                            Description = description,
                            Score = scoreList
                        };

                        var handler = new AssessmentAnswerCreateHandler(vmAssessmentAnswer);
                        var res = handler.Invoke();

                        if (res.Success) count++;
                    }

                    if (count != model.AnswerCount) return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = true, Id = model.DataId, errors = GetErrors() });
            }

            #endregion

            {
                {//问题保存
                    var handler = new BaseModifyHandler<AssessmentQuestions>(model);
                    var res = handler.Invoke();

                    if (res.Code != 0) return Json(new { success = false, errors = GetErrors() });
                }

                {//答案保存
                    var scoreItemList = GetScoreItems().Results;
                    var answerList = new List<VmAssessmentAnswer>();

                    for (var i = 0; i < model.AnswerCount; i++)
                    {
                        var answerId = (Request.Form["AnswerId" + (i + 1)] ?? "").ToGuid(Guid.NewGuid());
                        var description = Request.Form["Description" + (i + 1)] ?? "";

                        //var scoreId = (Request.Form["ScoreId" + (i + 1)] ?? "").ToGuid(Guid.NewGuid()); 
                        var scoreValue = Request.Form["txtScoreValue" + (i + 1) + "[]"] ?? "";

                        var scoreValueList = scoreValue.Split(',');
                        var scoreList = new List<VmScore>();
                        for (var j = 0; j < scoreValueList.Length; j++)
                        {
                            var scoreItem = scoreItemList[j];
                            var vmScore = new VmScore
                            {
                                SourceId = answerId,
                                ScoreItemsId = scoreItem.DataId,
                                ScoreItemName = scoreItem.Name,
                                SourceType = "AssessmentAnswer",
                                ScoreValue = scoreValueList[j].ToDecimal(0)
                            };
                            scoreList.Add(vmScore);
                        }

                        answerList.Add(new VmAssessmentAnswer
                        {
                            DataId = answerId,
                            QuestionsId = model.DataId,
                            Description = description,
                            Score = scoreList
                        });
                    }

                    var handler = new AssessmentAnswerUltimatesHandler(answerList);
                    var res = handler.Invoke();

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            }
        }

        [HttpPost]
        public ActionResult AjaxEditAnswer(VmAssessmentAnswer model)
        {
            if (ModelState.IsValid)
            {
                if (model.DataId == Guid.Empty)
                {
                    var handler = new AssessmentAnswerCreateHandler(model);
                    var res = handler.Invoke();

                    if (res.Code != 0)
                    {
                        return Json(new { success = false, errors = GetErrors() });
                    }

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
                else
                {
                    var handler = new AssessmentAnswerModifyHandler(model);
                    var res = handler.Invoke();

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            }
            return Json(new { status = false, errors = GetErrors() });
        }

        //删除问题
        [HttpPost]
        public ActionResult AjaxDeleteAnswer(Guid? id)
        {
            var flag = false;
            if (id.HasValue)
            {
                var handler = new AssessmentAnswerDeleteHandler(id.Value);
                var res = handler.Invoke();
                flag = res.Success;
            }
            return Json(new { success = flag, Id = id, errors = GetErrors() });
        }

        //启用问题
        [HttpPost]
        public ActionResult AjaxEnableQuestion(Guid? id)
        {
            var flag = false;
            if (id.HasValue)
            {
                var handler = new AssessmentQuestionEnableHandler(id.Value);
                var res = handler.Invoke();
                flag = res.Success;
            }
            return Json(new { success = flag, Id = id, errors = GetErrors() });
        }

        //批量启用问题
        [HttpPost]
        public ActionResult AjaxBatchEnableQuestion(Guid[] ids)
        {
            var handler = new AssessmentQuestionBatchEnableHandler(ids);
            var res = handler.Invoke();

            return Json(new { success = res.Success, Id = ids, errors = GetErrors() });
        }

        //禁用问题
        [HttpPost]
        public ActionResult AjaxDisableQuestion(Guid? id)
        {
            var flag = false;
            if (id.HasValue)
            {
                var handler = new AssessmentQuestionDisableHandler(id.Value);
                var res = handler.Invoke();
                flag = res.Success;
            }
            return Json(new { success = flag, Id = id, errors = GetErrors() });
        }

        //批量禁用问题
        [HttpPost]
        public ActionResult AjaxBatchDisableQuestion(Guid[] ids)
        {

            var handler = new AssessmentQuestionBatchDisableHandler(ids);
            var res = handler.Invoke();

            return Json(new { success = res.Success, Id = ids, errors = GetErrors() });
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new AssessmentQuestions();

            if (id.HasValue)
            {
                {//问题
                    var service = new GetEntityByIdService<AssessmentQuestions>(id.Value);
                    entity = service.Invoke() ?? new AssessmentQuestions();
                }

                GetAnswerAndScore(entity.DataId);
            }

            return View(entity.CreateViewModel<AssessmentQuestions, VmAssessmentQuestion>());
        }

        #region Public Method

        /// <summary>
        /// 获取答案与分数信息
        /// </summary>
        /// <param name="id"></param>
        public void GetAnswerAndScore(Guid id)
        {
            var idList = new List<Guid>();
            {//答案
                var service = new CustomSearchService<AssessmentAnswers>
                {
                    CustomConditions = new List<CustomCondition<AssessmentAnswers>>
                        {
                            new CustomConditionPlus<AssessmentAnswers>
                            {
                                Value = id,
                                Operation = SqlOperation.Like,
                                Member = new Expression<Func<AssessmentAnswers, object>>[] { x => x.QuestionsId },
                            }
                        },
                };

                var result = service.Invoke();
                idList.AddRange(result.Select(x => x.DataId));
                ViewBag.AnswerList = result.OrderBy(x => x.CreatedTime).ToList();
                ViewBag.AnswerCount = result.Count;
            }

            {//分数
                var service = new SearchSourceIdList
                {
                    IdList = idList
                };

                ViewBag.AnswerSourceList = service.Invoke();
            }
        }

        #endregion
    }
}