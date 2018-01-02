using System;
using System.Configuration;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.Agent;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Agent
{
    public class AgentCreateHandler
    {
        public AgentCreateHandler(VmAgentEdit vm)
        {
            Model = vm;
        }

        public VmAgentEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var AgentContact = Model.CreateNewAgentContactDataModel();
                AgentContact.AgentId = datamodel.DataId;

                var accountmodel = Model.CreateNewUserAccountDataModel();
                accountmodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var accountRole = Model.CreateNewUserRoleDataModel();
                accountRole.UserId = accountmodel.DataId;

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var acreateservice = new BaseCreateService<UserAccount>(accountmodel);
                        acreateservice.Invoke(wrapper.Transaction);

                        var roleservice = new BaseCreateService<Model.DataModel.User.UserRole>(accountRole);
                        roleservice.Invoke(wrapper.Transaction);

                        var createservice = new BaseCreateService<Model.DataModel.Agent.Agent>(datamodel);
                        createservice.Invoke(wrapper.Transaction);


                        var contactservice = new BaseCreateService<Model.DataModel.Agent.AgentContact>(AgentContact);
                        contactservice.Invoke(wrapper.Transaction);


                        //TODO: 需要分配代理商角色，等配置好了再说
                       
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
                LogHelper.LogException("AgentCreateHandler", "创建失败", LogLevel.Error, ex);
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
