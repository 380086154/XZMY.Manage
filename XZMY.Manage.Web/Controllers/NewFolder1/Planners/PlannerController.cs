using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Web.Controllers.Auth;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    /// <summary>
    /// 规划师
    /// </summary>
    public class PlannerController : ControllerBase
    {
        #region 页面
        [HttpGet]
        [AutoCreateAuthAction(Name = "规划师创建", Code = "PlannerEdit", ModuleCode = "PLANNER", Url = "/Planner/Edit", Visible = true, Remark = "")]
        public ActionResult Edit(Guid? Id)
        {
            UserAccountController bllUserAccount = new UserAccountController();
            var entity = new Planner();
            if (Id.HasValue)
            {
                var service = new GetEntityByIdService<Planner>(Id.Value);
                entity = service.Invoke();
            }
            var model = entity.CreateViewModel<Planner, VmPlannerEdit>();
            model.modelUser = bllUserAccount.GetModel(model.UserId);
            return View(model);
        }
        /// <summary>
        /// 规划师查看页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid? Id)
        {
            var entity = new Planner();
            if (Id.HasValue)
            {
                var service = new GetEntityByIdService<Planner>(Id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Planner, VmPlannerEdit>());
        }
        /// <summary>
        /// 规划师列表展示页面
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "规划师列表", Code = "PlannerList", ModuleCode = "PLANNER", Url = "/Planner/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 学生活动列表
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "学生活动管理", Code = "PlannerProjectList", ModuleCode = "PLANNER", Url = "/Planner/ProjectList", Visible = true, Remark = "")]
        public ActionResult ProjectList()
        {
            //默认规划师信息
            Guid id = Guid.Empty;
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
            }



            var service = new GetEntityByIdService<Planner>(id);
            var entity = service.Invoke();
            if (entity == null) entity = new Model.DataModel.Planners.Planner();
            VmPlannerEdit modelVmPlanner = entity.CreateViewModel<Model.DataModel.Planners.Planner, Model.ViewModel.Planners.VmPlannerEdit>();
            #region 跟踪学生参加的成长
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CommandType cmdType = CommandType.Text;

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PlannerId",id)
            };

            string cmdText = "select Count(0) from Student where PlannerId = @PlannerId";
            object StatisticsStudent = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);

            cmdText = "select Count(0) from Student a where a.PlannerId = @PlannerId and EXISTS (SELECT * FROM OrderCourse a1 where a1.StudentId = a.Id and a1.ProcessState <> 4)";
            object StatisticsAttendCourse = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);
            //
            cmdText = "select Count(0) from Student a where a.PlannerId = @PlannerId and EXISTS(SELECT * FROM OrderProject a1 where a1.StudentId = a.Id and a1.ProcessState <> 4)";
            object StatisticsAttendProject = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);

            int iStatisticsStudent = 0;
            int.TryParse(StatisticsStudent.ToString(), out iStatisticsStudent);
            modelVmPlanner.StatisticsStudent = iStatisticsStudent;

            int iStatisticsAttendCourse = 0;
            int.TryParse(StatisticsAttendCourse.ToString(), out iStatisticsAttendCourse);
            modelVmPlanner.StatisticsAttendCourse = iStatisticsAttendCourse;

            int iStatisticsAttendProject = 0;
            int.TryParse(StatisticsAttendProject.ToString(), out iStatisticsAttendProject);
            modelVmPlanner.StatisticsAttendProject = iStatisticsAttendProject;


            #endregion
            return View(modelVmPlanner);
        }
        /// <summary>
        /// 学生课程管理
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "学生课程管理", Code = "PlannerCourseList", ModuleCode = "PLANNER", Url = "/Planner/CourseList", Visible = true, Remark = "")]
        public ActionResult CourseList()
        {
            //默认规划师信息
            Guid id = Guid.Empty;
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
            }

            var service = new GetEntityByIdService<Planner>(id);
            var entity = service.Invoke();
            if (entity == null) entity = new Model.DataModel.Planners.Planner();
            VmPlannerEdit modelVmPlanner = entity.CreateViewModel<Model.DataModel.Planners.Planner, Model.ViewModel.Planners.VmPlannerEdit>();



            #region 跟踪学生参加的成长
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CommandType cmdType = CommandType.Text;

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PlannerId",id)
            };

            string cmdText = "select Count(0) from Student where PlannerId = @PlannerId";
            object StatisticsStudent = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);

            cmdText = "select Count(0) from Student a where a.PlannerId = @PlannerId and EXISTS (SELECT * FROM OrderCourse a1 where a1.StudentId = a.Id and a1.ProcessState <> 4)";
            object StatisticsAttendCourse = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);
            //
            cmdText = "select Count(0) from Student a where a.PlannerId = @PlannerId and EXISTS(SELECT * FROM OrderProject a1 where a1.StudentId = a.Id and a1.ProcessState <> 4)";
            object StatisticsAttendProject = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteScalar(connString, cmdType, cmdText, sqlParams);

            int iStatisticsStudent = 0;
            int.TryParse(StatisticsStudent.ToString(), out iStatisticsStudent);
            modelVmPlanner.StatisticsStudent = iStatisticsStudent;

            int iStatisticsAttendCourse = 0;
            int.TryParse(StatisticsAttendCourse.ToString(), out iStatisticsAttendCourse);
            modelVmPlanner.StatisticsAttendCourse = iStatisticsAttendCourse;

            int iStatisticsAttendProject = 0;
            int.TryParse(StatisticsAttendProject.ToString(), out iStatisticsAttendProject);
            modelVmPlanner.StatisticsAttendProject = iStatisticsAttendProject;


            #endregion

            return View(modelVmPlanner);
        }
        /// <summary>
        /// 推荐活动
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="PlannerId"></param>
        /// <returns></returns>
        public ActionResult ExtendedRecommendProject(Guid StudentId, Guid PlannerId)
        {
            var serviceStudent = new GetEntityByIdService<Student>(StudentId);
            var entityStudent = serviceStudent.Invoke();
            if (entityStudent == null) entityStudent = new Student();
            VmStudent modelVmStudent = entityStudent.CreateViewModel<Student, VmStudent>();
            return View(modelVmStudent);
        }
        /// <summary>
        /// 推荐课程
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="PlannerId"></param>
        /// <returns></returns>
        public ActionResult ExtendedRecommendCourse(Guid StudentId, Guid PlannerId)
        {
            var serviceStudent = new GetEntityByIdService<Student>(StudentId);
            var entityStudent = serviceStudent.Invoke();
            if (entityStudent == null) entityStudent = new Student();
            VmStudent modelVmStudent = entityStudent.CreateViewModel<Student, VmStudent>();
            return View(modelVmStudent);
        }
        #endregion



        #region 规划师页面 创建 编辑和列表
        //创建/编辑


        #region Ajax方法
        #region 规划师 显示学生参加活动界面 

        public ActionResult AjaxOrderCourseList(VmSearchBase<V_OrderCourseList> model, Guid? PlannersId, bool? IsCancel)
        {
            var service = new CustomSearchWithPaginationService<V_OrderCourseList>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_OrderCourseList>>
                {
                    new CustomConditionPlus<V_OrderCourseList>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<V_OrderCourseList, object>>[] { x => x.CourseName, x=>x.Name }
                    }
                },

                SortMember = new Expression<Func<V_OrderCourseList, object>>[] { x => x.CreatedTime }
            };
            if (PlannersId != null)
            {
                //添加参数
                service.CustomConditions.AddRange(
                new List<CustomCondition<V_OrderCourseList>>
                    {
                    new CustomConditionPlus<V_OrderCourseList>
                    {
                        Value = PlannersId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_OrderCourseList, object>>[] { x => x.PlannerId}
                    }
                    });
            }

            if (IsCancel != null)
            {
                if (IsCancel == true)
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<V_OrderCourseList>>
                    {
                        new CustomConditionPlus<V_OrderCourseList>
                        {
                            Value = EOrderProcessState.已取消.GetHashCode(),
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<V_OrderCourseList, object>>[] { x => x.OrderCourseProcessState}
                        }
                    });
                }
                else
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<V_OrderCourseList>>
                    {
                        new CustomConditionPlus<V_OrderCourseList>
                        {
                            Value =EOrderProcessState.已取消.GetHashCode(),
                            Operation = SqlOperation.NotEquals,
                            Member = new Expression<Func<V_OrderCourseList, object>>[] { x => x.OrderCourseProcessState }
                        }
                    });
                }
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxOrderProjectList(VmSearchBase<V_OrderProjectList> model, Guid? PlannersId, bool? IsCancel)
        {
            var service = new CustomSearchWithPaginationService<V_OrderProjectList>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_OrderProjectList>>
                {
                    new CustomConditionPlus<V_OrderProjectList>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<V_OrderProjectList, object>>[] { x => x.ProjectName, x=>x.Name }
                    }
                },
                SortMember = new Expression<Func<V_OrderProjectList, object>>[] { x => x.CreatedTime }
            };
            if (PlannersId != null)
            {
                //添加参数
                service.CustomConditions.AddRange(
                new List<CustomCondition<V_OrderProjectList>>
                    {
                    new CustomConditionPlus<V_OrderProjectList>
                    {
                        Value = PlannersId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_OrderProjectList, object>>[] { x => x.PlannerId}
                    }
                    });
            }
            if (IsCancel != null)
            {
                if (IsCancel == true)
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<V_OrderProjectList>>
                    {
                        new CustomConditionPlus<V_OrderProjectList>
                        {
                            Value = EOrderProcessState.已取消.GetHashCode(),
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<V_OrderProjectList, object>>[] { x => x.OrderProjectProcessState}
                        }
                    });
                }
                else
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<V_OrderProjectList>>
                    {
                        new CustomConditionPlus<V_OrderProjectList>
                        {
                            Value =EOrderProcessState.已取消.GetHashCode(),
                            Operation = SqlOperation.NotEquals,
                            Member = new Expression<Func<V_OrderProjectList, object>>[] { x => x.OrderProjectProcessState}
                        }
                    });
                }
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmPlannerList model)
        {
           
            var service = new CustomSearchWithPaginationService<V_PlannerList>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_PlannerList>>()
                  {
                      new CustomConditionPlus<V_PlannerList>()
                      {
                          Value = model.Keyword??String.Empty,
                          Operation = SqlOperation.Like,
                          Member = new Expression<Func<V_PlannerList, object>>[] { x => x.LoginName,x=>x.Name }
                      }
                  },
                SortMember = new Expression<Func<V_PlannerList, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<V_PlannerList>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<V_PlannerList, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxPlannerStudentList(VmSearchBase<V_PlannerStudentList> model, Guid PlannerId, Int32? StudentOrderProjectCount, Int32? StudentOrderCourseCount)
        {
            var service = new CustomSearchWithPaginationService<V_PlannerStudentList>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_PlannerStudentList>>
                  {
                      new CustomConditionPlus<V_PlannerStudentList>
                      {
                          Value = PlannerId,
                          Operation = SqlOperation.Equals,
                          Member = new Expression<Func<V_PlannerStudentList, object>>[] { x => x.PlannerId}
                      }
                  },
                SortMember = new Expression<Func<V_PlannerStudentList, object>>[] { x => x.CreatedTime }
            };

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                #region 关键字搜索
                service.CustomConditions.Add(
                  new CustomConditionPlus<V_PlannerStudentList>
                  {
                      Value = model.Keyword,
                      Operation = SqlOperation.Equals,
                      Member = new Expression<Func<V_PlannerStudentList, object>>[] { x => x.PlannerName, x => x.StudentName, x => x.StudentMobile }
                  });
                #endregion
            }
            if (StudentOrderProjectCount != null)
            {
                #region StudentOrderProjectCount 学生下活动订单数量
                service.CustomConditions.Add(
                    new CustomConditionPlus<V_PlannerStudentList>
                    {
                        Value = StudentOrderProjectCount,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_PlannerStudentList, object>>[] { x => x.StudentOrderProjectCount }
                    });
                #endregion
            }
            if (StudentOrderCourseCount != null)
            {
                #region StudentOrderCourseCount 学生下课程订单数量
                service.CustomConditions.Add(
                    new CustomConditionPlus<V_PlannerStudentList>
                    {
                        Value = 0,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_PlannerStudentList, object>>[] { x => x.StudentOrderCourseCount }
                    });
                #endregion
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //保存 创建/编辑
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmPlannerEdit model)
        {
            Guid returnId = Guid.Empty;
            UserAccountController bllUserAccount = new UserAccountController();
            #region User

            Model.ViewModel.User.VmUserAccountEdit modelUser = new Model.ViewModel.User.VmUserAccountEdit();
            if (model.UserId != Guid.Empty)
            {
                modelUser = bllUserAccount.GetModel(model.UserId);
            }
            modelUser.LoginName = Request.Params["User_LoginName"].ToString();
            modelUser.RealName = model.Name;
            if (!String.IsNullOrEmpty(Request.Params["User_Password"].ToString()))
            {
                modelUser.Password = Request.Params["User_Password"].ToString();
            }
            else
            {
                modelUser.Password = "";
            }
            modelUser.Mobile = Request.Params["User_Mobile"].ToString();
            modelUser.Email = Request.Params["User_Email"].ToString();
            modelUser.QQ = Request.Params["User_QQ"].ToString();
            modelUser.Zipcode = Request.Params["User_Zipcode"].ToString();
            modelUser.Gender = (EGender)int.Parse(Request.Form["Gender"].ToString());
            modelUser.State = (EState)int.Parse(Request.Form["State"].ToString());
            modelUser.Location = Request.Params["User_Location"].ToString();
            #endregion
            model.modelUser = modelUser;
            model.UserId = bllUserAccount.UserAccountAddEdit(modelUser);

            if (model.DataId == Guid.Empty)
            {
                //权限
                var UserRole = new VmUserRole()
                {
                    UserId = model.UserId,
                    RoleId = new Guid(ConfigurationManager.AppSettings["RolePlannerId"].ToString())
                };
                bllUserAccount.UserRoleAdd(UserRole);
            }

            model.LevelId = Request.Params["LevelId"].ToGuid(Guid.Empty);
            model.LevelName = XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.GetDataById("PlannerLevel", model.LevelId).Name;

            model.QualificationsId = Request.Params["QualificationsId"].ToGuid(Guid.Empty);
            model.QualificationsName = XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.GetDataById("PlannerQualifications", model.QualificationsId).Name;

            returnId = PlannerAddEdit(model);
            if (returnId == Guid.Empty)
            {
                return Json(new { success = false, Id = returnId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = returnId, errors = GetErrors() });
            }
        }
        #endregion
        #endregion
        #region 功能
        /// <summary>
        /// 规划师创建或编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid PlannerAddEdit(VmPlannerEdit model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Planners.Planner>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Planners.Planner>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 获取规划师对象
        /// </summary>
        /// <param name="PlannerId"></param>
        /// <returns></returns>
        public VmPlannerEdit GetPlanner(Guid PlannerId)
        {
            var entity = new Planner();
            var service = new GetEntityByIdService<Planner>(PlannerId);
            entity = service.Invoke();
            return entity.CreateViewModel<Planner, VmPlannerEdit>();
        }
        #endregion
    }
}