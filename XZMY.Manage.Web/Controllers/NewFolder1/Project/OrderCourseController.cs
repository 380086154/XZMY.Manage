using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Agent;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Project;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Project
{
    //课程订单
    public class OrderCourseController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "课程订单", Code = "OrderCourseList", ModuleCode = "ORDER", Url = "/OrderCourse/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 课程订单查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid? id,Int32? type)
        {
            #region 课程订单信息
            VmOrderCourseDetails model = new VmOrderCourseDetails();
            var entityOrderCourse = new OrderCourse();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<OrderCourse>(id.Value);
                entityOrderCourse = service.Invoke();
            }
            model.modelVmOrderCourse = entityOrderCourse.CreateViewModel<OrderCourse, VmOrderCourse>();
            #endregion
            #region 评论信息
            if (model.modelVmOrderCourse != null)
            {
                
            }
            #endregion
            #region 课程信息
            if (model.modelVmOrderCourse != null)
            {
                var entityCourse = new Model.DataModel.Courses.Course();
                var serviceCourse = new GetEntityByIdService<Model.DataModel.Courses.Course>(model.modelVmOrderCourse.DataId);
                entityCourse = serviceCourse.Invoke();
                model.modelVmCourse = entityCourse.CreateViewModel<Model.DataModel.Courses.Course, VmCourseEdit>();
            }
            #endregion

            if (type.HasValue)
            {
                if (type.Value ==1)
                {
                    model.IsEditComment = true;
                }
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEditComment(Guid Id, String CommentContent)
        {
            Guid PlannerId = Guid.Empty;
            String PlannerName = "";
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
            }


            //获取数据
            var entityOrderCourse = new OrderCourse();
            var service = new GetEntityByIdService<OrderCourse>(Id);
            entityOrderCourse = service.Invoke();
            VmOrderCourse modelOrderCourse = entityOrderCourse.CreateViewModel<OrderCourse, VmOrderCourse>();

            //修改数据
            modelOrderCourse.CommentContent = CommentContent;
            modelOrderCourse.CommentPlannerId = PlannerId;
            modelOrderCourse.CommentPlannerName = PlannerName;
            modelOrderCourse.CommentTime = DateTime.Now;

            //保存数据
            var handler = new BaseModifyHandler<OrderCourse>(modelOrderCourse);
            var res = handler.Invoke();
            if (res.Code != 0)
            {
                return Json(new { success = false, errors = GetErrors() });
            }
            return Json(new { success = res.Success, Id = modelOrderCourse.DataId, errors = GetErrors() });

        }
        
        public ActionResult AjaxList(VmOrderCourse model)
        {
            int TotalCount = 0;
            var listResults = GetList(new VmOrderCourse() { ProcessState = model.ProcessState }, out TotalCount);

            return Json(new { success = true, total = TotalCount, rows = listResults, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }


        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOrderCourse GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<OrderCourse>(Id);
            var entity = service.Invoke() ?? new OrderCourse();
            return entity.CreateViewModel<OrderCourse, VmOrderCourse>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmOrderCourse model)
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
        /// <summary>
        /// 物理删除对象
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            var service = new BaseDeleteService<OrderCourse>(Id);
            service.Invoke();
        }
        public List<VmOrderCourse> GetList(VmOrderCourse model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<OrderCourse>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<OrderCourse>>
                    {
                        new CustomConditionPlus<OrderCourse>
                        {
                            Value = model.OrderNo,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<OrderCourse, object>>[]{m => m.OrderNo},
                        }
                    },
                SortMember = new Expression<Func<OrderCourse, object>>[] { m => m.CreatedTime },
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
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
                {
                    Value = model.Name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<OrderCourse, object>>[] { x => x.Name }
                });
            }


            if (model.ProcessState > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
                {
                    Value = model.ProcessState,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderCourse, object>>[] { x => x.ProcessState }
                });
            }
            if (model.StudentId != null)
            {
                if (model.StudentId != Guid.Empty)
                {
                    service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
                    {
                        Value = model.StudentId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OrderCourse, object>>[] { x => x.StudentId }
                    });
                }
            }
            var result = service.Invoke();
            List<VmOrderCourse> list = new List<VmOrderCourse>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OrderCourse, VmOrderCourse>());
            }
            TotalCount = result.TotalCount;
            return list;
        }

        #endregion
    }
}