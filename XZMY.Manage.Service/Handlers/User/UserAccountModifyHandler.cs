using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Service.Handlers.User
{
    public class UserAccountModifyHandler
    {
        public UserAccountModifyHandler(VmUserAccountEdit vm)
        {
            Model = vm;
        }

        public VmUserAccountEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            var tablename = "SYS_USER";
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<UserAccount>(Model.DataId, tablename);
                var oldmodel = goservice.Invoke();

                var datamodel = Model.MergeDataModel(oldmodel);
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var roleids = Model.GetRoleIdList();

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<UserAccount>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        if (roleids != null)
                        {
                            var groleservice = new GetEntityByForeignIdService<Sys_UserRole>()
                            {
                                ForeignId = Model.DataId,
                                ForeignMember = m => m.UserId
                            };
                            var oldroles = groleservice.Invoke();
                            var oldroleids = oldroles.Select(m => m.RoleId).ToList();

                            var urcr = roleids.Compare(oldroleids);

                            var roles = urcr.AddedElements.Select(m =>
                            {
                                var ur = new Sys_UserRole()
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


                            oldroles.Where(m => m.RoleId.IsIn(urcr.RemovedElements)).Select(m => m.DataId).Foreach(m =>
                            {
                                var deleteserivce = new BaseDeleteService<Sys_UserRole>(m);
                                deleteserivce.Invoke(wrapper.Transaction);
                            });
                        }
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
                LogHelper.LogException("UserAccountModifyHandler", "编辑失败", LogLevel.Error, ex);
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
