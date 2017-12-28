using System;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.Agent;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Order
{
    public class OrderProjectCreateHandler
    {
        public OrderProjectCreateHandler(VmOrderProject vm)
        {
            Model = vm;
        }

        public VmOrderProject Model { get; set; }

        public HandlerInvokeResult Invoke()
        {
            if (Model == null) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                var memberservice = new GetEntityBySingleColumnService<Model.DataModel.Members.Member>
                {
                    ColumnMember = m => m.Mobile,
                    ColumnValue = Model.Mobile
                };
                var existedmember = memberservice.Invoke().FirstOrDefault();
                if (existedmember != null) Model.MemberId = existedmember.DataId;

                var datamodel = Model.CreateNewDataModel();
                Model.DataId = datamodel.DataId;
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                var incomemodel = Model.CreateNewIncomeDataModel();
                incomemodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        if(existedmember == null)
                        {
                            var membermodel = Model.CreateNewMemberDataModel();
                            membermodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                            var mcreateservice = new BaseCreateService<Model.DataModel.Members.Member>(membermodel);
                            mcreateservice.Invoke(wrapper.Transaction);

                            var studentmodel = Model.CreateNewStudentDataModel();
                            studentmodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                            var screateservice = new BaseCreateService<Model.DataModel.Members.Student>(studentmodel);
                            screateservice.Invoke(wrapper.Transaction);

                            datamodel.StudentId = studentmodel.DataId;
                        }

                        var createservice = new BaseCreateService<OrderProject>(datamodel);
                        createservice.Invoke(wrapper.Transaction);

                        //var acreateservice = new BaseCreateService<InCome>(incomemodel);
                        //acreateservice.Invoke(wrapper.Transaction);

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
                LogHelper.LogException("OrderProjectCreateHandler", "创建失败", LogLevel.Error, ex);
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
