using System;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Data.SqlServer.Impl;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;

namespace XZMY.Manage.Service.Handlers.Action
{
    public class ActionBaseModifyHandler
    {
        public ActionBaseModifyHandler(VmActionEdit vm)
        {
            Model = vm;
        }

        public VmActionEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.ConvertTo<Sys_Action>();
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new UpdateActionBase(datamodel);
                        createservice.Execute(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }

                }
                AuthCenter.ClearRoleResourceCache(Model.DataId);
                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("ActionBaseModifyHandler", "编辑失败", LogLevel.Error, ex);
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
