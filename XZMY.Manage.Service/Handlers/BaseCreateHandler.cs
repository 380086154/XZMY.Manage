using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Service.Handlers
{
    public class BaseCreateHandler<T> where T : EntityBase, IDataModel, new()
    {
        public BaseCreateHandler(IActionViewModel2C<T> vm)
        {
            Model = vm;
        }

        public IActionViewModel2C<T> Model { get; set; }

        protected virtual IInvokeTransactionService<T> GetService(T model)
        {
            return new BaseCreateService<T>(model);
        }

        public virtual HandlerInvokeResult<Guid> Invoke()
        {
            if (Model == null) return HandlerInvokeResult<Guid>.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                //Model.Id = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = GetService(datamodel);
                        createservice.Invoke(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }

                var res = HandlerInvokeResult<Guid>.SUCCESS_VIEWMODEL.DeepClone();
                res.Output = datamodel.DataId;
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CreateHandlerOf" + typeof(T), "创建失败", LogLevel.Error, ex);
                return new HandlerInvokeResult<Guid>
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}
