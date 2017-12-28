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

namespace XZMY.Manage.Service.Handlers.Action
{
    public class ActionCreateHandler
    {
        public ActionCreateHandler(VmActionEdit vm)
        {
            Model = vm;
        }

        public VmActionEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {

                var datamodel = Model.GetDataModel(true);
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseCreateService<Sys_Action>(datamodel);
                        createservice.Invoke(wrapper.Transaction);
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
                LogHelper.LogException("ActionCreateHandler", "创建失败", LogLevel.Error, ex);
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
