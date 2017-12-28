using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AssessmentAnswerModifyHandler
    {
        public AssessmentAnswerModifyHandler(VmAssessmentAnswer vm)
        {
            Model = vm;
        }

        public VmAssessmentAnswer Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<AssessmentAnswers>(Model.DataId);
                var oldmodel = goservice.Invoke();

                var datamodel = Model.MergeDataModel(oldmodel);
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var smodels = Model.CreateScoreDataModels();
                smodels.ForEach(m => m.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer()));

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {

                        var createservice = new BaseUpdateService<AssessmentAnswers>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        var deleteSerivce = new BaseDeleteByForeignIdQuery<Scores>()
                        {
                            ForeignId = Model.DataId,
                            ForeignMember = m => m.SourceId
                        };
                        deleteSerivce.Execute(wrapper.Transaction);

                        smodels.ForEach(m =>
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

                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AssessmentAnswerModifyHandler", "编辑失败", LogLevel.Error, ex);
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
