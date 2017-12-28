using System;
using System.Collections.Generic;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel.Assessment;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Assessment
{
    public class AssessmentAnswerUltimatesHandler
    {
        /// <summary>
        /// 新增、编辑、删除
        /// 删除时不传进来即可实现自动删除
        /// </summary>
        /// <param name="vm"></param>
        public AssessmentAnswerUltimatesHandler(IList<VmAssessmentAnswer> vm)
        {
            Model = vm;
        }

        public IList<VmAssessmentAnswer> Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null || Model.Count == 0) return HandlerInvokeResult.NULL_VIEWMODEL;

            var glservice = new GetEntityByForeignIdService<AssessmentAnswers>
            {
                ForeignId = Model.First().QuestionsId,
                ForeignMember = m => m.QuestionsId
            };
            var oldlist = glservice.Invoke();
            var a2del = oldlist.Where(m => !m.DataId.IsIn(Model.Select(n => n.DataId)));

            using (var wrapper = new SqlTransactionWrapper())
            {
                try
                {
                    foreach (var answer in a2del)
                    {
                        try
                        {
                            var createservice = new BaseDeleteService<AssessmentAnswers>(answer.DataId);
                            createservice.Invoke(wrapper.Transaction);

                            var deleteSerivce = new BaseDeleteByForeignIdQuery<Scores>()
                            {
                                ForeignId = answer.DataId,
                                ForeignMember = m => m.SourceId
                            };
                            deleteSerivce.Execute(wrapper.Transaction);
                        }
                        catch
                        {
                            wrapper.HasError = true;
                            throw;
                        }
                    }

                    foreach (var answer in Model)
                    {
                        var goservice = new GetEntityByIdService<AssessmentAnswers>(answer.DataId);

                        var oldanswer = goservice.Invoke();
                        if (oldanswer != null)
                        {
                            var dataanswer = answer.MergeDataModel(oldanswer);
                            dataanswer.SetModifier(
                                LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                            var sanswers = answer.CreateScoreDataModels();
                            sanswers.ForEach(m => m.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer()));

                            try
                            {
                                var createservice = new BaseUpdateService<AssessmentAnswers>(dataanswer);
                                createservice.Invoke(wrapper.Transaction);

                                var deleteSerivce = new BaseDeleteByForeignIdQuery<Scores>()
                                {
                                    ForeignId = answer.DataId,
                                    ForeignMember = m => m.SourceId
                                };
                                deleteSerivce.Execute(wrapper.Transaction);

                                sanswers.ForEach(m =>
                                {
                                    var acreateservice = new BaseCreateService<Scores>(m);
                                    acreateservice.Invoke(wrapper.Transaction);
                                });
                            }
                            catch
                            {
                                wrapper.HasError = true;
                                throw;
                            }
                        }
                        else
                        {
                            var datamodel = answer.CreateNewDataModel();
                            answer.DataId = datamodel.DataId;
                            datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                            var smodels = answer.CreateScoreDataModels();
                            smodels.ForEach(m => m.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer()));

                            try
                            {
                                var createservice = new BaseCreateService<AssessmentAnswers>(datamodel);
                                createservice.Invoke(wrapper.Transaction);
                                smodels.ForEach(m =>
                                {
                                    m.SourceId = datamodel.DataId;
                                    var acreateservice = new BaseCreateService<Scores>(m);
                                    acreateservice.Invoke(wrapper.Transaction);
                                });
                            }
                            catch
                            {
                                wrapper.HasError = true;
                                throw;
                            }
                        }
                    }

                    return HandlerInvokeResult.SUCCESS_VIEWMODEL;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("AssessmentAnswerUltimatesHandler", "编辑时异常", LogLevel.Error, ex);
                    return new HandlerInvokeResult()
                    {
                        Code = (int)HandlerInvokeResultCode.服务器异常,
                        Message = ex.Message,
                        Exception = ex
                    };
                }
            }
        }
    }
}
