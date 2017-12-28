using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

using System.Web.Http;
using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Assessment;
using XZMY.Manage.Model.ServiceModel.Order;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Auth.Models;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Web.Controllers.Course;
using XZMY.Manage.Web.Controllers.Customers;
using XZMY.Manage.Web.Controllers.Planners;
using XZMY.Manage.Web.Controllers.Program;
using XZMY.Manage.Web.Controllers.Project;
using XZMY.Manage.Web.Controllers.SiteSetting;
using XZMY.Manage.Web.Controllers.Sys;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using T2M.Common.Utils.ADONET.SQLServer;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    /// <summary>
    /// 规划接口
    /// </summary>
    public class APlanController : ApiController
    {
        


        #region 获取指定规划
        
        /// <summary>
         /// 获取全部活动模板
         /// </summary>
         /// <returns></returns>
        private List<VmProjectTemplate> GetListProjectTemplate()
        {
            var service = new CustomSearchWithPaginationService<ProjectTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<ProjectTemplate>>()
                {
                    new CustomConditionPlus<ProjectTemplate>()
                    {
                        Value = "",
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectTemplate, object>>[] { x => x.Name}
                    }
                },
                SortMember = new Expression<Func<ProjectTemplate, object>>[] { x => x.CreatedTime }
            };
            var result = service.Invoke();
            List<VmProjectTemplate> modelList = new List<VmProjectTemplate>();
            VmProjectTemplate model;
            foreach (var m in result.Results)
            {
                model = m.CreateViewModel<ProjectTemplate, VmProjectTemplate>();
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// 获取全部课程模板
        /// </summary>
        /// <returns></returns>
        private List<VmCourseTemplate> GetListCourseTemplate()
        {
            var service = new CustomSearchWithPaginationService<CourseTemplate>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<CourseTemplate>>()
                {
                    new CustomConditionPlus<CourseTemplate>()
                    {
                        Value = "",
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<CourseTemplate, object>>[] { x => x.Name}
                    }
                },
                SortMember = new Expression<Func<CourseTemplate, object>>[] { x => x.CreatedTime }
            };
            var result = service.Invoke();
            List<VmCourseTemplate> modelList = new List<VmCourseTemplate>();
            VmCourseTemplate model;
            foreach (var m in result.Results)
            {
                model = m.CreateViewModel<CourseTemplate, VmCourseTemplate>();
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        /// 获取全部年级升学
        /// </summary>
        /// <returns></returns>
        private List<SmPlanningNote> GetListPlanningNote()
        {
            var service = new CustomSearchWithPaginationService<PlanningNote>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<PlanningNote>>()
                {
                    new CustomConditionPlus<PlanningNote>()
                    {
                        Value = "",
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<PlanningNote, object>>[] { x => x.Grade}
                    }
                },
                SortMember = new Expression<Func<PlanningNote, object>>[] { x => x.Sort }
            };
            var result = service.Invoke();
            List<SmPlanningNote> modelList = new List<SmPlanningNote>();
            SmPlanningNote model;
            foreach (var m in result.Results)
            {
                model = m.CreateViewModel<PlanningNote, SmPlanningNote>();
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        /// 获取全部地区表中全部国家数据
        /// </summary>
        /// <returns></returns>
        private List<VmLocation> GetListCountry()
        {
            var service = new CustomSearchWithPaginationService<Location>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<Location>>()
                {
                    new CustomConditionPlus<Location>()
                    {
                        Value = 1,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Location, object>>[] { x => x.Level}
                    }
                },
                SortMember = new Expression<Func<Location, object>>[] { x => x.Sort }
            };
            var result = service.Invoke();
            List<VmLocation> modelList = new List<VmLocation>();
            VmLocation model;
            foreach (var m in result.Results)
            {
                model = m.CreateViewModel<Location, VmLocation>();
                modelList.Add(model);
            }
            return modelList;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <returns></returns>
        private List<SmStudentPlan> GetListStudentPlan(Guid PlanRecordId)
        {
            PlanController bllPlan = new PlanController();
            var list = bllPlan.GetStudentPlanList(PlanRecordId);
            foreach (var m in list)
            {
                m.listStudentPlanProgram = GetListStudentPlanProgram(m.DataId);
            }
            return list;
        }
        private List<SmStudentPlanProgram> GetListStudentPlanProgram(Guid StudentPlanId)
        {
            var service = new CustomSearchWithPaginationService<StudentPlanProgram>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<StudentPlanProgram>>()
                {
                    new CustomConditionPlus<StudentPlanProgram>()
                    {
                        Value = StudentPlanId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.StudentPlanId}
                    }
                },
                SortMember = new Expression<Func<StudentPlanProgram, object>>[] { x => x.Name }
            };
            var result = service.Invoke();
            List<SmStudentPlanProgram> modelList = new List<SmStudentPlanProgram>();
            foreach (var m in result.Results)
            {
                SmStudentPlanProgram modelStudentPlanProgram = m.CreateViewModel<Model.DataModel.Plan.StudentPlanProgram, SmStudentPlanProgram>();
                modelList.Add(modelStudentPlanProgram);
            }
            return modelList;
        }
        /// <summary>
        /// 获取课程订单
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        private List<SmCourseOrderCreate> GetListCourseOrder(Guid StudentId)
        {
            var service = new CustomSearchWithPaginationService<OrderCourse>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<OrderCourse>>()
                {
                    new CustomConditionPlus<OrderCourse>()
                    {
                        Value = StudentId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OrderCourse, object>>[] { x => x.StudentId}
                    }
                },
                SortMember = new Expression<Func<OrderCourse, object>>[] { x => x.CreatedTime }
            };
            service.CustomConditions.Add(new CustomConditionPlus<OrderCourse>
            {
                Value = EOrderProcessState.已取消,
                Operation = SqlOperation.NotEquals,
                Member = new Expression<Func<OrderCourse, object>>[] { x => x.ProcessState }
            });


            var result = service.Invoke();
            List<SmCourseOrderCreate> modelList = new List<SmCourseOrderCreate>();
            foreach (var m in result.Results)
            {
                SmCourseOrderCreate modelOrderCourse = m.CreateViewModel<OrderCourse, SmCourseOrderCreate>();
                modelOrderCourse.ModelCourse = GetVmCourse(modelOrderCourse.CourseId);
                modelList.Add(modelOrderCourse);
            }
            return modelList;
        }
        /// <summary>
        /// 获取活动订单
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        private List<SmProjectOrderCreate> GetListProjectOrder(Guid StudentId)
        {
            var service = new CustomSearchWithPaginationService<OrderProject>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<OrderProject>>()
                {
                    new CustomConditionPlus<OrderProject>()
                    {
                        Value = StudentId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OrderProject, object>>[] { x => x.StudentId}
                    }
                },
                SortMember = new Expression<Func<OrderProject, object>>[] { x => x.CreatedTime }
            };
            service.CustomConditions.Add(new CustomConditionPlus<OrderProject>
            {
                Value = EOrderProcessState.已取消,
                Operation = SqlOperation.NotEquals,
                Member = new Expression<Func<OrderProject, object>>[] { x => x.ProcessState }
            });


            var result = service.Invoke();
            List<SmProjectOrderCreate> modelList = new List<SmProjectOrderCreate>();
            foreach (var m in result.Results)
            {
                SmProjectOrderCreate modelOrderProject = m.CreateViewModel<OrderProject, SmProjectOrderCreate>();
                modelOrderProject.ModelProject = GetVmProject(modelOrderProject.ProjectId);
                modelList.Add(modelOrderProject);
            }
            return modelList;
        }
        /// <summary>
        /// 获取课程对象
        /// </summary>
        /// <param name="CourseId">课程ID</param>
        /// <returns></returns>
        private VmCourseEdit GetVmCourse(Guid CourseId)
        {
            VmCourseEdit model = new VmCourseEdit();
            var entity = new Model.DataModel.Courses.Course();
            var service = new GetEntityByIdService<Model.DataModel.Courses.Course>(CourseId);
            entity = service.Invoke();
            model = entity.CreateViewModel<Model.DataModel.Courses.Course, VmCourseEdit>();
            return model;
        }
        /// <summary>
        /// 获取活动对象
        /// </summary>
        /// <param name="ProjectId">活动ID</param>
        /// <returns></returns>
        private VmProjectEdit GetVmProject(Guid ProjectId)
        {
            VmProjectEdit model = new VmProjectEdit();
            var entity = new Model.DataModel.Project.Project();
            var service = new GetEntityByIdService<Model.DataModel.Project.Project>(ProjectId);
            entity = service.Invoke();
            model = entity.CreateViewModel<Model.DataModel.Project.Project, VmProjectEdit>();
            return model;
        }
        #endregion
       
        /// <summary>
        /// 获取规划年级的活动列表
        /// </summary>
        /// <param name="modelQuery">条件</param>
        /// <returns></returns>
        private List<SmStudentPlanProgram> GetStudentPlanProgramList(PlanRecordStudentGradeProgramQuery modelQuery)
        {
            List<SmStudentPlanProgram> list = new List<SmStudentPlanProgram>();
            var service = new CustomSearchWithPaginationService<StudentPlanProgram>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<StudentPlanProgram>>()
                {
                    new CustomConditionPlus<StudentPlanProgram>()
                    {
                        Value = modelQuery.PlanStudentGradeId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.StudentPlanId}
                    }
                },
                SortMember = new Expression<Func<StudentPlanProgram, object>>[] { x => x.CreatedTime }
            };
            if (!String.IsNullOrEmpty(modelQuery.ItemName))
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = modelQuery.ItemName,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.ItemName }
                });
            }
            #region 非素质能力
            if (modelQuery.IsQuality==2)
            {
                //素质能力 表示为课程
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = 2,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.Type }
                });
            }
            else
            {
                //非素质能力 表示为活动
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = 1,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.Type }
                });
            }
            #endregion
            var result = service.Invoke();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentPlanProgram, SmStudentPlanProgram>());
            }
            return list;
        }
        
        
        /// <summary>
        /// 获取规划年级对象
        /// </summary>
        /// <param name="PlanStudentGradeId"></param>
        /// <returns></returns>
        private SmStudentPlan PlanRecordStudentGradeModel(Guid PlanStudentGradeId)
        {
            var service = new CustomSearchWithPaginationService<StudentPlan>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<StudentPlan>>()
                {
                    new CustomConditionPlus<StudentPlan>()
                    {
                        Value = PlanStudentGradeId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentPlan, object>>[] { x => x.DataId }
                    }
                }
            };
            var result = service.Invoke();
            var list = result.Results;
            SmStudentPlan model = new SmStudentPlan();
            if (list.Count > 0)
            {
                model = list[0].CreateViewModel<StudentPlan, SmStudentPlan>();
            }
            return model;
        }
        /// <summary>
        /// 修改规划年级对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Boolean PlanRecordStudentGradeEdit(SmStudentPlan model)
        {
            var handler = new BaseModifyHandler<StudentPlan>(model);
            var res = handler.Invoke();
            if (res.Code != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        #region 公布接口
        /// <summary>
        /// 获取指定规划
        /// </summary>
        /// <param name="id">规划ID</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, SmPlanRecord>> GetPlanRecord([FromBody]GetPlanRecordQuery modelQuery)
        {
            LocationController bllLoaction = new LocationController();
            ProjectTemplateController bllProjectTemplate = new ProjectTemplateController();
            CourseTemplateController bllCourseTemplate = new CourseTemplateController();
            PlanningNoteController bllPlaningNote = new PlanningNoteController();
            PlanController bllPlan = new PlanController();
            SmPlanRecord model = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId);
            List<SmStudentPlan> listStudentPlan = bllPlan.GetStudentPlanList(model.DataId);
            foreach (var m in listStudentPlan)
            {
                m.listStudentPlanProgram = bllPlan.StudentPlanProgramGetList(new SmStudentPlanProgram() { StudentPlanId = m.DataId });
            }
            model.listStudentPlan = listStudentPlan;
            //model.listOrderCourse = GetListCourseOrder(model.StudentId);
            //model.listOrderProject = GetListProjectOrder(model.StudentId);
            //model.listCountry =  GetListCountry();
            model.listSmPlanningNote = bllPlaningNote.GetPlanningNoteList("", "", "");
            model.listCourseTemplate = bllCourseTemplate.GetListVmCourseTemplate("", "", "");
            model.listProjectTemplate = bllProjectTemplate.GetListVmProjectTemplate("", "", "");

            var res = new Dictionary<string, SmPlanRecord>();
            res.Add("SmPlanRecord", model);
            return new ApiHandlerInvokeResult<Dictionary<string, SmPlanRecord>>()
            {
                Output = res,
                Success = true
            };
        }
        /// <summary>
        /// 获取全部活动课程的模板数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, List<V_ProjectCourseTemplateList>>> GetTemplate()
        {
            PlanController bllPlan = new PlanController();
            var listTemplate = bllPlan.GetListProjectCourseTemplate(new V_ProjectCourseTemplateList() { });
            var res = new Dictionary<string, List<V_ProjectCourseTemplateList>>();
            res.Add("V_ProjectCourseTemplateList", listTemplate);
            try
            {
                return new ApiHandlerInvokeResult<Dictionary<string, List<V_ProjectCourseTemplateList>>>()
                {
                    Output = res,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Dictionary<string, List<V_ProjectCourseTemplateList>>>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }
        /// <summary>
        /// 获取一个指定完整规划信息
        /// </summary>
        /// <param name="planQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, List<PlanningNote>>> GetPlanningNote(Model.Utils.PlanQuery planQuery)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CommandType cmdType = CommandType.StoredProcedure;
            string cmdText = "P_PlanningNote";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Grade",planQuery.Grade),
                new SqlParameter("@SchoolType",planQuery.SchoolType),
                new SqlParameter("@AbroadGrade",planQuery.GradeAbroad),
                new SqlParameter("@GeneralBudget",planQuery.GeneralBudget)
            };
            try
            {
                List<PlanningNote> listPN = new List<PlanningNote>();
                using (SqlDataReader reader = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteReader(connString, cmdType, cmdText, sqlParams))
                {
                    while (reader.Read())
                    {
                        //reader.ToModel<PlanningNote>();
                        PlanningNote model = new PlanningNote();
                        //model.Id = (Guid)reader["Id"];
                        model.SchoolPlace = reader["SchoolPlace"].ToString();
                        model.SchoolType = reader["SchoolType"].ToString();
                        model.SchoolTypeId = (Int32)reader["SchoolTypeId"];
                        model.Sort = (Int32)reader["Sort"];
                        model.Fee = (Decimal)reader["Fee"];
                        model.Grade = reader["Grade"].ToString();
                        model.EnglishScore = (Decimal)reader["EnglishScore"];
                        model.LearnScore = (Decimal)reader["LearnScore"];
                        model.QualityScore = (Decimal)reader["QualityScore"];
                        model.AddEnglishScore = (Decimal)reader["AddEnglishScore"];
                        model.AddLearnScore = (Decimal)reader["AddLearnScore"];
                        model.AddQualityScore = (Decimal)reader["AddQualityScore"];
                        model.CreatedTime = (DateTime)reader["CreatedTime"];
                        //model.CreatorId = (Guid)reader["CreatorId"];
                        //model.CreatorName = reader["CreatorName"].ToString();
                        //model.ModifiedTime = (DateTime)reader["ModifiedTime"];
                        //model.ModifierId = (Guid)reader["ModifierId"];
                        //model.ModifierName = reader["ModifierName"].ToString();
                        listPN.Add(model);
                    }
                }
                var res = new Dictionary<string, List<PlanningNote>>();
                res.Add("PlanningNote", listPN);


                return new ApiHandlerInvokeResult<Dictionary<string, List<PlanningNote>>>()
                {
                    Output = res,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Dictionary<string, List<PlanningNote>>>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }

        }

        #region 留学规划第1步
        /// <summary>
        /// 留学意向获取数据
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, SmPlanIntention>> GetPlanIntention(GetPlanRecordQuery modelId)
        {
            PlanController bllPlan = new PlanController();
            SmPlanIntention model = new SmPlanIntention();
            var modelPlanRecord = bllPlan.GetSmPlanRecord(modelId.PlanRecordId);
            if (modelPlanRecord.DataId != Guid.Empty)
            {
                model.PlanRecordId = modelPlanRecord.DataId;
                model.StudentId = modelPlanRecord.StudentId;
                model.TargetCountryId = modelPlanRecord.TargetCountryId;
                model.TargetCountryName = modelPlanRecord.TargetCountryName;
                model.EducationId = modelPlanRecord.EducationId;
                model.EducationName = modelPlanRecord.EducationName;
                model.GoAbroadEducationId = modelPlanRecord.GoAbroadEducationId;
                model.GoAbroadEducationIName = modelPlanRecord.GoAbroadEducationIName;
                model.Fee = modelPlanRecord.Fee;
                model.FeeInterval = modelPlanRecord.FeeInterval;
                model.IntentionalSchoolTop = modelPlanRecord.IntentionalSchoolTop;
                model.IntentionalSchoolName = modelPlanRecord.IntentionalSchoolName;
                model.GradeRanking = modelPlanRecord.GradeRanking;
                model.SchoolName = modelPlanRecord.School;
                model.currentSchoolType = modelPlanRecord.currentSchoolType;
                model.MajorName = modelPlanRecord.major;
                model.GraduationDate = modelPlanRecord.GraduationDate;
                model.LearnScore = modelPlanRecord.LearnScore;
                model.listLearn = modelPlanRecord.listLearn;
                model.listEnglishItem = modelPlanRecord.listEnglishItem;
                model.listEnglishOtherItem = modelPlanRecord.listEnglishOtherItem;
                model.SchoolTypeId = modelPlanRecord.SchoolTypeId.ToString();
              
            }
            var res = new Dictionary<string, SmPlanIntention>();
            res.Add("PlanIntention", model);
            return new ApiHandlerInvokeResult<Dictionary<string, SmPlanIntention>>()
            {
                Output = res,
                Success = true
            };
        }
        /// <summary>
        /// 留学规划_留学意向 第1步提交数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> SetPlanIntention([FromBody]SmPlanIntention model)
        {
            SchoolTypeController bllSchoolType = new SchoolTypeController();
            StudentController bllStudent = new StudentController();
            PlanController bllPlan = new PlanController();
            SchoolLevelController bllSchoolLevel = new SchoolLevelController();
            StudentTotalScoreController bllStudentTotalScore = new StudentTotalScoreController();
            SmPlanRecord modelPlanRecord = new SmPlanRecord();
            if (model.PlanRecordId != Guid.Empty)
            {
                modelPlanRecord = bllPlan.GetSmPlanRecord(model.PlanRecordId);
            }
            modelPlanRecord.DataId = model   .PlanRecordId;
            modelPlanRecord.StudentId = model.StudentId;

            modelPlanRecord.TargetCountryId = model.TargetCountryId;
            modelPlanRecord.TargetCountryName = model.TargetCountryName;
            modelPlanRecord.EducationId = model.EducationId;
            modelPlanRecord.EducationName = model.EducationName;
            modelPlanRecord.GoAbroadEducationId = model.GoAbroadEducationId;
            modelPlanRecord.GoAbroadEducationIName = model.GoAbroadEducationIName;
            modelPlanRecord.Fee = model.Fee;
            modelPlanRecord.IntentionalSchoolTop = model.IntentionalSchoolTop;
            modelPlanRecord.IntentionalSchoolName = model.IntentionalSchoolName;
            modelPlanRecord.currentSchoolType = model.currentSchoolType;
            modelPlanRecord.GradeRanking = model.GradeRanking;
            modelPlanRecord.School = model.SchoolName;
            modelPlanRecord.major = model.MajorName;
            modelPlanRecord.GraduationDate = model.GraduationDate;

            modelPlanRecord.FeeInterval = model.FeeInterval;
            modelPlanRecord.listEnglishItem = model.listEnglishItem;
            modelPlanRecord.listEnglishOtherItem = model.listEnglishOtherItem;
            if ((int)modelPlanRecord.Route == 0)
            {
                modelPlanRecord.Route = ERoute.稳妥路线;
            }
            #region 意向学校类型 普通学校  重点学校



            if (model.SchoolTypeId.ToGuid(Guid.Empty) == Guid.Empty)
            {
                int SchoolTypeTotalCount = 0;
                var listTempSchoolType = bllSchoolType.GetList(new Model.ViewModel.School.VmSchoolType() { State = EState.启用 }, out SchoolTypeTotalCount);
                if (listTempSchoolType.Count > 0)
                {
                    modelPlanRecord.SchoolTypeId = listTempSchoolType[0].DataId;
                }
            }
            else
            {
                modelPlanRecord.SchoolTypeId = model.SchoolTypeId.ToGuid(Guid.Empty);
            }

            #endregion

            #region 在读学校等级
            if (model.currentSchoolType == Guid.Empty)
            {
                var listSchoolLevel = bllSchoolLevel.GetList();
                if (listSchoolLevel.Count > 0)
                {
                    modelPlanRecord.currentSchoolType = listSchoolLevel[0].DataId;
                }
            }
            #endregion
            modelPlanRecord.listLearn = model.listLearn ?? String.Empty;

            
            #region 学术成绩

            modelPlanRecord.LearnScore = model.LearnScore;
            //currentSchoolTypeController bllcurrentSchoolType = new currentSchoolTypeController();
            //int TotalCount = 0;
            //decimal PlanLearnScore = 0;
            //var listcurrentSchoolType = bllcurrentSchoolType.GetList(new Model.ViewModel.School.VmcurrentSchoolType() { }, out TotalCount);
            //foreach (var mschooltype in listcurrentSchoolType)
            //{
            //    if (mschooltype.currentSchoolTypeId == modelPlanRecord.currentSchoolType)
            //    {
            //        PlanLearnScore = modelPlanRecord.LearnScore * mschooltype.coefficient;
            //    }
            //}
            ////添加学术获奖项
            //PlanLearnScore += modelPlanRecord.listLearn.Split(",").Length;
            //modelPlanRecord.LearnScore = PlanLearnScore;
            #endregion
            #region 英语成绩
            decimal EnglishScore = 0M;
            if (model.listEnglishItem != null)
            {
                //ItemName:,ListeningScore:,VerbalScore:,ReadingScore:,WritingScore:,ItemTotalScore:|
                string[] strlistEnglishItem = model.listEnglishItem.Split("|");
                foreach (var oItem in strlistEnglishItem)
                {

                    if (oItem.IndexOf("雅思") > -1)
                    {
                        //IElTS
                        string[] oItemValue = oItem.Split(",");
                        foreach (var vItemValue in oItemValue)
                        {
                            var vvItem = vItemValue.Split(":");
                            if (vvItem.Length == 2)
                            {
                                if (vvItem[0].Trim() == "ItemTotalScore")
                                {
                                    EnglishScore = GetTOEFLScore(vvItem[1].ToDecimal(0));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //托福 或者 别的
                        string[] oItemValue = oItem.Split(",");
                        if (oItemValue.Length >= 6)
                        {
                            var ItemNameItem = oItemValue[5].Split(":");
                            if (ItemNameItem.Length == 2)
                            {
                                if (ItemNameItem[0].Trim() == "ItemTotalScore")
                                {
                                    EnglishScore = ItemNameItem[1].ToDecimal(0);
                                }
                            }
                        }
                    }




                }
            }
            modelPlanRecord.EnglishScore = EnglishScore;

            
            #endregion
            #region 英语和 学术的最大分值百分比设置分数
            //设置最大分值
            var listTotalScore = bllStudentTotalScore.GetList();
            if (listTotalScore.Count > 0)
            {
                modelPlanRecord.EnglishScore = (modelPlanRecord.EnglishScore / listTotalScore[0].EnglishScore) * 100;
                modelPlanRecord.LearnScore = (modelPlanRecord.LearnScore / listTotalScore[0].LearnScore) * 100;
            }
            #endregion
            #region 修改学生信息
            var modelStudent = bllStudent.GetStudent(model.StudentId);
            modelPlanRecord.StudentName = bllStudent.GetStudent(model.StudentId).Name;

            modelStudent.EnglishScore = modelPlanRecord.EnglishScore;
            modelStudent.LearnScore = modelPlanRecord.LearnScore;
            bllStudent.StudentAddEdit(modelStudent);
            #endregion
            Guid PlanRecordId = bllPlan.CreateEditPlanRecord(modelPlanRecord);
            #region 更新学生申请集中的留学意向
            int StudentApplyIntentionCount = 0;
            var listStudentApplyIntention = bllStudent.GetListStudentApplyIntention(new Model.ViewModel.Members.VmStudentApply_Intention() { StudentId = modelPlanRecord.StudentId }, out StudentApplyIntentionCount);
            VmStudentApply_Intention modelStudentApplyIntention = new VmStudentApply_Intention();
            if (listStudentApplyIntention.Count > 0)
            {
                modelStudentApplyIntention = listStudentApplyIntention[0];
            }
            else
            {
                modelStudentApplyIntention.DataId = Guid.Empty;
                modelStudentApplyIntention.StudentId = modelPlanRecord.StudentId;
            }
            modelStudentApplyIntention.TargetCountryId = modelPlanRecord.TargetCountryId;
            modelStudentApplyIntention.TargetCountryName = modelPlanRecord.TargetCountryName;
            modelStudentApplyIntention.Education = modelPlanRecord.EducationName;
            modelStudentApplyIntention.GoAbroadEducationId = modelPlanRecord.GoAbroadEducationId;
            modelStudentApplyIntention.GoAbroadEducationIName = modelPlanRecord.GoAbroadEducationIName;
            if (modelPlanRecord.Fee <= 100000)
            {
                modelStudentApplyIntention.BudgetCost = 1;
            }
            else if (modelPlanRecord.Fee <= 200000)
            {
                modelStudentApplyIntention.BudgetCost = 2;
            }
            else if (modelPlanRecord.Fee <= 300000)
            {
                modelStudentApplyIntention.BudgetCost = 3;
            }
            else if (modelPlanRecord.Fee <= 400000)
            {
                modelStudentApplyIntention.BudgetCost = 4;
            }
            else
            {
                modelStudentApplyIntention.BudgetCost = 5;
            }
            modelStudentApplyIntention.IntentionalSchoolTop = modelPlanRecord.IntentionalSchoolTop;
            modelStudentApplyIntention.IntentionalSchoolName = modelPlanRecord.IntentionalSchoolName;
            modelStudentApplyIntention.major = modelPlanRecord.major;
            modelStudentApplyIntention.ImmigrationProgram = modelPlanRecord.ImmigrationProgram;
            modelStudentApplyIntention.SchoolTypeId = modelPlanRecord.SchoolTypeId;
            modelStudentApplyIntention.SchoolTypeName = bllSchoolType.GetModel(modelPlanRecord.SchoolTypeId).Name;
            bllStudent.CreateEditStudentApplyIntention(modelStudentApplyIntention);
            #endregion
            if (PlanRecordId == Guid.Empty)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = Guid.Empty,
                    Success = false
                };
            }
            else
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = (Guid)PlanRecordId,
                    Success = true
                };
            }
        }
        /// <summary>
        /// 通过雅思成绩获取托福成绩
        /// </summary>
        /// <param name="IElTSBand"></param>
        /// <returns></returns>
        public decimal GetTOEFLScore(decimal IElTSBand)
        {
            decimal TOEFLScore = 0M;
            if (IElTSBand >= 0M && IElTSBand <= 4M)
            {
                TOEFLScore = IElTSBand * 7.75M;
            }
            else if (IElTSBand > 4M && IElTSBand <= 4.5M)
            {
                TOEFLScore = IElTSBand * 7.55M;
            }
            else if (IElTSBand > 4.5M && IElTSBand <= 5M)
            {
                TOEFLScore = IElTSBand * 9M;
            }
            else if (IElTSBand > 5M && IElTSBand <= 5.5M)
            {
                TOEFLScore = IElTSBand * 10.72M;
            }
            else if (IElTSBand > 5.5M && IElTSBand <= 6M)
            {
                TOEFLScore = IElTSBand * 13M;
            }
            else if (IElTSBand > 6M && IElTSBand <= 6.5M)
            {
                TOEFLScore = IElTSBand * 14.3M;
            }
            else if (IElTSBand > 6.5M && IElTSBand <= 7M)
            {
                TOEFLScore = IElTSBand * 14.42M;
            }
            else if (IElTSBand > 7M && IElTSBand <= 7.5M)
            {
                TOEFLScore = IElTSBand * 14.53M;
            }
            else if (IElTSBand > 7.5M && IElTSBand <= 8M)
            {
                TOEFLScore = IElTSBand * 14.25M;
            }
            else if (IElTSBand > 8M && IElTSBand <= 8.5M)
            {
                TOEFLScore = IElTSBand * 13.76M;
            }
            else if (IElTSBand > 8.5M && IElTSBand <= 9M)
            {
                TOEFLScore = IElTSBand * 13.33M;
            }
            else
            {
                TOEFLScore = IElTSBand * 13.33M;
            }
            return TOEFLScore;
        }
        #endregion
        /// <summary>
        /// 留学规划_评估中心提交答案未完成Pass 第2步提交数据
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> SetPlanAssessmentAnswerPass([FromBody]SetPlanAssessmentAnswerPassQuery modelQuery)
        {
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            PlanController bllPlan = new PlanController();
            var modelSmPlanRecord = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId);
            modelSmPlanRecord.JsonQualityScore = "";
            modelSmPlanRecord.QualityScore = bllPlanningNote.GetPlanningNote(modelSmPlanRecord.EducationId).QualityScore;
            modelSmPlanRecord.QualityScore = 50;
            Guid returnId = bllPlan.CreateEditPlanRecord(modelSmPlanRecord);

            if (returnId == Guid.Empty)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = Guid.Empty,
                    Success = false
                };
            }
            else
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = (Guid)returnId,
                    Success = true
                };
            }
           
        }
        /// <summary>
        /// 留学规划_评估中心提交答案完成 第2步提交数据
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <param name="listModel">答案列表</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> SetPlanAssessmentAnswerIds([FromBody]SetPlanAssessmentAnswerQuery modelQuery)
        {
            modelQuery.AnswerIds = modelQuery.AnswerIds ?? "";
            string[] AnswerIds = modelQuery.AnswerIds.Split(",");
            List<SmAssessmentAnswer> listModel = new List<SmAssessmentAnswer>();

            foreach (var AnswerId in AnswerIds)
            {
                if (AnswerId.ToGuid(Guid.Empty) != Guid.Empty)
                {
                    listModel.Add(new SmAssessmentAnswer()
                    {
                        QuestionId = Guid.Empty.ToString(),
                        QuestionTitle = "",
                        QuestionSort = "0",
                        AnswerId = AnswerId.ToGuid(Guid.Empty).ToString(),
                        AnswerDescription = ""
                    });
                }
            }
            modelQuery.listModel = listModel;
            return SetPlanAssessmentAnswer(modelQuery);
        }
        /// <summary>
        /// 留学规划_评估中心提交答案完成 第2步提交数据
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <param name="listModel">答案列表</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> SetPlanAssessmentAnswer([FromBody]SetPlanAssessmentAnswerQuery modelQuery)
        {
            PlanRecordAssessmentController bllPlanRecordAssessment = new PlanRecordAssessmentController();
            PlanRecordAssessmentAnswersController bllPlanRecordAssessmentAnswers = new PlanRecordAssessmentAnswersController();
            PlanController bllPlan = new PlanController();

            ScoresController bllScorse = new ScoresController();
            VmSearchBase model = new VmSearchBase();
            model.PageIndex = 1;
            model.PageSize = 999999999;
            var listScores = bllScorse.GetListScores(model, "AssessmentAnswer");
            var modelSmPlanRecord = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId);
            //问答卷总分数
            decimal QualityScore = 0m;
            //创建一个评估答题
            Model.ViewModel.Plan.VmPlanRecord_Assessment modelAssessment = new Model.ViewModel.Plan.VmPlanRecord_Assessment();
            modelAssessment.PlanRecordId = modelQuery.PlanRecordId;
            modelAssessment.StudentId = modelSmPlanRecord.StudentId;
            modelAssessment.Score = 0;
            modelAssessment.AssessmentTime = DateTime.Now;
            modelAssessment.DataId = bllPlanRecordAssessment.CreateEdit(modelAssessment);

            List<Scores> listAllScores = new List<Scores>();
            //循环创建答题的答案
            foreach (var modelAssessmentAnswer in modelQuery.listModel)
            {
                if (modelAssessmentAnswer.AnswerId.ToGuid(Guid.Empty) != Guid.Empty)
                {
                    var listAnswerScores = listScores.Where(x => x.SourceId == modelAssessmentAnswer.AnswerId.ToGuid(Guid.Empty)).ToList();
                    StringBuilder sbScoreContent = new StringBuilder();
                    decimal mQualityScore = 0M;
                    foreach (var mAnswerScores in listAnswerScores)
                    {
                        mQualityScore += mAnswerScores.ScoreValue;
                        sbScoreContent.AppendFormat("{0}:{1},", mAnswerScores.ScoreItemsName, mAnswerScores.ScoreValue);
                        #region 累计各种分值
                        if (listAllScores.Where(x => x.ScoreItemsName == mAnswerScores.ScoreItemsName).ToList().Count > 0)
                        {
                            foreach (var ms in listAllScores)
                            {
                                if (ms.ScoreItemsName == mAnswerScores.ScoreItemsName)
                                {
                                    ms.ScoreValue += mAnswerScores.ScoreValue;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            listAllScores.Add(new Scores()
                            {
                                ScoreItemsName = mAnswerScores.ScoreItemsName,
                                ScoreValue = mAnswerScores.ScoreValue
                            });
                        }
                        #endregion
                    }

                    bllPlanRecordAssessmentAnswers.CreateEdit(new Model.ViewModel.Plan.VmPlanRecord_AssessmentAnswers
                    {
                        PlanRecordId = modelQuery.PlanRecordId,
                        AssessmentId = modelAssessment.DataId,
                        AnswersId = modelAssessmentAnswer.AnswerId.ToGuid(Guid.Empty),
                        QualityScore = mQualityScore,
                        ScoreContent = sbScoreContent.ToString()
                    });
                    QualityScore += mQualityScore;
                }
            }
            //修改答题的总分值
            modelAssessment.Score = QualityScore;
            bllPlanRecordAssessment.CreateEdit(modelAssessment);
            
            //修改规划的分值
            modelSmPlanRecord.QualityScore = QualityScore;


            #region 修改学生的当前成绩
            if (modelSmPlanRecord.StudentId != Guid.Empty)
            {
                StudentController bllStudent = new StudentController();
                var modelStudent = bllStudent.GetVmStudent(modelSmPlanRecord.StudentId);

                StudentTotalScoreController bllStudentTotalScore = new StudentTotalScoreController();
                var listTotalScore = bllStudentTotalScore.GetList();
                if (listTotalScore.Count > 0)
                {
                    modelSmPlanRecord.QualityScore = (modelSmPlanRecord.QualityScore / listTotalScore[0].QualityScore) * 100;
                }


                modelStudent.QualityScore = modelSmPlanRecord.QualityScore;
                bllStudent.StudentAddEdit(modelStudent);
            }
            #endregion
            Guid returnId = bllPlan.CreateEditPlanRecord(modelSmPlanRecord);
            if (returnId == Guid.Empty)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = Guid.Empty,
                    Success = false
                };
            }
            else
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = (Guid)returnId,
                    Success = true
                };
            }
        }
        /// <summary>
        /// 留学规划_规划 第3步 初次规划获取规划信息 规划年级和活动和课程
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <param name="Route">路线 学霸路线 = 1, 稳妥路线 = 2, 经济路线 = 3</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> SetPlanCreatePlan([FromBody]SetPlanCreatePlanQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            if (modelQuery.Route == 0)
            {
                var modelPlanRecord = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId);
                
                //重新规划 年级和 活动
                bllPlan.PlanStudnetGrade(modelQuery.PlanRecordId);
                bllPlan.PlanStudentPlanProgram(modelQuery.PlanRecordId);
            }
            else
            {
                var modelPlanRecord = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId);
                modelPlanRecord.Route = (ERoute)modelQuery.Route;
                bllPlan.CreateEditPlanRecord(modelPlanRecord);
                bllPlan.PlanStudentPlanProgram(modelQuery.PlanRecordId);
            }
            return new ApiHandlerInvokeResult<Guid>()
            {
                Output = (Guid)modelQuery.PlanRecordId,
                Success = true
            };
        }
        /// <summary>
        /// 留学规划_规划 第3步 获取指定规划的年级列表
        /// </summary>
        /// <param name="modelQuery">规划ID</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, List<SmWebStudentPlan>>> PlanRecordStudentGradeList([FromBody]GetPlanRecordQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            PlanningNoteController bllPlanningNote = new PlanningNoteController();

            var GradList = bllPlan.GetStudentPlanList(modelQuery.PlanRecordId);
            List<SmWebStudentPlan> list = new List<SmWebStudentPlan>();
            foreach (var m in GradList)
            {
                string GradeDescription = "";
               var listNote= bllPlanningNote.PlanningNoteGetList(new Model.ViewModel.Plan.VmPlanningNote() { Sort = m.Sort, SchoolType = m.SchoolType, SchoolPlace = m.SchoolPlace });
                if (listNote.Count > 0)
                {
                    GradeDescription = listNote[0].Description;
                }
                list.Add(new SmWebStudentPlan()
                {
                    Id = m.DataId,
                    // 规划年级名称 如 高中一年级
                    Name = m.Grade,
                    // 学校类型  普通学校、重点学校、国际学校
                    SchoolType = m.SchoolType,
                    SchoolPlace = m.SchoolPlace,
                    // 学校排序 从小到大
                    Sort = m.Sort,
                    GradeDescription = GradeDescription

                });
            }
            var res = new Dictionary<string, List<SmWebStudentPlan>>();
            res.Add("SmWebStudentPlan", list);
            return new ApiHandlerInvokeResult<Dictionary<string, List<SmWebStudentPlan>>>()
            {
                Output = res,
                Success = true
            };
        }
        /// <summary>
        /// 留学规划_规划 第3步 获取规划年级的活动列表
        /// </summary>
        /// <param name="modelQuery">条件</param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, List<SmWebStudentPlanProgram>>> PlanRecordStudentGradeProgramList([FromBody]PlanRecordStudentGradeProgramQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            ProjectTemplateController bllProjectTemplate = new ProjectTemplateController();
            CourseTemplateController bllCourseTemplate = new CourseTemplateController();
            ProgramOrderController bllProgramOrder = new ProgramOrderController();

            Guid StudentId = bllPlan.GetSmPlanRecord(modelQuery.PlanRecordId).StudentId;
            List<SmStudentPlanProgram> Programlist = bllPlan.StudentPlanProgramGetList(new SmStudentPlanProgram() { StudentPlanId = modelQuery.PlanStudentGradeId, Type = modelQuery.IsQuality, ItemName = modelQuery.ItemName });
            List<SmWebStudentPlanProgram> returnlist = new List<SmWebStudentPlanProgram>();
            SmWebStudentPlanProgram model = new SmWebStudentPlanProgram();
            foreach (var m in Programlist)
            {
                model = new SmWebStudentPlanProgram();
                //model.Id = m.Id;
                model.Name = m.Name;
                model.ProgramId = m.ProgramId;
               

                string strImage = "";
                string strDescription = "";
                string strItemName = "";
                Guid ItemID = Guid.Empty;
                int IsOrder = 0;
                Guid OrderId=Guid.Empty;
                if (m.Type == 1)
                {
                    ScoresController bllScores = new ScoresController();
                    var modelTemplate = bllProjectTemplate.GetVmProjectTemplate(m.ProgramId);
                    
                    var listScores = bllScores.GetListSourceID(modelTemplate.DataId);
                    listScores = listScores.OrderByDescending(x => x.ScoreValue).ToList();
                    foreach (var mScore in listScores)
                    {
                        if (modelTemplate.ScoreItemNames.IndexOf(mScore.ScoreItemsName) > -1 )
                        {
                            strItemName = mScore.ScoreItemsName;
                            break;
                        }
                    }
                    strImage = modelTemplate.PrctureOnly;
                    strDescription= String.Format("历练类型：{0},主办方：{1},历练成长：{2}", modelTemplate.ProjectTypeName, modelTemplate.Sponsor, modelTemplate.ScoreItemNames);
                    var modelProject = bllProjectTemplate.GetProject(m.ProgramId);
                    if (modelProject != null)
                    {
                        ItemID = modelProject.DataId;
                    }
                    int TotalCount=0;
                    var listProgramOrder = bllProgramOrder.GetList(new Model.ViewModel.Program.VmProgramOrder() { TemplateId = m.ProgramId, Type = m.Type, StudentId = StudentId }, out TotalCount);
                    if (listProgramOrder.Count > 0)
                    {
                        IsOrder = 1;
                        OrderId = listProgramOrder[0].OrderId;
                    }
                }
                else
                {
                    var modelTemplate = bllCourseTemplate.GetVmCourseTemplate(m.ProgramId);
                    strItemName = modelTemplate.ScoreItemNames;
                    strImage = modelTemplate.PrctureOnly;
                    strDescription = String.Format("历练类型：{0},主办方：{1},历练成长：{2}", modelTemplate.CourseTypeName, modelTemplate.Sponsor, modelTemplate.ScoreItemNames);
                    
                    var modelCourse = bllCourseTemplate.GetCourse(m.ProgramId);
                    if (modelCourse != null)
                    {
                        ItemID = modelCourse.DataId;
                    }
                    int TotalCount = 0;
                    var listProgramOrder = bllProgramOrder.GetList(new Model.ViewModel.Program.VmProgramOrder() { TemplateId = m.ProgramId, Type = m.Type, StudentId = StudentId }, out TotalCount);
                    if (listProgramOrder.Count > 0)
                    {
                        IsOrder = 1;
                        OrderId = listProgramOrder[0].OrderId;
                    }
                }
                model.OrderId = OrderId;
                model.Type = m.Type;
                model.ItemName = strItemName;
                model.ProgramImage = strImage;
                model.Description = strDescription;
                model.ItemID = ItemID;
                model.IsOrder = IsOrder;

                returnlist.Add(model);
            }
            var res = new Dictionary<string, List<SmWebStudentPlanProgram>>();
            res.Add("StudentPlanProgram", returnlist);
            return new ApiHandlerInvokeResult<Dictionary<string, List<SmWebStudentPlanProgram>>>()
            {
                Output = res,
                Success = true
            };
        }

        /// <summary>
        /// 留学规划_规划 第3步  修改指定规划年级
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> PlanRecordStudentGradeProgramEidt([FromBody]PlanRecordStudentGradeProgramEidtQuery modelQuery)
        {
            int TotalCount = 0;
            Guid PlanRecordId = Guid.Empty;
            
            PlanController bllPlan = new PlanController();

            
            


            var modelStudentGrade = bllPlan.GetModelStudentPlan(modelQuery.PlanStudentGradeId);

            //获取年级节点数据
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            var modelPlanningNote = bllPlanningNote.PlanningNoteGetList(new Model.ViewModel.Plan.VmPlanningNote() { Sort = modelStudentGrade.Sort, SchoolPlace = modelQuery.SchoolPlace, SchoolType = modelQuery.SchoolType, })[0];

            PlanRecordId = modelStudentGrade.PlanRecordId;
            modelStudentGrade.SchoolType = modelQuery.SchoolType;
            modelStudentGrade.SchoolPlace = modelQuery.SchoolPlace;
            //modelStudentGrade.AddEnglishScore = modelPlanningNote.AddEnglishScore;
            //modelStudentGrade.AddLearnScore = modelPlanningNote.AddLearnScore;
            //modelStudentGrade.AddQualityScore = modelPlanningNote.AddQualityScore;
            //modelStudentGrade.EnglishScore = modelPlanningNote.EnglishScore;
            //modelStudentGrade.LearnScore = modelPlanningNote.LearnScore;
            //modelStudentGrade.QualityScore = modelPlanningNote.QualityScore;
            //modelStudentGrade.Fee = modelPlanningNote.Fee;
            modelStudentGrade.PlanningNoteId = modelPlanningNote.DataId;

            //var listStudentPlanBackup = bllPlan.GetListStudentPlanBackup(new Model.ViewModel.Plan.VmStudentPlanBackup() { StudentPlanId = modelQuery.PlanStudentGradeId }, out TotalCount);
            //if (listStudentPlanBackup.Count > 0)
            //{
            //    var mpb = listStudentPlanBackup[0];
            //    if (mpb.Grade == modelStudentGrade.Grade && mpb.SchoolPlace == modelStudentGrade.SchoolPlace && mpb.SchoolType == modelStudentGrade.SchoolType)
            //    {
            //        modelStudentGrade.AddEnglishScore = mpb.AddEnglishScore;
            //        modelStudentGrade.AddLearnScore = mpb.AddLearnScore;
            //        modelStudentGrade.AddQualityScore = mpb.AddQualityScore;
            //        modelStudentGrade.EnglishScore = mpb.EnglishScore;
            //        modelStudentGrade.LearnScore = mpb.LearnScore;
            //        modelStudentGrade.QualityScore = mpb.QualityScore;
            //    }
            //}

            Guid returnId = bllPlan.CreateEditStudentPlan(modelStudentGrade);
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
                if (modelQuery.IsPlanProgram == "是")
                {
                    bllPlan.PlanStudentPlanProgram(PlanRecordId);
                }
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Output = (Guid)modelQuery.PlanStudentGradeId,
                    Success = true
                };
            }
        }
        /// <summary>
        /// 留学规划_规划 第3步  添加一个规划年级活动
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <param name="StudentPlanID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> AddStudentPlanProgram([FromBody]AddStudentPlanProgramQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            modelQuery.listPlanProgramTemplateId = modelQuery.listPlanProgramTemplateId ?? string.Empty;
            var listIds = modelQuery.listPlanProgramTemplateId.Split(",");
            foreach (string sid  in listIds)
            {
                Guid mid = Guid.Parse(sid.Replace("[","").Replace("]", "").Replace("\"", ""));
                SmStudentPlanProgram model = new SmStudentPlanProgram();
                //添加
                CourseTemplateController bllCourseTemplate = new CourseTemplateController();
                var modelTemplate = bllCourseTemplate.GetVmCourseTemplate(mid);
                if (modelTemplate.DataId != Guid.Empty)
                {
                    //model.Id = Guid.Empty;
                    model.StudentPlanId = modelQuery.StudentPlanID;
                    model.Type = 2;
                    model.ProgramId = modelTemplate.DataId;
                    model.Name = modelTemplate.Name;
                    model.ItemName = modelTemplate.ScoreItemNames;
                    model.Images = modelTemplate.Pictures;
                    model.AddEnglishScore = modelTemplate.EnglishScore;
                    model.AddLearnScore = modelTemplate.LearnScore;
                    model.AddQualityScore = modelTemplate.QualityScore;
                }
                else
                {
                    ProjectTemplateController bllProjectTemplate = new ProjectTemplateController();
                    var modelProjectTemplate = bllProjectTemplate.GetVmProjectTemplate(mid);
                    //model.Id = Guid.Empty;
                    model.StudentPlanId = modelQuery.StudentPlanID;
                    model.Type = 1;
                    model.ProgramId = modelProjectTemplate.DataId;
                    model.Name = modelProjectTemplate.Name;
                    model.ItemName = modelProjectTemplate.ScoreItemNames;
                    model.Images = modelProjectTemplate.Pictures;
                    model.AddEnglishScore = modelProjectTemplate.EnglishScore;
                    model.AddLearnScore = modelProjectTemplate.LearnScore;
                    model.AddQualityScore = modelProjectTemplate.QualityScore;
                }
                if (model.ProgramId != Guid.Empty)
                {
                    bllPlan.CreateEditStudentPlanProgram(model);
                }
            }
            return new ApiHandlerInvokeResult<Guid>()
            {
                Output = (Guid)modelQuery.StudentPlanID,
                Success = true
            };
        }
        /// <summary>
        /// 留学规划_规划 第3步  删除一个规划年级活动
        /// </summary>
        /// <param name="StudentPlanProgramId"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> DelStudentPlanProgram([FromBody]DelStudentPlanProgramQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            bllPlan.DeleteStudentPlanProgram(modelQuery.StudentPlanProgramId);
            return new ApiHandlerInvokeResult<Guid>()
            {
                Success = true
            };
        }
        /// <summary>
        /// 留学规划_规划 第3步  获取模板列表
        /// </summary>
        /// <param name="modelQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Dictionary<string, List<SmWebProgramTemplate>>> GetStudentPlanProgramTemplateList([FromBody]ProgramTemplateQuery modelQuery)
        {
            PlanController bllPlan = new PlanController();
            String TemplateName = "";
            String ItemName = modelQuery.ItemName;
            String SuitablePerson = "";
            String SuitablePersonId = "";
            #region SuitablePerson计算 
            if (modelQuery.Sort >= 7 && modelQuery.Sort <= 12)
            {
                SuitablePerson = "小学";
            } else if (modelQuery.Sort >= 13 && modelQuery.Sort <= 15)
            {
                SuitablePerson = "初中";
            }
            else if (modelQuery.Sort >= 16 && modelQuery.Sort <= 18)
            {
                SuitablePerson = "高中";
            }
            #endregion
            #region z组装返回列表
            List<SmWebProgramTemplate> list = new List<SmWebProgramTemplate>();
            if (modelQuery.Type == 1)
            {
                //活动
                ProjectTemplateController bllProjectTemplate = new ProjectTemplateController();
                int TotalCount = 0;
                var TList = bllProjectTemplate.GetList(new VmProjectTemplate() { State = EState.启用, Name = TemplateName, ScoreItemNames = modelQuery.ItemName,SuitablePerson= modelQuery.Sort.ToString() }, out TotalCount);
                
                foreach (var mt in TList)
                {
                    list.Add(new SmWebProgramTemplate()
                    {
                        // 模板ID
                        TemplateId = mt.DataId,
                        // 课程活动名称
                        Name = mt.Name,
                        // 课程活动描述
                        Description = String.Format("历练类型：{0},主办方：{1},历练成长：{2}", mt.ProjectTypeName, mt.Sponsor, mt.ScoreItemNames),
                        // 图片
                        ProgramImage = mt.Pictures,
                        // 类型名称  学习能力    领导能力
                        ItemName = mt.ScoreItemNames
                    });
                }
            }
            else
            {
                //课程
                CourseTemplateController bllCourseTemplate = new CourseTemplateController();
                int TotalCount = 0;
                var TList = bllCourseTemplate.GetList(new VmCourseTemplate() { State = EState.启用, Name = TemplateName, ScoreItemNames = modelQuery.ItemName, SuitablePerson = modelQuery.Sort.ToString() }, out TotalCount);
                foreach (var mt in TList)
                {
                    list.Add(new SmWebProgramTemplate()
                    {
                        // 模板ID
                        TemplateId = mt.DataId,
                        // 课程活动名称
                        Name = mt.Name,
                        // 课程活动描述
                        Description = String.Format("历练类型：{0},主办方：{1},历练成长：{2}", mt.CourseTypeName, mt.Sponsor, mt.ScoreItemNames),
                        // 图片
                        ProgramImage = mt.Pictures,
                        // 类型名称  学习能力    领导能力
                        ItemName = mt.ScoreItemNames
                    });
                }
            }
            #endregion
            #region 组装排除了的规划活动列表
            var oldStudentPlanProgramList = bllPlan.StudentPlanProgramGetList(new SmStudentPlanProgram() { StudentPlanId = modelQuery.StudentPlanId });
            List<SmWebProgramTemplate> returnlist = new List<SmWebProgramTemplate>();
            foreach (var ml in list)
            {
                bool b = true;
                foreach (var oldM in oldStudentPlanProgramList)
                {
                    if (oldM.ProgramId == ml.TemplateId)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    returnlist.Add(ml);
                }
            }
            #endregion
            var res = new Dictionary<string, List<SmWebProgramTemplate>>();
            res.Add("TemplateList", returnlist);
            return new ApiHandlerInvokeResult<Dictionary<string, List<SmWebProgramTemplate>>>()
            {
                Output = res,
                Success = true
            };
        }

        #region 新增规划访客记录
        /// <summary>
        /// 新增规划访客记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiHandlerInvokeResult<Guid> PlanVisitorAdd([FromBody]VmPlanVisitor model)
        {
            PlanVisitorController bllPlanVisitor = new PlanVisitorController();
            Guid returnId = bllPlanVisitor.AddEdit(model);
            return new ApiHandlerInvokeResult<Guid>()
            {
                Output = returnId,
                Success = true
            };
        }

      
        #endregion

        #endregion
    }
}