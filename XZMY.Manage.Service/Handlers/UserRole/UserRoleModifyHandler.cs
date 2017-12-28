using System;
using System.Collections.Generic;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.Extendsions;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.UserRole
{
    public class UserRoleModifyHandler
    {
        public UserRoleModifyHandler(List<VmUserRoleEdit> vm)
        {
            Model = vm;
        }

        public List<VmUserRoleEdit> Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var userId = Model.FirstOrDefault().UserId;

                var service = new GetEntityByForeignIdService<Sys_UserRole>
                {
                    ForeignMember = x => x.UserId,
                    ForeignId = userId
                };
                var idList = service.Invoke().Select(x => x.DataId).ToList();

                using (var wrapper = new SqlTransactionWrapper())
                {
                    idList.Foreach(m =>
                    {
                        var deleteserivce = new BaseDeleteService<Sys_UserRole>(m);
                        deleteserivce.Invoke(wrapper.Transaction);
                    });

                    foreach (var model in Model)
                    {
                        var datamodel = model.CreateNewDataModel();
                        //datamodel = model.ConvertTo<Sys_UserRole>();
                        datamodel.RoleId = model.RoleId;
                        datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                        try
                        {
                            var createservice = new BaseCreateService<Sys_UserRole>(datamodel);
                            createservice.Invoke(wrapper.Transaction);
                        }
                        catch
                        {
                            wrapper.HasError = true;
                            throw;
                        }
                    }
                }

                AuthCenter.ClearUserResourceCache(userId);
                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("UserRoleModifyHandler", "编辑失败", LogLevel.Error, ex);
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
