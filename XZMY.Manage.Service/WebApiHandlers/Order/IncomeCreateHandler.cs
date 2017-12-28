using System;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.ServiceModel.Order;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Service.WebApiHandlers.Order
{
    public class IncomeCreateHandler
    {
        public IncomeCreateHandler(SmAddOrderIncome vm)
        {
            Model = vm;
        }

        public SmAddOrderIncome Model { get; set; }

        public HandlerInvokeResult<Guid> Invoke()
        {
            if (Model == null) return HandlerInvokeResult<Guid>.NULL_VIEWMODEL;

            try
            {
                var datamodel = Model.CreateNewDataModel();
                datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());



                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {

                        if (Model.OrderType == OrderType.Course)
                        {
                            var service = new GetEntityByIdService<OrderCourse>(Model.OrderId);
                            var order = service.Invoke();
                            if (order == null)
                            {
                                return new HandlerInvokeResult<Guid>
                                {
                                    Code = (int)HandlerInvokeResultCode.参数异常,
                                    Message = "没有找到对应的订单",
                                };
                            }
                            order.SetModifier(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());
                            order.PayPrice += Model.PayPrice;
                            order.PayCompletionTime = DateTime.Now;
                            order.ProcessState = EOrderProcessState.已结束;
                            order.IsPayCompletion = 1;

                            var mservice = new BaseUpdateService<OrderCourse>(order);
                            mservice.Invoke(wrapper.Transaction);


                        }
                        else if (Model.OrderType == OrderType.Project)
                        {
                            var service = new GetEntityByIdService<OrderProject>(Model.OrderId);
                            var order = service.Invoke();
                            if (order == null) return new HandlerInvokeResult<Guid>
                            {
                                Code = (int)HandlerInvokeResultCode.参数异常,
                                Message = "没有找到对应的订单",
                            };
                            order.PayPrice += Model.PayPrice;
                            order.PayCompletionTime = DateTime.Now;
                            //order.ProcessState = Model.Enum.EOrderProcessState.已结束;
                            //order.IsPayCompletion = Model.Enum.EOrderIsPayCompletion.是;
                            var mservice = new BaseUpdateService<OrderProject>(order);
                            mservice.Invoke(wrapper.Transaction);
                        }
                        var acreateservice = new BaseCreateService<InCome>(datamodel);
                        acreateservice.Invoke(wrapper.Transaction);



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
                LogHelper.LogException("IncomeCreateHandler", "创建失败", LogLevel.Error, ex);
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
