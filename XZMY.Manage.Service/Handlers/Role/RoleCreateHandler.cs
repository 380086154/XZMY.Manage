using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.Extendsions;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Role
{
    public class RoleCreateHandler
    {
        public RoleCreateHandler(VmRoleEdit vm)
        {
            Model = vm;
        }

        public VmRoleEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.GetDataModel(true);
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                var modules = Model.GetModuleIdList();
                var actions = Model.GetActionIdList();
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseCreateService<Sys_Role>(datamodel);
                        createservice.Invoke(wrapper.Transaction);
                        if (modules != null && modules.Count > 0)
                        {
                            var moduledatas = modules.Select(m =>
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

                        }
                        if (actions != null && actions.Count > 0)
                        {
                            var actiondatas = actions.Select(m =>
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
                LogHelper.LogException("RoleCreateHandler", "创建失败", LogLevel.Error, ex);
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
