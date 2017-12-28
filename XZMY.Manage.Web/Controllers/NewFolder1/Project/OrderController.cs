using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Order;
using XZMY.Manage.Service.Handlers.Project;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Web.Controllers.Project
{
    //课程1
    public class OrderController : ControllerBase
    {
        #region 页面
        /// <summary>
        /// 账目管理 账目列表
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "账目列表", Code = "OrderMoneyList", ModuleCode = "ORDER", Url = "/Order/OrderMoneyList", Visible = true)]
        public ActionResult OrderMoneyList()
        {
            return View();
        }
        #endregion
        #region AJAX
        /// <summary>
        /// AJAX获取账目列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public ActionResult AjaxOrderMoneyList(VmSearchBase model,Int32? OrderType)
        {
            var service = new CustomSearchWithPaginationService<InCome>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<InCome>>()
                {
                    new CustomConditionBase<InCome>()
                    {
                        Value = 0.00,
                        Operation = SqlOperation.NotEquals,
                        Member = x => x.PayPrice
                    }
                },
                SortMember = new Expression<Func<InCome, object>>[] { x => x.CreatedTime }
            };
            service.CustomConditions.AddRange(
                   new List<CustomCondition<InCome>>
                   {
                        new CustomConditionPlus<InCome>
                        {
                            Value = DateTime.Parse("1901-01-01"),
                            Operation = SqlOperation.Greater,
                            Member = new Expression<Func<InCome, object>>[] { x => x.PayTime}
                        }
                   });
            #region 订单类型 课程或是活动
            if (OrderType.HasValue)
            {
                if (OrderType.Value > 0)
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<InCome>>
                    {
                        new CustomConditionPlus<InCome>
                        {
                            Value = OrderType,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<InCome, object>>[] { x => x.Type}
                        }
                    });
                }
            }
            #endregion
            var result = service.Invoke();
            List<VmInCome> list = new List<VmInCome>();
            foreach(var m in result.Results)
            {
                list.Add(m.CreateViewModel<InCome, VmInCome>());
            }
            foreach (var m in list)
            {
                if (String.IsNullOrEmpty(m.PayName))
                {
                    if (m.Type == 1)
                    {
                        OrderProjectController bllOrderProject = new OrderProjectController();
                        m.PayName = bllOrderProject.GetModel(m.OrderId).Name;
                    }
                    else
                    {
                        OrderCourseController bllOrderCourse = new OrderCourseController();
                        m.PayName = bllOrderCourse.GetModel(m.OrderId).Name;
                    }
                }
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 功能
        /// <summary>
        /// 添加录入支付数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid InComeAddEdit(VmInCome model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId != Guid.Empty)
            {
                var handler = new BaseModifyHandler<InCome>(model);
                var result = handler.Invoke();
                if (result.Code == 0)
                {
                    returnId = model.DataId;
                }
            }
            else
            {
                var handler = new BaseCreateHandler<InCome>(model);
                var result = handler.Invoke();
                if (result.Code == 0)
                {
                    returnId = result.Output;
                }
            }

            return returnId;
        }
        /// <summary>
        /// 获取支付数据
        /// </summary>
        /// <param name="InComeId"></param>
        /// <returns></returns>
        public VmInCome InComeGetModel(Guid InComeId)
        {
            var service = new GetEntityByIdService<InCome>(InComeId);
            var model = service.Invoke();
            return model.CreateViewModel<InCome, VmInCome>();
        }

        
        public List<VmOrderCourse> OrderCourseGetList(VmOrderCourse model)
        {
            List<VmOrderCourse> list = new List<VmOrderCourse>();
            var service = new CustomSearchWithPaginationService<OrderCourse>
            {
                PageIndex = 1,
                PageSize = 999999,
                CustomConditions = new List<CustomCondition<OrderCourse>>()
                {
                    new CustomConditionPlus<OrderCourse>()
                    {
                        Value =  string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<OrderCourse, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<OrderCourse, object>>[] { x => x.CreatedTime }
            };

            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderCourse, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.OrderNo))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
                {
                    Value = model.OrderNo,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderCourse, object>>[] { x => x.OrderNo }
                });
            }
            var result = service.Invoke();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OrderCourse, VmOrderCourse>());
            }
            return list;
        }


        public List<VmOrderProject> OrderProjectGetList(VmOrderProject model)
        {
            List<VmOrderProject> list = new List<VmOrderProject>();
            var service = new CustomSearchWithPaginationService<OrderProject>
            {
                PageIndex = 1,
                PageSize = 999999,
                CustomConditions = new List<CustomCondition<OrderProject>>()
                {
                    new CustomConditionPlus<OrderProject>()
                    {
                        Value =  string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<OrderProject, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<OrderProject, object>>[] { x => x.CreatedTime }
            };

            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderProject>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderProject, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.OrderNo))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderProject>
                {
                    Value = model.OrderNo,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderProject, object>>[] { x => x.OrderNo }
                });
            }
            var result = service.Invoke();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OrderProject, VmOrderProject>());
            }
            return list;
        }

        public Guid OrderProjectAddEdit(VmOrderProject model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OrderProject>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OrderProject>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }

        public Guid OrderCourseAddEdit(VmOrderCourse model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OrderCourse>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OrderCourse>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        #endregion
    }
}