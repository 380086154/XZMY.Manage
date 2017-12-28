using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Impl.Query.Module;
using XZMY.Manage.Data.Query.Module;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.Extendsions;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Module
{
    public class ModuleModifyHandler
    {
        public ModuleModifyHandler(VmModuleEdit vm)
        {
            Model = vm;
        }

        public VmModuleEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {

                var datamodel = Model.GetDataModel();
                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<Sys_Module>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        IModifyActionOnModuleModified subquery = new ModifyActionOnModuleModified
                        {
                            ModuleId = datamodel.DataId,
                            Code = datamodel.Code,
                            TableName = "SYS_Action"
                        };
                        subquery.Execute(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }
                AuthCenter.ClearAllCache();
                return HandlerInvokeResult.SUCCESS_VIEWMODEL;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("ModuleModifyHandler", "编辑异常", LogLevel.Error, ex);
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
