using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Service.Handlers
{
    public class BaseModifyHandler<T> where T : EntityBase, IDataModel, new()
    {
        private SmStudentPlan model;
      
        
        public BaseModifyHandler(IActionViewModel2M<T> vm)
        {
            Model = vm;
        }

        public IActionViewModel2M<T> Model { get; set; }

        public virtual HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var goservice = new GetEntityByIdService<T>(Model.DataId);
                var oldmodel = goservice.Invoke();

                var datamodel = Model.MergeDataModel(oldmodel);

                datamodel.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseUpdateService<T>(datamodel);
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
                LogHelper.LogException("BaseModifyHandlerOf" + typeof(T), "编辑失败", LogLevel.Error, ex);
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
