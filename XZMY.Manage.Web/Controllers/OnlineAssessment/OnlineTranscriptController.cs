using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.OnlineAssessment;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.OnlineAssessment;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.OnlineAssessment
{
    public class OnlineTranscriptController : ControllerBase
    {

        #region 页面
        /// <summary>
        /// 答题卡数据编辑页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AutoCreateAuthAction(Name = "编辑答题卡", Code = "OnlineTranscriptEdit", ModuleCode = "OnlineAssessment", Url = "/OnlineTranscript/Edit", Visible = true)]
        public ActionResult Edit(Guid? id)
        {
            var entity = new OnlineAssessmentTranscript();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<OnlineAssessmentTranscript>(id.Value);
                entity = service.Invoke();
            }
            var model = entity.CreateViewModel<OnlineAssessmentTranscript, VmOnlineAssessmentTranscript>();
            model.Password = "";
            return View(model);
        }

        /// <summary>
        /// 答题卡列表
        /// </summary>
        /// <returns></returns>
        //[AutoCreateAuthAction(Name = "答题卡列表", Code = "OnlineTranscriptList", ModuleCode = "OnlineAssessment", Url = "/OnlineTranscript/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">答题卡ID</param>
        /// <param name="index">当前查询第index题</param>
        /// <returns></returns>
        //[AutoCreateAuthAction(Name = "答案查看列表", Code = "OnlineTranscriptAnswerList", ModuleCode = "OnlineAssessment", Url = "/OnlineTranscript/AnswerList", Visible = true)]
        public ActionResult AnswerList(Guid id, int index)
        {
            index = index <= 0 ? 1 : index;
            var entity = new OnlineAssessmentTranscript();
            var securityEntity = new OnlineAssessmentSecurity();

            var model = new VmOnlineAssessmentQuestions();

            ViewBag.TranscriptId = id.ToString();
            ViewBag.QuestionId = "";
            ViewBag.IndexPage = index;
            ViewBag.SecurityName = "";//试卷名称
            ViewBag.StudentName = "";//答题学生
            ViewBag.UsedTime = "";//总共耗时
            ViewBag.SeTime = "";//答题时间
            ViewBag.QuestionAllCount = 0;
            ViewBag.QuestionGroupList = new List<VmOnlineAssessmentAnswersGroup>();
            ViewBag.AnswerList = new List<VmOnlineAssessmentAnswers>();
            if (id != Guid.Empty)
            {
                //获取答题卡对象
                var service = new GetEntityByIdService<OnlineAssessmentTranscript>(id);
                entity = service.Invoke();
                var vmEntity = entity.CreateViewModel<OnlineAssessmentTranscript, VmOnlineAssessmentTranscript>();
                ViewBag.StudentName = vmEntity.Name + " (账号:" + vmEntity.LoginName + ")";
                ViewBag.UsedTime = vmEntity.UsedTime;
                ViewBag.SeTime = vmEntity.BeginTimeStr + " 至 " + vmEntity.EndTimeStr;

                //查询试卷对象
                var securityService = new GetEntityByIdService<OnlineAssessmentSecurity>(vmEntity.OnlineAssessmentSecurityId);
                securityEntity = securityService.Invoke();
                ViewBag.SecurityName = securityEntity.Name + " " + securityEntity.Code;

                //查询问题
                int rCount = 0;
                model = GetQuestionsList(index, vmEntity.OnlineAssessmentSecurityId, out rCount);
                ViewBag.QuestionAllCount = rCount;
                if (model.DataId != Guid.Empty)
                {
                    ViewBag.QuestionId = model.DataId;
                    if (model.Type == EOnlineAssessmentQuestionType.分组单选题)
                    {
                        ViewBag.QuestionGroupList = GetQuestionsGroupList(model.DataId);
                    }
                    ViewBag.AnswerList = GetQuestionsAnswerList(model.DataId);
                }
            }
            return View(model);
        }
        #endregion

        #region Ajax
        public ActionResult AjaxList(VmOnlineAssessmentTranscript model)
        {
            int TotalCount = 0;
            List<VmOnlineAssessmentTranscript> list = GetList(model.PageIndex, model.State, model.Keywords, "", "", out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() });
        }

        [HttpPost]
        public ActionResult AjaxEdit(VmOnlineAssessmentTranscript model)
        {
            object result = new { success = false, message = "", errors = GetErrors() };
            if (model.DataId == Guid.Empty)
            {
                if(!hasStudentLoginName(model.LoginName))
                {
                    model.BeginTime = DateTime.MinValue;
                    model.EndTime = DateTime.MinValue;
                    model.State = ETranscriptState.未开始;
                    model.Password = string.IsNullOrEmpty(model.Password) ? "123456".ToMd5() : model.Password.ToMd5();
                    var handler = new BaseCreateHandler<OnlineAssessmentTranscript>(model);
                    var res = handler.Invoke();
                    if (res.Code == 0)
                    {
                        // returnId = res.Output;
                        result = new { success = true, id = res.Output, message = "", errors = GetErrors() };
                    }
                }
                else
                    result = new { success = false,  message = "该账号已存在", errors = GetErrors() };
            }
            else
            {
                if (!hasStudentLoginName(model.DataId, model.LoginName))
                {
                    var server = new GetEntityByIdService<OnlineAssessmentTranscript>(model.DataId);
                    var entity = server.Invoke();
                    if (entity.State == ETranscriptState.未开始)
                    {
                        entity.Name = model.Name;
                        entity.LoginName = model.LoginName;
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            entity.Password = model.Password.ToMd5();
                        }
                        entity.OnlineAssessmentSecurityId = model.OnlineAssessmentSecurityId;
                        var handler = new BaseModifyHandler<OnlineAssessmentTranscript>(entity.CreateViewModel<OnlineAssessmentTranscript, VmOnlineAssessmentTranscript>());
                        var res = handler.Invoke();
                        if (res.Code == 0)
                        {
                            result = new { success = true, id = entity.DataId, message = "", errors = GetErrors() };
                        }
                    }
                    else
                        result = new { success = false, message = "该答题卡正处于答题状态或已经提交，因此不能修改", errors = GetErrors() };
                }
                else
                    result = new { success = false, message = "该账号已存在", errors = GetErrors() };


            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult AjaxLoadUserAnswer(Guid transcriptId, Guid questionId)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswerSheet>
            {
                PageIndex = 1,
                PageSize = 999,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswerSheet>>
                {
                    new CustomConditionPlus<OnlineAssessmentAnswerSheet>
                    {
                        Value = transcriptId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[] { x => x.OnlineAssessmentTranscriptId }
                    },
                    new CustomConditionPlus<OnlineAssessmentAnswerSheet>
                    {
                        Value = questionId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[] { x => x.OnlineAssessmentQuestionsId }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[] { m => m.CreatedTime },
            };
            var result = service.Invoke();
            List<VmOnlineAssessmentAnswerSheet> list = new List<VmOnlineAssessmentAnswerSheet>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentAnswerSheet, VmOnlineAssessmentAnswerSheet>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 功能
        /// <summary>
        /// 判断登录名是否存在 (存在即为 true)
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        [NonAction]
        public bool hasStudentLoginName(string loginName)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentTranscript>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<OnlineAssessmentTranscript>>
                {
                    new CustomConditionPlus<OnlineAssessmentTranscript>
                    {
                        Value = loginName,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.LoginName }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentTranscript, object>>[] { m => m.CreatedTime },
            };
            var result = service.Invoke();
            return result.TotalCount > 0 ? true : false;
        }
        /// <summary>
        /// 修改编辑时:判断登录名是否存在 (存在即为 true)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        [NonAction]
        public bool hasStudentLoginName(Guid id, string loginName)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentTranscript>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<OnlineAssessmentTranscript>>
                {
                    new CustomConditionPlus<OnlineAssessmentTranscript>
                    {
                        Value = loginName,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.LoginName }
                    },
                    new CustomConditionPlus<OnlineAssessmentTranscript>
                    {
                        Value = id,
                        Operation = SqlOperation.NotEquals,
                        Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.DataId }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentTranscript, object>>[] { m => m.CreatedTime },
            };
            var result = service.Invoke();
            return result.TotalCount > 0 ? true : false;
        }

        /// <summary>
        /// 学生答题卡查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="state">答题卡状态</param>
        /// <param name="keyWords">根据学生名或登录名查询</param>
        /// <param name="startTime">创建时间段 下限</param>
        /// <param name="endTime">创建时间段的 上限</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        [NonAction]
        public List<VmOnlineAssessmentTranscript> GetList(int pageIndex, ETranscriptState state, string keyWords, string startTime, string endTime, out int TotalCount)
        {
            TotalCount = 0;

            var service = new CustomSearchWithPaginationService<OnlineAssessmentTranscript>
            {
                PageIndex = pageIndex <= 0 ? 1 : pageIndex,
                PageSize = 20,
                CustomConditions = new List<CustomCondition<OnlineAssessmentTranscript>>
                {
                    new CustomConditionPlus<OnlineAssessmentTranscript>
                    {
                        Value = state,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentTranscript, object>>[] { m => m.CreatedTime },
            };
            if (!string.IsNullOrEmpty(keyWords))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentTranscript>
                {
                    Value = keyWords,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.Name }
                });
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentTranscript>
                {
                    Value = DateTime.Parse(startTime),
                    Operation = SqlOperation.LesserOrEquals,
                    Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.CreatedTime }
                });
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentTranscript>
                {
                    Value = DateTime.Parse(endTime),
                    Operation = SqlOperation.GreaterOrEquals,
                    Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.CreatedTime }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentTranscript> list = new List<VmOnlineAssessmentTranscript>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentTranscript, VmOnlineAssessmentTranscript>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        /// <summary>
        /// 根据试卷ID查询问题集合
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="securityId">试卷Id</param>
        /// <param name="rCount"></param>
        /// <returns></returns>
        public VmOnlineAssessmentQuestions GetQuestionsList(int pageIndex, Guid securityId, out int rCount)
        {
            rCount = 0;
            var service = new CustomSearchWithPaginationService<OnlineAssessmentQuestions>
            {
                PageIndex = pageIndex,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<OnlineAssessmentQuestions>>
                {
                    new CustomConditionPlus<OnlineAssessmentQuestions>
                    {
                        Value = securityId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentQuestions, object>>[] { x => x.OnlineAssessmentSecurityId }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentQuestions, object>>[] { m => m.Sort },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };
    
            var result = service.Invoke();
            rCount = result.TotalCount;
            if (result.Results != null && result.Results.Count>0)
            {
                return result.Results[0].CreateViewModel<OnlineAssessmentQuestions, VmOnlineAssessmentQuestions>();
            }
            return new VmOnlineAssessmentQuestions();
        }

        public List<VmOnlineAssessmentAnswersGroup> GetQuestionsGroupList(Guid questionId)
        {
            List<VmOnlineAssessmentAnswersGroup> list = new List<VmOnlineAssessmentAnswersGroup>();
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswersGroup>
            {
                PageIndex = 1,
                PageSize = 999,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswersGroup>>
                {
                    new CustomConditionPlus<OnlineAssessmentAnswersGroup>
                    {
                        Value = questionId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[] { x => x.OnlineAssessmentQuestionsId }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[] { m => m.CreatedTime },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };
            var result = service.Invoke();
            if (result.Results != null && result.Results.Count > 0)
            {
                foreach (var item in result.Results)
                {
                    list.Add(item.CreateViewModel<OnlineAssessmentAnswersGroup, VmOnlineAssessmentAnswersGroup>());
                }
            }
            return list;
        }
        public List<VmOnlineAssessmentAnswers> GetQuestionsAnswerList(Guid questionId)
        {
            List<VmOnlineAssessmentAnswers> list = new List<VmOnlineAssessmentAnswers>();
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswers>
            {
                PageIndex = 1,
                PageSize = 999,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswers>>
                {
                    new CustomConditionPlus<OnlineAssessmentAnswers>
                    {
                        Value = questionId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OnlineAssessmentAnswers, object>>[] { x => x.OnlineAssessmentQuestionsId }
                    }
                },
                SortMember = new Expression<Func<OnlineAssessmentAnswers, object>>[] { m => m.CreatedTime },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };
            var result = service.Invoke();
            if (result.Results != null && result.Results.Count > 0)
            {
                foreach (var item in result.Results)
                {
                    list.Add(item.CreateViewModel<OnlineAssessmentAnswers, VmOnlineAssessmentAnswers>());
                }
            }
            return list;
        }
        #endregion

    }
    }