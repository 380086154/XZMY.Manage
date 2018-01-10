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
    
    public class OnlineAssessmentController : ControllerBase
    {
        #region 页面
        #region 学生
        /// <summary>
        /// 学生列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentList()
        {
            return View();
        }
        public ActionResult StudentEdit(Guid? Id)
        {
            VmOnlineAssessmentStudent model = new VmOnlineAssessmentStudent();
            if (Id.HasValue)
            {
                model = GetModelOnlineAssessmentStudent(Id.Value);
            }
            return View(model);
        }
        public ActionResult StudentAjaxList(VmOnlineAssessmentStudent model)
        {
            int TotalCount = 0;
            List<VmOnlineAssessmentStudent> list = new List<VmOnlineAssessmentStudent>();
            list = GetListOnlineAssessmentStudent(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StudentAjaxEdit(VmOnlineAssessmentStudent model)
        {
            if (model.DataId != Guid.Empty)
            {
                var oldMoel = GetModelOnlineAssessmentStudent(model.DataId);
                if (String.IsNullOrEmpty(model.Password))
                {
                    model.Password = oldMoel.Password;
                }
            }
            model.State = (EState)Request.Params["State"].ToInt32(1);
            Guid returnId = CreateEditOnlineAssessmentStudent(model);
            if (returnId == Guid.Empty)
            {
                return Json(new { success = false, Id = returnId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = returnId, errors = GetErrors() });
            }
        }
        public ActionResult StudentAjaxIsExist(Guid? id, string loginName)
        {
            bool flag = false;
            int count = 0;
            var list = GetListOnlineAssessmentStudent(new VmOnlineAssessmentStudent() { LoginName = loginName }, out count);
            foreach (var m in list)
            {
                if (m.LoginName == loginName && id.Value != m.DataId)
                {
                    flag = true;
                }
            }
            return Json(flag ? "已存在" : "true", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Security
        //[AutoCreateAuthAction(Name = "试卷列表", Code = "OnlineAssessmentSecurityList", ModuleCode = "OnlineAssessment", Url = "/OnlineAssessment/SecurityList", Visible = true)]
        public ActionResult SecurityList()
        {
            return View();
        }
        public ActionResult SecurityEdit(Guid? Id)
        {
            VmOnlineAssessmentSecurity model = new VmOnlineAssessmentSecurity();
            if (Id.HasValue)
            {
                model = GetModelOnlineAssessmentSecurity(Id.Value);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SecurityAjaxEdit(VmOnlineAssessmentSecurity model)
        {
            model.State = (EState)Request.Params["State"].ToInt32(1);
            Guid returnId = CreateEditOnlineAssessmentSecurity(model);
            if (returnId == Guid.Empty)
            {
                return Json(new { success = false, Id = returnId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = returnId, errors = GetErrors() });
            }
        }
        public ActionResult SecurityAjaxList(VmOnlineAssessmentSecurity model)
        {
            int TotalCount = 0;
            List<VmOnlineAssessmentSecurity> list = new List<VmOnlineAssessmentSecurity>();
            list = GetListOnlineAssessmentSecurity(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Questions
        public ActionResult QuestionsList()
        {
            return View();
        }
        public ActionResult QuestionsAjaxList(VmOnlineAssessmentQuestions model)
        {
            int TotalCount = 0;
            List<VmOnlineAssessmentQuestions> list = new List<VmOnlineAssessmentQuestions>();
            list = GetListOnlineAssessmentQuestions(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QuestionsEdit(Guid? Id)
        {
            VmOnlineAssessmentQuestions model = new VmOnlineAssessmentQuestions();
            if (Id.HasValue)
            {
                model = GetModelOnlineAssessmentQuestions(Id.Value);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuestionsAjaxEdit(VmOnlineAssessmentQuestions model)
        {
            var oldModel = GetModelOnlineAssessmentQuestions(model.DataId);
            model.Type = (EOnlineAssessmentQuestionType)Request.Params["Type"].ToInt32(1);
            model.State = (EState)Request.Params["State"].ToInt32(1);
            Guid returnId = CreateEditOnlineAssessmentQuestions(model);


            #region 分组答案
            int RowIndexGroup = Request.Params["indexGroup"].ToInt32(0);
            for (var i = 1; i <= RowIndexGroup; i++)
            {
                if (Request.Params[string.Format("GroupId{0}", i)] != null)
                {
                    VmOnlineAssessmentAnswersGroup modelAnswersGroup = new VmOnlineAssessmentAnswersGroup();
                    modelAnswersGroup.OnlineAssessmentQuestionsId = returnId;
                    modelAnswersGroup.DataId = Request.Params[string.Format("GroupId{0}", i)].ToGuid(Guid.Empty);
                    modelAnswersGroup.GroupName = Request.Params[string.Format("GroupName{0}", i)].ToString();
                    CreateEditOnlineAssessmentAnswersGroup(modelAnswersGroup);
                }
            }
            #endregion
            #region 添加答案
            int RowIndex = Request.Params["index"].ToInt32(0);
            for (var i = 1; i <= RowIndex; i++)
            {
                VmOnlineAssessmentAnswers modelAnswers = new VmOnlineAssessmentAnswers();
                if (Request.Params[string.Format("OnlineAssessmentAnswersId{0}", i)] != null)
                {
                    modelAnswers.DataId = Request.Params[string.Format("OnlineAssessmentAnswersId{0}", i)].ToGuid(Guid.Empty);
                    modelAnswers.OnlineAssessmentQuestionsId = returnId;
                    modelAnswers.Description = Request.Params[string.Format("AnswersDescription{0}", i)].ToString();
                    modelAnswers.Picture = Request.Params[string.Format("AnswersPicture{0}", i)].ToString();
                    modelAnswers.State = EState.启用;
                    CreateEditOnlineAssessmentAnswers(modelAnswers);
                }
            }
            #endregion

            if (returnId == Guid.Empty)
            {
                return Json(new { success = false, Id = returnId, errors = GetErrors() });
            }
            else
            {
                #region 题目类型改变删除答案
                if (oldModel.DataId != Guid.Empty)
                {
                    if (oldModel.Type != model.Type)
                    {
                        int t = 0;
                        var listAnswers = GetListOnlineAssessmentAnswers(new VmOnlineAssessmentAnswers() { OnlineAssessmentQuestionsId = model.DataId }, out t);
                        foreach (var m in listAnswers)
                        {
                            DeleteOnlineAssessmentAnswers(m.DataId);
                        }
                    }
                }
                #endregion
                return Json(new { success = true, Id = returnId, errors = GetErrors() });
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuestionsAnswerEdit(Guid? Id)
        {
            VmOnlineAssessmentQuestions model = new VmOnlineAssessmentQuestions();
            if (Id.HasValue)
            {
                model = GetModelOnlineAssessmentQuestions(Id.Value);
            }
            return View(model);
        }
        public ActionResult QuestionsAjaxDelete(Guid Id)
        {
            var model = GetModelOnlineAssessmentQuestions(Id);
            DeleteOnlineAssessmentQuestions(Id);
            return Redirect(string.Format("/OnlineAssessment/QuestionsEdit/?SecurityId={0}", model.OnlineAssessmentSecurityId));
        }
        public ActionResult AnswerAjaxDelete(Guid Id)
        {
            var model = GetModelOnlineAssessmentAnswers(Id);
            DeleteOnlineAssessmentAnswers(Id);
            return Redirect(string.Format("/OnlineAssessment/QuestionsEdit/{0}", model.OnlineAssessmentQuestionsId));
        }
        public ActionResult AnswerGroupAjaxDelete(Guid Id)
        {
            var model =GetModelOnlineAssessmentAnswersGroup(Id);
            DeleteOnlineAssessmentAnswersGroup(Id);
            return Redirect(string.Format("/OnlineAssessment/QuestionsEdit/{0}", model.OnlineAssessmentQuestionsId));
        }
        #endregion

        #endregion
        #region 功能
        #region OnlineAssessmentTranscript
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentTranscript GetModelOnlineAssessmentTranscript(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentTranscript>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentTranscript();
            return entity.CreateViewModel<OnlineAssessmentTranscript, VmOnlineAssessmentTranscript>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentTranscript(VmOnlineAssessmentTranscript model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentTranscript>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentTranscript>(model);
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
        public void DeleteOnlineAssessmentTranscript(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentTranscript>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentTranscript> GetListOnlineAssessmentTranscript(VmOnlineAssessmentTranscript model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentTranscript>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentTranscript>>
                    {
                        new CustomConditionPlus<OnlineAssessmentTranscript>
                        {
                            Value = model.DataId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<OnlineAssessmentTranscript, object>>[]
                            {
                                m => m.DataId
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentTranscript, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentTranscript>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentTranscript, object>>[] { x => x.DataId }
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
        #endregion
        #region OnlineAssessmentStudent
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentStudent GetModelOnlineAssessmentStudent(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentStudent>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentStudent();
            return entity.CreateViewModel<OnlineAssessmentStudent, VmOnlineAssessmentStudent>();
        }
       
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentStudent(VmOnlineAssessmentStudent model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentStudent>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentStudent>(model);
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
        public void DeleteOnlineAssessmentStudent(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentStudent>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentStudent> GetListOnlineAssessmentStudent(VmOnlineAssessmentStudent model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentStudent>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentStudent>>
                    {
                        new CustomConditionPlus<OnlineAssessmentStudent>
                        {
                            Value = model.State,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<OnlineAssessmentStudent, object>>[]
                            {
                                m => m.State
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentStudent, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentStudent>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentStudent, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.LoginName))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentStudent>
                {
                    Value = model.LoginName,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<OnlineAssessmentStudent, object>>[] { x => x.LoginName }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentStudent> list = new List<VmOnlineAssessmentStudent>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentStudent, VmOnlineAssessmentStudent>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region OnlineAssessmentSecurity
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentSecurity GetModelOnlineAssessmentSecurity(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentSecurity>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentSecurity();
            return entity.CreateViewModel<OnlineAssessmentSecurity, VmOnlineAssessmentSecurity>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentSecurity(VmOnlineAssessmentSecurity model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentSecurity>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentSecurity>(model);
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
        public void DeleteOnlineAssessmentSecurity(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentSecurity>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentSecurity> GetListOnlineAssessmentSecurity(VmOnlineAssessmentSecurity model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentSecurity>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentSecurity>>
                    {
                        new CustomConditionPlus<OnlineAssessmentSecurity>
                        {
                            Value = model.State,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<OnlineAssessmentSecurity, object>>[]
                            {
                                m => m.State
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentSecurity, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentSecurity>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentSecurity, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentSecurity> list = new List<VmOnlineAssessmentSecurity>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentSecurity, VmOnlineAssessmentSecurity>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region OnlineAssessmentQuestions
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentQuestions GetModelOnlineAssessmentQuestions(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentQuestions>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentQuestions();
            return entity.CreateViewModel<OnlineAssessmentQuestions, VmOnlineAssessmentQuestions>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentQuestions(VmOnlineAssessmentQuestions model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentQuestions>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentQuestions>(model);
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
        public void DeleteOnlineAssessmentQuestions(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentQuestions>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentQuestions> GetListOnlineAssessmentQuestions(VmOnlineAssessmentQuestions model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentQuestions>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentQuestions>>
                    {
                        new CustomConditionPlus<OnlineAssessmentQuestions>
                        {
                            Value = model.Title??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<OnlineAssessmentQuestions, object>>[]
                            {
                                m => m.Title
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentQuestions, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentQuestions>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentQuestions, object>>[] { x => x.DataId }
                });
            }
            if (model.OnlineAssessmentSecurityId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentQuestions>
                {
                    Value = model.OnlineAssessmentSecurityId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentQuestions, object>>[] { x => x.OnlineAssessmentSecurityId }
                });
            }
            if ((int)model.State>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentQuestions>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentQuestions, object>>[] { x => x.State }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentQuestions> list = new List<VmOnlineAssessmentQuestions>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentQuestions, VmOnlineAssessmentQuestions>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region OnlineAssessmentAnswerSheet
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentAnswerSheet GetModelOnlineAssessmentAnswerSheet(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentAnswerSheet>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentAnswerSheet();
            return entity.CreateViewModel<OnlineAssessmentAnswerSheet, VmOnlineAssessmentAnswerSheet>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentAnswerSheet(VmOnlineAssessmentAnswerSheet model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentAnswerSheet>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentAnswerSheet>(model);
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
        public void DeleteOnlineAssessmentAnswerSheet(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentAnswerSheet>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentAnswerSheet> GetListOnlineAssessmentAnswerSheet(VmOnlineAssessmentAnswerSheet model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswerSheet>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswerSheet>>
                    {
                        new CustomConditionPlus<OnlineAssessmentAnswerSheet>
                        {
                            Value = model.DataId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[]
                            {
                                m => m.DataId
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentAnswerSheet>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentAnswerSheet, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentAnswerSheet> list = new List<VmOnlineAssessmentAnswerSheet>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentAnswerSheet, VmOnlineAssessmentAnswerSheet>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region OnlineAssessmentAnswersGroup
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentAnswersGroup GetModelOnlineAssessmentAnswersGroup(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentAnswersGroup>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentAnswersGroup();
            return entity.CreateViewModel<OnlineAssessmentAnswersGroup, VmOnlineAssessmentAnswersGroup>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentAnswersGroup(VmOnlineAssessmentAnswersGroup model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentAnswersGroup>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentAnswersGroup>(model);
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
        public void DeleteOnlineAssessmentAnswersGroup(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentAnswersGroup>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentAnswersGroup> GetListOnlineAssessmentAnswersGroup(VmOnlineAssessmentAnswersGroup model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswersGroup>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswersGroup>>
                    {
                        new CustomConditionPlus<OnlineAssessmentAnswersGroup>
                        {
                            Value = model.GroupName??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[]
                            {
                                m => m.GroupName
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[] { m => m.CreatedTime },
            };
            if (model.OnlineAssessmentQuestionsId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentAnswersGroup>
                {
                    Value = model.OnlineAssessmentQuestionsId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[] { x => x.OnlineAssessmentQuestionsId }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentAnswersGroup>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentAnswersGroup, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentAnswersGroup> list = new List<VmOnlineAssessmentAnswersGroup>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentAnswersGroup, VmOnlineAssessmentAnswersGroup>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region OnlineAssessmentAnswers
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOnlineAssessmentAnswers GetModelOnlineAssessmentAnswers(Guid Id)
        {
            var service = new GetEntityByIdService<OnlineAssessmentAnswers>(Id);
            var entity = service.Invoke() ?? new OnlineAssessmentAnswers();
            return entity.CreateViewModel<OnlineAssessmentAnswers, VmOnlineAssessmentAnswers>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditOnlineAssessmentAnswers(VmOnlineAssessmentAnswers model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OnlineAssessmentAnswers>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OnlineAssessmentAnswers>(model);
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
        public void DeleteOnlineAssessmentAnswers(Guid Id)
        {
            var service = new BaseDeleteService<OnlineAssessmentAnswers>(Id);
            service.Invoke();
        }

        public List<VmOnlineAssessmentAnswers> GetListOnlineAssessmentAnswers(VmOnlineAssessmentAnswers model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OnlineAssessmentAnswers>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OnlineAssessmentAnswers>>
                    {
                        new CustomConditionPlus<OnlineAssessmentAnswers>
                        {
                            Value = model.Description??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<OnlineAssessmentAnswers, object>>[]
                            {
                                m => m.Description
                            },
                        }
                    },
                SortMember = new Expression<Func<OnlineAssessmentAnswers, object>>[] { m => m.CreatedTime },
            };
            if (model.OnlineAssessmentQuestionsId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentAnswers>
                {
                    Value = model.OnlineAssessmentQuestionsId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentAnswers, object>>[] { x => x.OnlineAssessmentQuestionsId }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OnlineAssessmentAnswers>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OnlineAssessmentAnswers, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            List<VmOnlineAssessmentAnswers> list = new List<VmOnlineAssessmentAnswers>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OnlineAssessmentAnswers, VmOnlineAssessmentAnswers>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #endregion
    }
}