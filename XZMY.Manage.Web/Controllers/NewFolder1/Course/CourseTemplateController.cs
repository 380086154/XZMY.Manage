using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel;
using T2M.Common.DataServiceComponents.Data.Query;
using XZMY.Manage.Service.Auth.Attributes;

namespace XZMY.Manage.Web.Controllers.Course
{


    public class CourseTemplateController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "课程模版列表", Code = "CourseTemplateList", ModuleCode = "COURSE", Url = "/CourseTemplate/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 创建编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AutoCreateAuthAction(Name = "创建课程模版", Code = "CourseTemplateEdit", ModuleCode = "COURSE", Url = "/CourseTemplate/Edit", Visible = true, Remark = "")]
        public ActionResult Edit(Guid? id)
        {
            var entity = new CourseTemplate();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<CourseTemplate>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<CourseTemplate, VmCourseTemplate>());
        }

        //用户 创建/编辑 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmCourseTemplate model)
        {
            //if (ModelState.IsValid)
            //{
            model.SuitablePerson = string.Format(",{0},", Request.Params["dllFitGrade[]"]);
            model.ScoreItemNames = Request.Form["ckScoreItemNames[]"];

            if (Request.Params["ckScoreItemNames"] != null)
            {
                model.ScoreItemNames = Request.Params["ckScoreItemNames"].ToString();
            }
            model.DifficultyValue = Request.Params["dllDifficultyValue"].ToInt32(5);
            model.CompletionValue = 10 - model.DifficultyValue;
            model.FeeValue = Request.Params["dllFeeValue"].ToInt32(5);

            if (Request.Params["dllCountry"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CourseTemplatePlaceLocationId = Request.Params["dllCountry"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllProvince"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CourseTemplatePlaceLocationId = Request.Params["dllProvince"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllCity"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CourseTemplatePlaceLocationId = Request.Params["dllCity"].ToGuid(Guid.Empty);
            }

            model.CourseTemplatePlaceName = model.CourseTemplatePlaceName.Replace(",--请选择--", "").Replace(",请选择", "");
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Courses.CourseTemplate>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Courses.CourseTemplate>(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }

        //
        public ActionResult AjaxList(VmCourseTemplate model)
        {
            var service = new CustomSearchWithPaginationService<CourseTemplate>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<CourseTemplate>>
                {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.Name,x=>x.Code }
                    }
                },
                SortMember = new Expression<Func<CourseTemplate, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<CourseTemplate>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<CourseTemplate, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();
            List<VmCourseTemplate> list = new List<VmCourseTemplate>();
            foreach (var m in result.Results)
            {
               list.Add( m.CreateViewModel<CourseTemplate, VmCourseTemplate>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取课程模板信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmCourseTemplate GetVmCourseTemplate(Guid Id)
        {
            VmCourseTemplate model = new VmCourseTemplate();
            var service = new GetEntityByIdService<CourseTemplate>(Id);
            var entity = service.Invoke();
            model = entity.CreateViewModel<CourseTemplate, VmCourseTemplate>();
            return model;
        }
        /// <summary>
        /// 通过模板ID获取课程的ID
        /// </summary>
        /// <param name="CourseTemplateId">课程模板ID</param>
        /// <returns></returns>
        public VmCourseEdit GetCourse(Guid CourseTemplateId)
        {
            var service = new CustomSearchWithPaginationService<Model.DataModel.Courses.Course>
            {
                PageIndex =1,
                PageSize =1,
                CustomConditions = new List<CustomCondition<Model.DataModel.Courses.Course>>
                {
                    new CustomConditionPlus<Model.DataModel.Courses.Course>
                    {
                        Value = CourseTemplateId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Model.DataModel.Courses.Course, object>>[] { x => x.CourseTemplateId }
                    },
                    new CustomConditionPlus<Model.DataModel.Courses.Course>
                    {
                        Value = Model.Enum.EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Model.DataModel.Courses.Course, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<Model.DataModel.Courses.Course, object>>[] { x => x.CreatedTime }
            };
            var result = service.Invoke();
            Model.ViewModel.Courses.VmCourseEdit model= null;
            if (result.TotalCount > 0)
            {
                model = result.Results[0].CreateViewModel<Model.DataModel.Courses.Course, Model.ViewModel.Courses.VmCourseEdit>();
            }
            return model;
        }
        public List<VmCourseTemplate> GetListVmCourseTemplate(String TemplateName, String ItemName, String SuitablePerson)
        {
            var service = new CustomSearchWithPaginationService<CourseTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<CourseTemplate>>
                {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = TemplateName,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<CourseTemplate, object>>[] { x => x.CreatedTime }
            };
            #region ItemName
            if (!String.IsNullOrEmpty(ItemName))
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<CourseTemplate>>
                    {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = ItemName,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.ScoreItemNames}
                    }
                    });
            }
            #endregion
            #region SuitablePerson
            if (!String.IsNullOrEmpty(SuitablePerson))
            {
                String iSuitablePerson = "2";
                switch (SuitablePerson)
                {
                    case "小学":
                        iSuitablePerson = "2";
                        break;
                    case "初中":
                        iSuitablePerson = "3";
                        break;
                    case "高中":
                        iSuitablePerson = "4";
                        break;
                }
                service.CustomConditions.AddRange(
                new List<CustomCondition<CourseTemplate>>
                    {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = iSuitablePerson,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.SuitablePerson }
                    }
                    });
            }
            #endregion
            var result = service.Invoke();
            List<VmCourseTemplate> list = new List<VmCourseTemplate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<CourseTemplate, VmCourseTemplate>());
            }
            return list;
        }



        /// <summary>
        /// 获取课程模板列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<VmCourseTemplate> GetList(VmCourseTemplate model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<CourseTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<CourseTemplate>>
                {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = model.Name??String.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.Name }
                    }
                },
                SortMember = new Expression<Func<CourseTemplate, object>>[] { x => x.CreatedTime }
            };
            #region State
            if ((int)model.State > 0)
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<CourseTemplate>>
                    {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = model.State,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.State }
                    }
                    });
            }
            #endregion
            #region ItemName
            if (!String.IsNullOrEmpty(model.ScoreItemNames))
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<CourseTemplate>>
                    {
                    new CustomConditionPlus<CourseTemplate>
                    {
                        Value = model.ScoreItemNames,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.ScoreItemNames }
                    }
                    });
            }
            #endregion
            #region SuitablePerson
            if (!String.IsNullOrEmpty(model.SuitablePerson))
            {
                var sSorts = model.SuitablePerson.Split(",");
                foreach (var mSort in sSorts)
                {
                    if (mSort.ToInt32(0) != 0)
                    {
                        service.CustomConditions.AddRange(
                        new List<CustomCondition<CourseTemplate>>
                            {
                            new CustomConditionPlus<CourseTemplate>
                            {
                                Value = string.Format(",{0},",mSort.ToInt32(0)),
                                Operation = SqlOperation.Like,
                                Member = new Expression<Func<CourseTemplate, object>>[] { x => x.SuitablePerson }
                            }
                            });
                    }
                }
            }
            #endregion
            var result = service.Invoke();
            TotalCount = result.TotalCount;
            List<VmCourseTemplate> list = new List<VmCourseTemplate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<CourseTemplate, VmCourseTemplate>());
            }
            return list;
        }
    }
}