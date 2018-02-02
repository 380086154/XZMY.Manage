using System;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel.WeixinUserInfo;
using XZMY.Manage.Model.DataModel;

namespace XZMY.Manage.Service.Handlers.WeixinUserInfo
{
    public class CreateHandler
    {
        public CreateHandler(VmWeixinUserInfoEdit vm)
        {
            Model = vm;
        }

        public CreateHandler(Guid id)
        {
            Model = new VmWeixinUserInfoEdit { DataId = id };
        }

        public VmWeixinUserInfoEdit Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var createservice = new BaseCreateService<WeixinUserInfoDto>(datamodel);
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
                LogHelper.LogException("WeixinUserInfoCreateHandler", "创建失败", LogLevel.Error, ex);
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
