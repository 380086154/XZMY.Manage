using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Service.Handlers.Assessment
{
    public class AssessmentQuestionEnableHandler
    {
        public AssessmentQuestionEnableHandler(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Id == Guid.Empty) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<AssessmentQuestions>(Id);
                var datamodel = goservice.Invoke();
                datamodel.State = EState.启用;
                datamodel.EnableTime = DateTime.Now;
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<AssessmentQuestions>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }

                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AssessmentQuestionEnableHandler", "编辑失败", LogLevel.Error, ex);
                return new HandlerInvokeResult
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }

    public class AssessmentQuestionBatchEnableHandler
    {
        public AssessmentQuestionBatchEnableHandler(Guid[] ids)
        {
            Ids = ids;
        }

        public Guid[] Ids { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Ids == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                using (var wrapper = new SqlTransactionWrapper())
                {
                    foreach (var id in Ids)
                    {

                        var goservice = new GetEntityByIdService<AssessmentQuestions>(id);
                        var datamodel = goservice.Invoke();
                        datamodel.State = EState.启用;
                        datamodel.EnableTime = DateTime.Now;
                        datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                        try
                        {
                            var createservice = new BaseUpdateService<AssessmentQuestions>(datamodel);
                            createservice.Invoke(wrapper.Transaction);

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
                LogHelper.LogException("AssessmentQuestionBatchEnableHandler", "编辑失败", LogLevel.Error, ex);
                return new HandlerInvokeResult
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }

    public class AssessmentQuestionDisableHandler
    {
        public AssessmentQuestionDisableHandler(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Id == Guid.Empty) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<AssessmentQuestions>(Id);
                var datamodel = goservice.Invoke();
                datamodel.State = EState.禁用;
                datamodel.DisableTime = DateTime.Now;
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<AssessmentQuestions>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }

                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AssessmentQuestionDisableHandler", "编辑失败", LogLevel.Error, ex);
                return new HandlerInvokeResult
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }


    public class AssessmentQuestionBatchDisableHandler
    {
        public AssessmentQuestionBatchDisableHandler(Guid[] ids)
        {
            Ids = ids;
        }

        public Guid[] Ids { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Ids == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                using (var wrapper = new SqlTransactionWrapper())
                {
                    foreach (var id in Ids)
                    {

                        var goservice = new GetEntityByIdService<AssessmentQuestions>(id);
                        var datamodel = goservice.Invoke();
                        datamodel.State = EState.禁用;
                        datamodel.DisableTime = DateTime.Now;
                        datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                        try
                        {
                            var createservice = new BaseUpdateService<AssessmentQuestions>(datamodel);
                            createservice.Invoke(wrapper.Transaction);

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
                LogHelper.LogException("AssessmentQuestionBatchDisableHandler", "编辑失败", LogLevel.Error, ex);
                return new HandlerInvokeResult
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}
