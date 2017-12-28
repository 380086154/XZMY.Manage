using System;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.User
{
    public class UserAccountCreateHandler
    {
        public UserAccountCreateHandler(VmUserAccountEdit vm)
        {
            Model = vm;
        }

        public UserAccountCreateHandler(Guid id)
        {
            Model = new VmUserAccountEdit { DataId = id };
        }

        public VmUserAccountEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var roleids = Model.GetRoleIdList();

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseCreateService<UserAccount>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        if (roleids != null && roleids.Count > 0)
                        {
                            var roles = roleids.Select(m =>
                            {
                                var ur = new Sys_UserRole
                                {
                                    DataId = Guid.NewGuid(),
                                    RoleId = m,
                                    UserId = datamodel.DataId
                                };
                                ur.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                                return ur;
                            }).ToList();

                            roles.ForEach(m =>
                            {
                                var mdcreateService = new BaseCreateService<Sys_UserRole>(m);
                                mdcreateService.Invoke(wrapper.Transaction);
                            });
                        }
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
                LogHelper.LogException("UserAccountCreateHandler","创建失败", LogLevel.Error, ex);
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
