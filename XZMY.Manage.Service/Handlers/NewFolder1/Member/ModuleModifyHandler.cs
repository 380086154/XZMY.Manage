using System;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Member
{
    public class MemberModifyHandler
    {
        public MemberModifyHandler(VmMember vm)
        {
            Model = vm;
        }

        public VmMember Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<Model.DataModel.Members.Member>(Model.DataId);
                var oldmodel = goservice.Invoke();

                var datamodel = Model.MergeDataModel(oldmodel);
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<Model.DataModel.Members.Member>(datamodel);
                        createservice.Invoke(wrapper.Transaction);
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
                LogHelper.LogException("MemberModifyHandler", "编辑失败", LogLevel.Error, ex);
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
