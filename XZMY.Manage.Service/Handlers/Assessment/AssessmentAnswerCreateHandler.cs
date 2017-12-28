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
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Assessment
{
    public class AssessmentAnswerCreateHandler
    {
        public AssessmentAnswerCreateHandler(VmAssessmentAnswer vm)
        {
            Model = vm;
        }

        public VmAssessmentAnswer Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var smodels = Model.CreateScoreDataModels();
                smodels.ForEach(m => m.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer()));

                using (var wrapper = new SqlTransactionWrapper())
                {
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

                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AssessmentAnswerCreateHandler", "创建失败", LogLevel.Error, ex);
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
