using System;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.Agent;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Agent
{
    public class AgentModifyHandler
    {
        public AgentModifyHandler(VmAgentEdit vm)
        {
            Model = vm;
        }

        public VmAgentEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<Model.DataModel.Agent.Agent>(Model.DataId);
                var oldmodel = goservice.Invoke();

                var datamodel = Model.MergeDataModel(oldmodel);
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var contact = new GetEntityByIdService<Model.DataModel.Agent.AgentContact>(Model.AgentContact.DataId).Invoke();
                contact.Name = Model.AgentContact.Name;
                contact.Mobile = Model.AgentContact.Mobile;
                contact.Tel = Model.AgentContact.Tel;
                contact.Email = Model.AgentContact.Email;
                contact.QQ = Model.AgentContact.QQ;
                contact.Description = Model.AgentContact.Description;



                //帐号信息赋值
                var getService = new GetEntityByIdService<UserAccount>(Model.UserId);
                var accountmodel = getService.Invoke();
                accountmodel.Gender = Model.Gender;
                accountmodel.RealName = Model.RealName;
                accountmodel.State = Model.State;
                if (!string.IsNullOrWhiteSpace(Model.Password))
                    accountmodel.Password = Model.Password.ToMd5();

                accountmodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var acreateservice = new BaseUpdateService<UserAccount>(accountmodel);
                        acreateservice.Invoke(wrapper.Transaction);

                        var createservice = new BaseUpdateService<Model.DataModel.Agent.Agent>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        var contactservice = new BaseUpdateService<Model.DataModel.Agent.AgentContact>(contact);
                        contactservice.Invoke(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }
                AuthCenter.ClearUserResourceCache(Model.DataId);
                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AgentModifyHandler", "编辑失败", LogLevel.Error, ex);
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
