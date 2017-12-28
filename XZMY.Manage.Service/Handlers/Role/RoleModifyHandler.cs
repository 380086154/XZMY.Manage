using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.Extendsions;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Service.Handlers.Role
{
    public class RoleModifyHandler
    {
        public RoleModifyHandler(VmRoleEdit vm)
        {
            Model = vm;
        }

        public VmRoleEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {

                var datamodel = Model.GetDataModel();
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                var modules = Model.GetModuleIdList();
                var actions = Model.GetActionIdList();
                
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<Sys_Role>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        #region modules
                        if (modules != null)
                        {
                            var getrmservice = new GetEntityByForeignIdService<Sys_RoleModule>()
                            {
                                ForeignId = Model.DataId,
                                ForeignMember = m => m.RoleId
                            };
                            var oldmodules = getrmservice.Invoke();
                            var oldmoduleids = oldmodules.Select(m => m.ModuleId).ToList();

                            var rmcr = modules.Compare(oldmoduleids);

                            var moduledatas = rmcr.AddedElements.Select(m =>
                            {
                                var md = new Sys_RoleModule()
                                {
                                    DataId = Guid.NewGuid(),
                                    ModuleId = m,
                                    RoleId = datamodel.DataId
                                };
                                md.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                                return md;
                            }).ToList();

                            moduledatas.ForEach(m =>
                            {
                                var mdcreateService = new BaseCreateService<Sys_RoleModule>(m);
                                mdcreateService.Invoke(wrapper.Transaction);
                            });

                            oldmodules.Where(m => m.ModuleId.IsIn(rmcr.RemovedElements)).Select(m => m.DataId).Foreach(m =>
                            {
                                var deleteserivce = new BaseDeleteService<Sys_RoleModule>(m);
                                deleteserivce.Invoke(wrapper.Transaction);
                            });

                        }
                        #endregion

                        #region actions

                        if (actions != null)
                        {

                            var getraservice = new GetEntityByForeignIdService<Sys_RoleAction>()
                            {
                                ForeignId = Model.DataId,
                                ForeignMember = m => m.RoleId
                            };
                            var oldactions = getraservice.Invoke();
                            var oldactionids = oldactions.Select(m => m.ActionId).ToList();

                            var racr = actions.Compare(oldactionids);

                            var actiondatas = racr.AddedElements.Select(m =>
                            {
                                var md = new Sys_RoleAction()
                                {
                                    DataId = Guid.NewGuid(),
                                    ActionId = m,
                                    RoleId = datamodel.DataId
                                };
                                md.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                                return md;
                            }).ToList();

                            actiondatas.ForEach(m =>
                            {
                                var mdcreateService = new BaseCreateService<Sys_RoleAction>(m);
                                mdcreateService.Invoke(wrapper.Transaction);
                            });


                            oldactions.Where(m => m.ActionId.IsIn(racr.RemovedElements)).Select(m => m.DataId).Foreach(m =>
                            {
                                var deleteserivce = new BaseDeleteService<Sys_RoleAction>(m);
                                deleteserivce.Invoke(wrapper.Transaction);
                            });
                        }
                        #endregion

                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }
                AuthCenter.ClearRoleResourceCache(datamodel.DataId);
                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("RoleModifyHandler", "编辑失败", LogLevel.Error, ex);
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
