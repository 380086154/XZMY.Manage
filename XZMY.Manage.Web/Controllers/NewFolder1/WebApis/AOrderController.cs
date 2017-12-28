using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.ServiceModel.Order;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Web.Controllers.Course;
using XZMY.Manage.Web.Controllers.Customers;
using XZMY.Manage.Web.Controllers.Project;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    public class AOrderController : ApiController
    {
        /// <summary>
        /// 前台下订单使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Guid> CreateOrderWeb([FromBody]SmOrderWeb model)
        {
            if (model.type == 1)
            {
                //活动订单


                ProjectController bllProject = new ProjectController();
                VmProjectEdit modelProject = bllProject.GetVmProject(model.ProgramId);
                StudentController bllStudent = new StudentController();
                VmStudent modelStudent = bllStudent.GetStudent(model.StudentId);
                //课程订单
                SmProjectOrderCreate modelOrder = new SmProjectOrderCreate();
                modelOrder.OrderNo = DateTime.Now.ToStringFormat("yyyyMMddHHmmss");
                modelOrder.ProjectId = model.ProgramId;
                modelOrder.ProjectName = modelProject.Name;
                modelOrder.Sponsor = modelProject.Sponsor;
                modelOrder.DepositPrice = modelProject.DepositPrice;
                modelOrder.PayPrice = 0M;
                modelOrder.TotalPrice = modelProject.ActualPrice;
                modelOrder.BeginDate = model.StartTime;
                modelOrder.EndDate = model.EndTime;
                modelOrder.DepartureCity = model.DepartureCity;
                modelOrder.MemberId = modelStudent.MemberId;
                modelOrder.StudentId = model.StudentId;
                modelOrder.Name = model.StudentName;
                modelOrder.Mobile = model.StudentMobile;
                modelOrder.Email = model.StudentEmail;
                modelOrder.LocationId = model.StudentLocationId;
                modelOrder.LocationName = model.StudentLocationName;
                modelOrder.Address = model.StudentAddress;
                modelOrder.Education = model.StudentEducation;
                modelOrder.IdentityCard = model.IdentityCard;

                modelOrder.ContactEmail = model.ContactEmail;
                modelOrder.ContactMobile = model.ContactMobile;
                modelOrder.ContactName = model.ContactName;
                modelOrder.Remark = model.Remark;

                modelOrder.AgentId = Guid.Empty;
                modelOrder.AgentName = String.Empty;
                modelOrder.AgentType = Model.Enum.EOrderAgentType.自己报名;
                modelOrder.CancelTime = DateTime.MinValue;
                modelOrder.CommentContent = String.Empty;
                modelOrder.CommentPlannerId = Guid.Empty;
                modelOrder.CommentPlannerName = String.Empty;
                modelOrder.CommentTime = DateTime.MinValue;
                modelOrder.EnrollType = Model.Enum.EOrderEnrollType.自己报名;
                modelOrder.IsPayCompletion = Model.Enum.EOrderIsPayCompletion.否;
                modelOrder.PayCompletionTime = DateTime.MinValue;

                modelOrder.ProcessState = Model.Enum.EOrderProcessState.已报名;

                return CreateProjectOrder(modelOrder);
            }
            else 
            {
                



                CourseController bllCourse = new CourseController();
                VmCourseEdit modelCourse = bllCourse.GetVmCourse(model.ProgramId);
                StudentController bllStudent = new StudentController();
                VmStudent modelStudent = bllStudent.GetStudent(model.StudentId);
                //课程订单
                SmCourseOrderCreate modelOrder = new SmCourseOrderCreate();
                modelOrder.OrderNo = DateTime.Now.ToStringFormat("yyyyMMddHHmmss");
                modelOrder.CourseId = model.ProgramId;
                modelOrder.CourseName = modelCourse.Name;
                modelOrder.Sponsor = modelCourse.Sponsor;
                modelOrder.DepositPrice = modelCourse.DepositPrice;
                modelOrder.PayPrice = 0M;
                modelOrder.TotalPrice = modelCourse.ActualPrice;
                modelOrder.BeginDate = model.StartTime;
                modelOrder.EndDate = model.EndTime;
                modelOrder.MemberId = modelStudent.MemberId;
                modelOrder.StudentId = model.StudentId;
                modelOrder.Name = model.StudentName;
                modelOrder.Mobile = model.StudentMobile;
                modelOrder.Email = model.StudentEmail;
                modelOrder.LocationId = model.StudentLocationId;
                modelOrder.LocationName = model.StudentLocationName;
                modelOrder.Address = model.StudentAddress;
                modelOrder.Education = model.StudentEducation;
                modelOrder.IdentityCard = model.IdentityCard;

                modelOrder.ContactEmail = model.ContactEmail;
                modelOrder.ContactMobile = model.ContactMobile;
                modelOrder.ContactName = model.ContactName;
                modelOrder.Remark = model.Remark;

                modelOrder.AgentId = Guid.Empty;
                modelOrder.AgentName = String.Empty;
                modelOrder.AgentType = Model.Enum.EOrderAgentType.自己报名;
                modelOrder.CancelTime = DateTime.MinValue;
                modelOrder.CommentContent = String.Empty;
                modelOrder.CommentPlannerId = Guid.Empty;
                modelOrder.CommentPlannerName = String.Empty;
                modelOrder.CommentTime = DateTime.MinValue;
                modelOrder.EnrollType = Model.Enum.EOrderEnrollType.自己报名;
                modelOrder.IsPayCompletion = 2;
                modelOrder.PayCompletionTime = DateTime.MinValue;

                modelOrder.ProcessState = Model.Enum.EOrderProcessState.已报名;

                return CreateCourseOrder(modelOrder);
            }
            
        }


        /// <summary>
        /// 创建课程订单
        /// </summary>
        /// <param name="model">创建订单对象</param>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Guid> CreateCourseOrder([FromBody]SmCourseOrderCreate model)
        {
            if (model == null)
                return ApiHandlerInvokeResult<Guid>.NULL_VIEWMODEL;
            try
            {
                #region 重新整理字段
                CourseController bllCourse = new CourseController();
                VmCourseEdit modelCourse = bllCourse.GetVmCourse(model.CourseId);

                model.CancelTime = DateTime.MinValue;
                model.CommentTime = DateTime.MinValue;
                model.CourseName = modelCourse.Name;
                model.ModelCourse = modelCourse;
                model.OrderNo = DateTime.Now.ToStringFormat("yyyyMMddHHmmss");


                #endregion


                var handler = new BaseCreateHandler<OrderCourse>(model);
                var res = handler.Invoke();
                var f = new ApiHandlerInvokeResult<Guid>()
                {
                    Message = res.Message,
                    Output = (Guid)res.Output,
                    Success = res.Success,
                    Code = res.Code,
                };
                return f;
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }


        /// <summary>
        /// 创建活动订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Guid> CreateProjectOrder([FromBody]SmProjectOrderCreate model)
        {
            if (model == null)
                return ApiHandlerInvokeResult<Guid>.NULL_VIEWMODEL;
            try
            {
                var handler = new BaseCreateHandler<OrderProject>(model);
                var res = handler.Invoke();
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Message = res.Message,
                    Output = (Guid)res.Output,
                    Success = res.Success,
                    Code = res.Code,
                };
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }
        /// <summary>
        /// 录入支付信息11
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Guid> CreateOrderInCome([FromBody]SmPayInCome model)
        {
            OrderController bllOrder = new OrderController();
            if (model == null)
                return ApiHandlerInvokeResult<Guid>.NULL_VIEWMODEL;
            Guid returnId = Guid.Empty;
            VmInCome modelInCome = new VmInCome();
            List<VmOrderCourse> listOrderCourse = bllOrder.OrderCourseGetList(new VmOrderCourse() { OrderNo = model.OrderNo });
            if (listOrderCourse.Count > 0)
            {
                //课程
                var modelOrder = listOrderCourse[0];
                modelInCome.OrderId = modelOrder.DataId;
                modelInCome.Type = 2;
                modelInCome.PayPrice = model.PayPrice.ToDecimal(0);
                modelInCome.PayType = Model.Enum.EOrderPayType.线上支付;
                modelInCome.PayMode = Guid.Empty;
                modelInCome.PayModeName = "支付宝";
                modelInCome.PayTime = DateTime.Now;
                modelInCome.SerialNumber = model.TradeNo;
                modelInCome.PayAccount = model.PayAccount;
                modelInCome.PayName = model.PayName;
                modelInCome.Description = "";
                returnId = bllOrder.InComeAddEdit(modelInCome);
                if (returnId != Guid.Empty)
                {
                    modelOrder.PayPrice = modelOrder.PayPrice + modelInCome.PayPrice;
                    bllOrder.OrderCourseAddEdit(modelOrder);
                }
            }
            else
            {
                //活动
                List<VmOrderProject> listOrderProject = bllOrder.OrderProjectGetList(new VmOrderProject() { OrderNo = model.OrderNo });
                if (listOrderProject.Count > 0)
                {
                    var modelOrder = listOrderProject[0];
                    modelInCome.OrderId = modelOrder.DataId;
                    modelInCome.Type = 1;
                    modelInCome.PayPrice = model.PayPrice.ToDecimal(0);
                    modelInCome.PayType = Model.Enum.EOrderPayType.线上支付;
                    modelInCome.PayMode = Guid.Empty;
                    modelInCome.PayModeName = "支付宝";
                    modelInCome.PayTime = DateTime.Now;
                    modelInCome.SerialNumber = model.TradeNo;
                    modelInCome.PayAccount = model.PayAccount;
                    modelInCome.PayName = model.PayName;
                    modelInCome.Description = "";
                    returnId = bllOrder.InComeAddEdit(modelInCome);
                    if (returnId != Guid.Empty)
                    {
                        modelOrder.PayPrice = modelOrder.PayPrice + modelInCome.PayPrice;
                        bllOrder.OrderProjectAddEdit(modelOrder);
                    }
                }
            }
            if (returnId == Guid.Empty)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = returnId,
                    Success = false
                };
            }
            else
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = returnId,
                    Success = true
                };
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> OrderCancel([FromBody]OrderCancelParam model)
        {
            Guid returnId = Guid.Empty;
            OrderController bllOrder = new OrderController();
            if (model.Type == 1)
            {
                VmOrderProject modelOrder = new VmOrderProject();
                var tempList = bllOrder.OrderProjectGetList(new VmOrderProject() { DataId = model.OrderId });
                if (tempList.Count > 0)
                {
                    modelOrder = tempList[0];
                    if (modelOrder.PayPrice == 0)
                    {
                        modelOrder.ProcessState = Model.Enum.EOrderProcessState.已取消;
                        returnId = bllOrder.OrderProjectAddEdit(modelOrder);
                    }
                }
            }
            else if (model.Type == 2)
            {
                VmOrderProject modelOrder = new VmOrderProject();
                var tempList = bllOrder.OrderProjectGetList(new VmOrderProject() { DataId = model.OrderId });
                if (tempList.Count > 0)
                {
                    modelOrder = tempList[0];
                    if (modelOrder.PayPrice == 0)
                    {
                        modelOrder.ProcessState = Model.Enum.EOrderProcessState.已取消;
                        returnId = bllOrder.OrderProjectAddEdit(modelOrder);
                    }
                }
            }
            if (returnId == Guid.Empty)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = returnId,
                    Success = false
                };
            }
            else
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = returnId,
                    Success = true
                };
            }
        }

    }
}