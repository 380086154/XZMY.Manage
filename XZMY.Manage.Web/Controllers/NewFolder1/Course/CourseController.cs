using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Course;
using XZMY.Manage.Web.Controllers.Project;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.Helper;

namespace XZMY.Manage.Web.Controllers.Course
{
    public class CourseController : ControllerBase
    {
        #region 展示页面
        //列表
        [AutoCreateAuthAction(Name = "课程列表", Code = "CourseList", ModuleCode = "COURSE", Url = "/Course/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        [AutoCreateAuthAction(Name = "创建课程", Code = "CourseEdit", ModuleCode = "COURSE", Url = "/Course/Edit", Visible = true, Remark = "")]
        public ActionResult Edit(Guid? id, Guid? TemplateId)
        {
            VmCourseEdit model = new VmCourseEdit();
            var entity = new Model.DataModel.Courses.Course();
            if (TemplateId.HasValue)
            {
                //获取模板数据
                var service = new GetEntityByIdService<CourseTemplate>(TemplateId.Value);
                var modelCourseTemplate = service.Invoke();
                //填充到课程页面中
                entity.CourseTemplateId = modelCourseTemplate.DataId;
                entity.Name = modelCourseTemplate.Name;
                entity.Code = modelCourseTemplate.Code;
                entity.Pictures = modelCourseTemplate.Pictures;
                entity.CourseTypeId = modelCourseTemplate.CourseTypeId;
                entity.CourseTypeName = modelCourseTemplate.CourseTypeName;
                entity.CoursePlaceLocationId = modelCourseTemplate.CourseTemplatePlaceLocationId;
                entity.CoursePlaceName = modelCourseTemplate.CourseTemplatePlaceName;
                entity.Sponsor = modelCourseTemplate.Sponsor;
                entity.RecommendedIndex = modelCourseTemplate.RecommendedIndex;
                entity.SuitablePerson = modelCourseTemplate.SuitablePerson;
                entity.Service = modelCourseTemplate.Service;
                entity.MarketPrice = modelCourseTemplate.MarketPrice;
                entity.ActualPrice = modelCourseTemplate.ActualPrice;
                entity.Discount = modelCourseTemplate.Discount;
                entity.DepositPrice = modelCourseTemplate.DepositPrice;
                entity.DifficultyValue = modelCourseTemplate.DifficultyValue;
                entity.CompletionValue = modelCourseTemplate.CompletionValue;
                entity.FeeValue = modelCourseTemplate.FeeValue;
                entity.EnglishScore = modelCourseTemplate.EnglishScore;
                entity.LearnScore = modelCourseTemplate.LearnScore;
                entity.QualityScore = modelCourseTemplate.QualityScore;
                // 
                entity.CourseDescription = modelCourseTemplate.CourseTemplateDescription;
                entity.Schedule = modelCourseTemplate.Schedule;
                entity.Fee = modelCourseTemplate.Fee;
                entity.Stay = modelCourseTemplate.Stay;
                entity.Visa = modelCourseTemplate.Visa;
                entity.Stroke = modelCourseTemplate.Stroke;
                entity.Security = modelCourseTemplate.Security;


                entity.ActualPrice = modelCourseTemplate.ActualPrice;

                model = entity.CreateViewModel<Model.DataModel.Courses.Course, VmCourseEdit>();
            }

            
            if (id.HasValue)
            {
                model = GetVmCourse(id.Value);
            }
            return View(model);
        }
        /// <summary>
        /// 通过ID获取课程对象
        /// </summary>
        /// <param name="CourseId">课程ID</param>
        /// <returns></returns>
        public VmCourseEdit GetVmCourse(Guid CourseId)
        {
            var service = new GetEntityByIdService<Model.DataModel.Courses.Course>(CourseId);
            var entity = service.Invoke();
            return entity.CreateViewModel<Model.DataModel.Courses.Course, VmCourseEdit>();
        }
        /// <summary>
        /// 详细展示页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid? id)
        {
            var entity = new Model.DataModel.Courses.Course();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Model.DataModel.Courses.Course>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Model.DataModel.Courses.Course, VmCourseEdit>());
        }
        #endregion
        //用户 创建/编辑 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmCourseEdit model)
        {
            //if (ModelState.IsValid)
            //{
            model.SuitablePerson = string.Format(",{0},", Request.Params["dllFitGrade[]"]);
            //model.ScoreItemNames = Request.Form["ckScoreItemNames[]"];
            model.ScoreItemNames = Request.Form["ckScoreItemNames"];
            model.DifficultyValue = Request.Params["dllDifficultyValue"].ToInt32(5);
            model.CompletionValue = 10 - model.DifficultyValue;
            model.FeeValue = Request.Params["dllFeeValue"].ToInt32(5);
            model.State = (EState)Request.Params["State"].ToInt32(1);

            if (Request.Params["dllCountry"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CoursePlaceLocationId = Request.Params["dllCountry"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllProvince"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CoursePlaceLocationId = Request.Params["dllProvince"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllCity"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.CoursePlaceLocationId = Request.Params["dllCity"].ToGuid(Guid.Empty);
            }

            model.CoursePlaceName = model.CoursePlaceName.Replace(",--请选择--", "").Replace(",请选择", "");
            if (model.DataId == Guid.Empty)
            {
                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<Model.DataModel.Courses.Course>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {

                    return Json(new { success = false, errors = GetErrors() });
                }
                else
                {
                    OutputId = res.Output; 
                }
                SaveProjectDate(model.DataId);

                if (model.State == EState.启用)
                {

                    //推荐活动
                    new ProjectController().CreateProgramMessage(new ProgramMessage
                    {
                        Message = "新的历练:" + model.Name,
                        ProgramId = OutputId,
                        ProgramType = EProgramType.课程,
                        PlannerId = Guid.Empty,
                        PlannerName = string.Empty,
                        IsRead = false,
                        MessageTime = DateTime.Now,
                        MessageType = EMessageType.历练消息,
                        CreatedTime = DateTime.Now,
                        //CreatorId = Guid.Empty,
                        //CreatorName = string.Empty,
                        //ModifiedTime = DateTime.Now,
                        //ModifierId = Guid.Empty,
                        //ModifierName = string.Empty
                    });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Courses.Course>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                SaveProjectDate(model.DataId);
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
        }

        //保存时间
        public void SaveProjectDate(Guid id)
        {
            CourseDateController bllCourseDate = new CourseDateController();
            int TotalCount = 0;
            var OldList = bllCourseDate.GetList(new VmCourseDate() { CourseId = id }, out TotalCount);
            foreach (var mOld in OldList)
            {
                bllCourseDate.Delete(mOld.DataId);
            }

            var beginDate = (Request.Form["BeginDate[]"] ?? "").Split(',');
            var endDate = (Request.Form["EndDate[]"] ?? "").Split(',');
            //var departureCity = (Request.Form["DepartureCity[]"] ?? "").Split(',');
            var courseTimeId = (Request.Form["CourseTimeId[]"] ?? "").Split(',');

            var list = new List<VmCourseDate>();
            for (var i = 0; i < courseTimeId.Length; i++)
            {
                var vm = new VmCourseDate
                {
                    DataId = Guid.Empty,
                    CourseId = id,
                    BeginDate = beginDate[i].ToDateTime(DateTimePlus.GetMinDateTime()),
                    EndDate = endDate[i].ToDateTime(DateTimePlus.GetMinDateTime()),
                    DepartureCity = ""
                };
                bllCourseDate.CreateEdit(vm);
            }
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmCourseEdit model)
        {
            var service = new CustomSearchWithPaginationService<CourseList>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<CourseList>>
                {
                    new CustomConditionPlus<CourseList>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseList, object>>[] { x => x.CourseName,x=>x.CourseName }
                    }
                },
                SortMember = new Expression<Func<CourseList, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<CourseList>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<CourseList, object>>[] { x => x.State }
                });
            }
            
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateDate(VmCourseDate model)
        {
            var handler = new BaseCreateHandler<CourseDate>(model);
            handler.Invoke();

            return View();
        }
        public ActionResult ModifyDate(VmCourseDate model)
        {
            var handler = new BaseModifyHandler<CourseDate>(model);
            handler.Invoke();

            return View();
        }
        public ActionResult DeleteDate(Guid id)
        {
            var service = new BaseDeleteService<CourseDate>(id);
            service.Invoke();

            return View();
        }
    }
}