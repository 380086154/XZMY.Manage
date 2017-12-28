using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Assessment
{
    public class AssessmentAnswerDeleteHandler
    {
        public AssessmentAnswerDeleteHandler(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Id == Guid.Empty) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {

                        var createservice = new BaseDeleteService<AssessmentAnswers>(Id);
                        createservice.Invoke(wrapper.Transaction);

                        var deleteSerivce = new BaseDeleteByForeignIdQuery<Scores>()
                        {
                            ForeignId = Id,
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

                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AssessmentAnswerDeleteHandler", "删除失败", LogLevel.Error, ex);
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
