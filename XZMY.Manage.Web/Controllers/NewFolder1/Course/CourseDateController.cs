using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Model.ViewModel;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
namespace XZMY.Manage.Web.Controllers.Course
{
    public class CourseDateController : ControllerBase
    {
        //列表 Ajax 获取数据
        public ActionResult AjaxList(Guid CourseId)
        {
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.Courses.CourseDate>()
            {
                ColumnMember = m => m.CourseId,
                ColumnValue = CourseId
            };
            var result = service.Invoke();

            return Json(new { success = true, rows = result, errors = GetErrors() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AjaxEdit(VmCourseDate model)
        {
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Courses.CourseDate>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Courses.CourseDate>(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }


        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmCourseDate GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<CourseDate>(Id);
            var entity = service.Invoke() ?? new CourseDate();
            return entity.CreateViewModel<CourseDate, VmCourseDate>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmCourseDate model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<CourseDate>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<CourseDate>(model);
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
            var service = new BaseDeleteService<CourseDate>(Id);
            service.Invoke();
        }

        public List<VmCourseDate> GetList(VmCourseDate model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<CourseDate>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<CourseDate>>
                    {
                        new CustomConditionPlus<CourseDate>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<CourseDate, object>>[]
                            {
                                m => m.DepartureCity
                            },
                        }
                    },
                SortMember = new Expression<Func<CourseDate, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<CourseDate>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<CourseDate, object>>[] { x => x.DataId }
                });
            }
            if (model.CourseId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<CourseDate>
                {
                    Value = model.CourseId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<CourseDate, object>>[] { x => x.CourseId }
                });
            }


            var result = service.Invoke();
            List<VmCourseDate> list = new List<VmCourseDate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<CourseDate, VmCourseDate>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}