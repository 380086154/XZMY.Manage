using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Project
{
    public class ProjectDateRefreshHandler
    {
        public ProjectDateRefreshHandler(IList<VmProjectDate> models)
        {
            Model = models;
        }

        public IList<VmProjectDate> Model;

        public HandlerInvokeResult Invoke()
        {
            if (Model == null || Model.Count == 0) return HandlerInvokeResult.NULL_VIEWMODEL;


            try
            {
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var deleteSerivce = new BaseDeleteByForeignIdQuery<ProjectDate>
                        {
                            ForeignId = Model.First().ProjectId,
                            ForeignMember = m => m.ProjectId
                        };
                        deleteSerivce.Execute(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }

                    foreach (var answer in Model)
                    {

                        var datamodel = answer.CreateNewDataModel();
                        answer.DataId = datamodel.DataId;
                        datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                        try
                        {
                            var createservice = new BaseCreateService<ProjectDate>(datamodel);
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
            }
            catch (Exception ex)
            {
                LogHelper.LogException("ProjectDateRefreshHandler", "编辑时异常", LogLevel.Error, ex);
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

