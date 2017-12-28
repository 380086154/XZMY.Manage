using System;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ServiceModel.Members;
using XZMY.Manage.Model.ViewModel.Agent;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.WebApiHandlers.Members
{
    public class MemberCreateHandler
    {
        public MemberCreateHandler(SmCreateMember vm)
        {
            Model = vm;
        }

        public SmCreateMember Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var accountmodel = Model.CreateNewStudentDataModel();
                accountmodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                var pmodel = Model.CreateNewParentDataModel();
                pmodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var refe = new R_StudentParent()
                {
                    DataId = Guid.NewGuid(),
                    ParentsId = pmodel.DataId,
                    StudentId = accountmodel.DataId,
                    IsMain = 1,
                    Relationship = 1
                };
                refe.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var acreateservice = new BaseCreateService<Member>(datamodel);
                        acreateservice.Invoke(wrapper.Transaction);

                        var createservice = new BaseCreateService<Student>(accountmodel);
                        createservice.Invoke(wrapper.Transaction);

                        var pcreateservice = new BaseCreateService<Parent>(pmodel);
                        pcreateservice.Invoke(wrapper.Transaction);


                        var rcreateservice = new BaseCreateService<R_StudentParent>(refe);
                        rcreateservice.Invoke(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }
                }

                var res = HandlerInvokeResult.SUCCESS_VIEWMODEL.DeepClone();
                res.DynamicOutput = datamodel.DataId;
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("MemberCreateHandler", "创建失败", LogLevel.Error, ex);
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
