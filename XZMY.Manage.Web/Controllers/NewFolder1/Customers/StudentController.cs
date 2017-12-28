using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Web.Controllers.Planners;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Customers
{
    public class StudentController : ControllerBase
    {
        #region 功能
        public List<VmStudent> GetList(VmStudent model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<Student>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<Student>>
                    {
                        new CustomConditionPlus<Student>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<Student, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<Student, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Student>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Student, object>>[] { x => x.DataId }
                });
            }
            if (model.MemberId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Student>
                {
                    Value = model.MemberId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Student, object>>[] { x => x.MemberId }
                });
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<Student>
                {
                    Value = model.Name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<Student, object>>[] { x => x.Name }
                });
            }
            var result = service.Invoke();
            List<VmStudent> list = new List<VmStudent>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<Student, VmStudent>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        /// <summary>
        /// 通过ID获取学生信息对象
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public VmStudent GetVmStudent(Guid StudentId)
        {
            var service = new GetEntityByIdService<Student>(StudentId);
            var entity = service.Invoke();
            if (entity == null) entity = new Student();
            return entity.CreateViewModel<Student, VmStudent>();
        }
        /// <summary>
        /// 创建或修改学生信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid StudentAddEdit(VmStudent model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Student>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Student>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        #endregion
        #region 页面
        
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        #region 看学生列表页面
        /// <summary>
        /// 规划师 查看学生列表页面
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "学生列表", Code = "StudentList", ModuleCode = "CUSTOMERS", Url = "/Student/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            Guid PlannerId = Guid.Empty;
            //if (LoggedUserManager.IsLogin())
            //{
            //    var User = LoggedUserManager.GetCurrentUserAccount();
            //    PlannerId = User.PlannerId;
            //}
            VmPlannerEdit model = new VmPlannerEdit();
            //if (PlannerId != Guid.Empty)
            //{
            //    var service = new GetEntityByIdService<Planner>(PlannerId);
            //    var entity = service.Invoke();
            //    if (entity == null) entity = new Model.DataModel.Planners.Planner();
            //    model = entity.CreateViewModel<Model.DataModel.Planners.Planner, Model.ViewModel.Planners.VmPlannerEdit>();
            //}
            return View(model);
        }
        /// <summary>
        /// Ajax查看学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PlannerId"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmSearchBase model, Guid? PlannerId)
        {
            var service = new CustomSearchWithPaginationService<V_StudentMember_List>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_StudentMember_List>>()
                {
                    new CustomConditionBase<V_StudentMember_List>()
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = x => x.Name
                    }
                },
                SortMember = new Expression<Func<V_StudentMember_List, object>>[] { x => x.CreatedTime }
            };
            if (PlannerId.HasValue)
            {
                if (PlannerId != Guid.Empty)
                {
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<V_StudentMember_List>>
                        {
                    new CustomConditionPlus<V_StudentMember_List>
                    {
                        Value = PlannerId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_StudentMember_List, object>>[] { x => x.PlannerId}
                    }
                        });
                }
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult Details(Guid id,string go)
        {
            //为空时 表示跳转到学生列表  否 则跳转到学生课程列表
            ViewBag.GoUrl = go ?? "";

            PlannerController bllPlanner = new PlannerController();
            var model = GetStudent(id);
            if (model.PlannerId != Guid.Empty)
            {
                model.modelPlanner = bllPlanner.GetPlanner(model.PlannerId);
            }
            return View(model);
        }
        /// <summary>
        /// 学生编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id) {
            return View(GetStudent(id));
        }


        #endregion
        

        public ActionResult AjaxParent(Guid id)
        {
            var service = new GetEntityByIdService<Parent>(id);
            var res = service.Invoke();
            if (res == null)
            {
                return Json(new { success = false, result = res }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, result = res }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 学生Help状态修改正常
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public ActionResult AjaxStudentHelpComplete(Guid StudentId)
        {
            VmStudent modelEdit = new VmStudent();
            //获取数据
            var service = new GetEntityByIdService<Student>(StudentId);
            var res = service.Invoke();
            modelEdit = res.CreateViewModel<Student, VmStudent>();
            //修改数据
            modelEdit.IsHelp = false;
            var handler = new BaseModifyHandler<Student>(modelEdit);
            var resEdit = handler.Invoke();
            if (resEdit.Code != 0)
            {
                return Json(new { success = false, errors = GetErrors() });
            }
            return Json(new { success = resEdit.Success, Id = modelEdit.DataId, errors = GetErrors() });
        }
        

        public ActionResult AjaxListStudent(VmSearchBase<Student> model,Guid? PlannerId,Boolean? JoinCourse)
        {
            var service = new CustomSearchWithPaginationService<Student>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,

                CustomConditions = new List<CustomCondition<Student>>()
                {
                    new CustomConditionBase<Student>()
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = x => x.Name
                    }
                },

                SortMember = new Expression<Func<Student, object>>[] { x => x.CreatedTime }
            };
            #region PlannerId
            if (PlannerId != null)
            {
                service.CustomConditions.AddRange(
                new List<CustomCondition<Student>>
                    {
                    new CustomConditionPlus<Student>
                    {
                        Value = PlannerId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Student, object>>[] { x => x.PlannerId}
                    }
                    });
            }
            #endregion
          
            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #region 给学生重新分配规划师
        /// <summary>
        /// 给学生重新分配规划师页面
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult EditStudentPlanner(Guid StudentId)
        {
            VmStudent modelStudent = new VmStudent();
            var serviceStudent = new GetEntityByIdService<Student>(StudentId);
            var entityStudent = serviceStudent.Invoke();
            if (entityStudent == null) entityStudent = new Student();
            modelStudent = entityStudent.CreateViewModel<Student, VmStudent>();
            
            #region 全部规划师列表 listPlanner
            var servicePlanner = new CustomSearchWithPaginationService<Planner>
            {
                PageIndex = 1,
                PageSize = 999999,
                CustomConditions = new List<CustomCondition<Planner>>()
                  {
                      new CustomConditionPlus<Planner>()
                      {
                          Value = "",
                          Operation = SqlOperation.Like,
                          Member = new Expression<Func<Planner, object>>[] { x => x.Name}
                      }
                  },
                SortMember = new Expression<Func<Planner, object>>[] { x => x.CreatedTime }
            };
            var resultPlanner = servicePlanner.Invoke();
            List<VmPlannerEdit> listPlanner = new List<VmPlannerEdit>();
            foreach (var m in resultPlanner.Results)
            {
                listPlanner.Add(m.CreateViewModel<Planner, VmPlannerEdit>());
            }
            modelStudent.listPlanner = listPlanner;
            #endregion 

            return View(modelStudent);
        }
        /// <summary>
        /// ajax 修改学生部分信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxEdit(VmStudent model)
        {
            if (model.DataId != Guid.Empty)
            {
                var modelStudent = GetStudent(model.DataId);
                modelStudent.StudentsEvaluation = model.StudentsEvaluation;
                Guid StudentId = StudentAddEdit(modelStudent);
                return Json(new { success = true,Id= StudentId, errors = GetErrors() });
            }
            return Json(new { success = false, errors = GetErrors() });
        }
        /// <summary>
        /// AJAX获取学生信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxEditStudentPlanner(VmStudent model)
        {
            
            VmStudent modelStudent = new VmStudent();
            var serviceStudent = new GetEntityByIdService<Student>(model.DataId);
            var entityStudent = serviceStudent.Invoke();
            if (entityStudent == null) entityStudent = new Student();
            modelStudent = entityStudent.CreateViewModel<Student, VmStudent>();

            Guid PlannerId = Guid.Empty;
            if (Request.Params["dllPlanner"] != null)
            {
                Guid.TryParse(Request.Params["dllPlanner"].ToString(), out PlannerId);
                modelStudent.IsVip = 1;
                modelStudent.PlannerId = PlannerId;
            }

            

            var handler = new BaseModifyHandler<Student>(modelStudent);
            var resEdit = handler.Invoke();
            if (resEdit.Code != 0)
            {
                return Json(new { success = false, errors = GetErrors() });
            }
            return Json(new { success = resEdit.Success, Id = modelStudent.DataId, errors = GetErrors() });
        }
        #endregion


        #region 学生申请集
        /// <summary>
        /// 学生申请集显示页面
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public ActionResult StudentApply(Guid StudentId)
        {
            return View(GetStudentApply(StudentId));
        }
        /// <summary>
        /// 获取学生申请集
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public VmStudentApply GetStudentApply(Guid StudentId)
        {
            VmStudentApply model = new VmStudentApply();
            model.modelStudent = GetStudent(StudentId);
            model.modelIntention = GetStudentApply_Intention(StudentId);
            #region VmStudentApply_ContactInformation
            VmStudentApply_ContactInformation modelContactInformation = new VmStudentApply_ContactInformation();
            // 中国 - 重庆 - 重庆 - 重庆渝中区大坪,中国 - 重庆 - 重庆 - 1111111111
            var tempV = model.modelStudent.RegisteredPermanentResidenceLocation.Split(",");
            if (tempV.Length > 0)
            {
                var tempV0 = tempV[0].ToString().Split("-");
                if (tempV0.Length > 0)
                    modelContactInformation.CurrentCountry = tempV0[0];
                if (tempV0.Length > 1)
                    modelContactInformation.CurrentProvince = tempV0[1];
                if (tempV0.Length > 2)
                    modelContactInformation.CurrentCity = tempV0[2];
                if (tempV0.Length > 3)
                    modelContactInformation.CurrentAddress = tempV0[3];
            }
            if (tempV.Length > 1)
            {
                modelContactInformation.CurrentAddress2 = tempV[1];
            }


            var tempPathV = model.modelStudent.LocationPathName.Split(",");
            if (tempPathV.Length > 0)
            {
                var tempPathV0 = tempPathV[0].ToString().Split("-");
                if (tempPathV0.Length > 0)
                    modelContactInformation.PermanentCountry = tempPathV0[0];
                if (tempPathV0.Length > 1)
                    modelContactInformation.PermanentProvince = tempPathV0[1];
                if (tempPathV0.Length > 2)
                    modelContactInformation.PermanentCity = tempPathV0[2];
                if (tempPathV0.Length > 3)
                    modelContactInformation.PermanentAddress = tempPathV0[3];
            }
            if (tempPathV.Length > 1)
            {
                modelContactInformation.PermanentAddress2 = tempPathV[1];
            }
            #endregion
            model.modelContactInformation = modelContactInformation;
            model.listProject= GetStudentApply_Project(StudentId);
            model.listInterest = GetStudentApply_InterestList(StudentId);
            model.listGuardian = GetStudentApply_GuardianList(StudentId);
            model.listCreificate = GetStudentApply_CertificateList(StudentId);
            #region listSchoolInformation
            List<VmStudentApply_SchoolInformation> listSchoolInformation= GetStudentApply_SchoolInformation(StudentId);
            List<VmStudentApply_SchoolGrade> listSchoolGrade = GetStudentApply_SchoolGrade(StudentId);
            List<VmStudentApply_SchoolCourse> listSchoolCourse = GetStudentApply_SchoolCourse(StudentId);

            List<VmStudentApply_SchoolInformation> listSchoolInformationModel = new List<VmStudentApply_SchoolInformation>();
            foreach (var mSchoolInformation in listSchoolInformation)
            {
                VmStudentApply_SchoolInformation mInformation = mSchoolInformation;
                List<VmStudentApply_SchoolGrade> listSchoolGradeInformation = listSchoolGrade.Where(x => x.SchoolInformationId == mInformation.DataId).ToList();
                foreach (var mSchoolGrade in listSchoolGradeInformation)
                {
                    List<VmStudentApply_SchoolCourse> listSchoolCourseGrade = listSchoolCourse.Where(x => x.StudentApply_SchoolGradeId == mSchoolGrade.DataId).ToList();
                    mSchoolGrade.listSchoolCourse = listSchoolCourseGrade;
                }
                mInformation.listSchoolGrade = listSchoolGradeInformation;
                listSchoolInformationModel.Add(mInformation);
            }
            model.listSchoolInformation = listSchoolInformationModel;
            #endregion
            return model;
        }
        /// <summary>
        /// 获取学生基础信息
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public VmStudent GetStudent(Guid StudentId)
        {
            return GetVmStudent(StudentId); 
        }
        #region 获取学生留学意向
        /// <summary>
        /// 获取学生留学意向
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public VmStudentApply_Intention GetStudentApply_Intention(Guid StudentId) {
            VmStudentApply_Intention model = new VmStudentApply_Intention();
            int TotalCount = 0;
            var list = GetListStudentApplyIntention(new VmStudentApply_Intention() { StudentId = StudentId }, out TotalCount);
            if (list.Count > 0)
            {
                model = list[0];
            }
            return model;
        }
        
        /// <summary>
        /// 创建或修改学生留学意向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditStudentApplyIntention(VmStudentApply_Intention model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<StudentApply_Intention>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<StudentApply_Intention>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 获取列表学生留学意向
        /// </summary>
        /// <param name="model"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public List<VmStudentApply_Intention> GetListStudentApplyIntention(VmStudentApply_Intention model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_Intention>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_Intention>>
                    {
                        new CustomConditionPlus<StudentApply_Intention>
                        {
                            Value = model.IntentionalSchoolName??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<StudentApply_Intention, object>>[]
                            {
                                m => m.IntentionalSchoolName
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_Intention, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Intention>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Intention, object>>[] { x => x.DataId }
                });
            }
            if (model.StudentId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Intention>
                {
                    Value = model.StudentId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Intention, object>>[] { x => x.StudentId }
                });
            }
            var result = service.Invoke();
            List<VmStudentApply_Intention> list = new List<VmStudentApply_Intention>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_Intention, VmStudentApply_Intention>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 获取学生联系方式
        /// <summary>
        /// 获取学生联系方式
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public VmStudentApply_ContactInformation GetStudentApply_ContactInformation(Guid StudentId)
        {
            int TotalCount = 0;
            VmStudentApply_ContactInformation model = new VmStudentApply_ContactInformation();
            var list = GetListStudentApplyContactInformation(new VmStudentApply_ContactInformation() { StudentId = StudentId }, out TotalCount);
            if (list.Count > 0)
            {
                model = list[0];
            }
            return model;
        }
        public List<VmStudentApply_ContactInformation> GetListStudentApplyContactInformation(VmStudentApply_ContactInformation model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_ContactInformation>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_ContactInformation>>
                    {
                        new CustomConditionPlus<StudentApply_ContactInformation>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_ContactInformation, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_ContactInformation, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_ContactInformation>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_ContactInformation, object>>[] { x => x.DataId }
                });
            }
           
            var result = service.Invoke();
            List<VmStudentApply_ContactInformation> list = new List<VmStudentApply_ContactInformation>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_ContactInformation, VmStudentApply_ContactInformation>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 学生获取证书
        /// <summary>
        /// 获取学生获取证书列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_Certificate> GetStudentApply_CertificateList(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplyCertificate(new VmStudentApply_Certificate() { StudentId = StudentId }, out TotalCount);
        }
        public List<VmStudentApply_Certificate> GetListStudentApplyCertificate(VmStudentApply_Certificate model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_Certificate>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_Certificate>>
                    {
                        new CustomConditionPlus<StudentApply_Certificate>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_Certificate, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_Certificate, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Certificate>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Certificate, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_Certificate> list = new List<VmStudentApply_Certificate>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_Certificate, VmStudentApply_Certificate>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 监护人家庭成员
        /// <summary>
        /// 获取 监护人家庭成员列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_Guardian> GetStudentApply_GuardianList(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplyGuardian(new VmStudentApply_Guardian() { StudentId=StudentId},out TotalCount);
        }
        public List<VmStudentApply_Guardian> GetListStudentApplyGuardian(VmStudentApply_Guardian model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_Guardian>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_Guardian>>
                    {
                        new CustomConditionPlus<StudentApply_Guardian>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_Guardian, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_Guardian, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Guardian>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Guardian, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_Guardian> list = new List<VmStudentApply_Guardian>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_Guardian, VmStudentApply_Guardian>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 学生兴趣
        /// <summary>
        /// 获取 兴趣列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_Interest> GetStudentApply_InterestList(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplyInterest(new VmStudentApply_Interest() { StudentId=StudentId},out TotalCount);
        }
        public List<VmStudentApply_Interest> GetListStudentApplyInterest(VmStudentApply_Interest model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_Interest>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_Interest>>
                    {
                        new CustomConditionPlus<StudentApply_Interest>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_Interest, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_Interest, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Interest>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Interest, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_Interest> list = new List<VmStudentApply_Interest>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_Interest, VmStudentApply_Interest>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 课外活动
        /// <summary>
        /// 获取 课外活动列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_Project> GetStudentApply_Project(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplyProject(new VmStudentApply_Project() { StudentId=StudentId},out TotalCount);
        }
        public List<VmStudentApply_Project> GetListStudentApplyProject(VmStudentApply_Project model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_Project>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_Project>>
                    {
                        new CustomConditionPlus<StudentApply_Project>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_Project, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_Project, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_Project>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_Project, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_Project> list = new List<VmStudentApply_Project>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_Project, VmStudentApply_Project>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 课外活动
        /// <summary>
        /// 获取 课外活动列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_SchoolInformation> GetStudentApply_SchoolInformation(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplySchoolInformation(new VmStudentApply_SchoolInformation() { StudentId=StudentId},out TotalCount);
        }
        public List<VmStudentApply_SchoolInformation> GetListStudentApplySchoolInformation(VmStudentApply_SchoolInformation model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_SchoolInformation>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_SchoolInformation>>
                    {
                        new CustomConditionPlus<StudentApply_SchoolInformation>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_SchoolInformation, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_SchoolInformation, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_SchoolInformation>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_SchoolInformation, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_SchoolInformation> list = new List<VmStudentApply_SchoolInformation>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_SchoolInformation, VmStudentApply_SchoolInformation>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 学校年级
        /// <summary>
        /// 获取 学校年级列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_SchoolGrade> GetStudentApply_SchoolGrade(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplySchoolGrade(new VmStudentApply_SchoolGrade() { StudentId=StudentId},out TotalCount);
        }
        public List<VmStudentApply_SchoolGrade> GetListStudentApplySchoolGrade(VmStudentApply_SchoolGrade model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_SchoolGrade>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_SchoolGrade>>
                    {
                        new CustomConditionPlus<StudentApply_SchoolGrade>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_SchoolGrade, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_SchoolGrade, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_SchoolGrade>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_SchoolGrade, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_SchoolGrade> list = new List<VmStudentApply_SchoolGrade>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_SchoolGrade, VmStudentApply_SchoolGrade>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #region 学校课程
        /// <summary>
        /// 获取 课程列表
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public List<VmStudentApply_SchoolCourse> GetStudentApply_SchoolCourse(Guid StudentId)
        {
            int TotalCount = 0;
            return GetListStudentApplySchoolCourse(new VmStudentApply_SchoolCourse() { StudentId = StudentId }, out TotalCount);
        }
        public List<VmStudentApply_SchoolCourse> GetListStudentApplySchoolCourse(VmStudentApply_SchoolCourse model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentApply_SchoolCourse>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentApply_SchoolCourse>>
                    {
                        new CustomConditionPlus<StudentApply_SchoolCourse>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<StudentApply_SchoolCourse, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentApply_SchoolCourse, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentApply_SchoolCourse>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentApply_SchoolCourse, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmStudentApply_SchoolCourse> list = new List<VmStudentApply_SchoolCourse>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentApply_SchoolCourse, VmStudentApply_SchoolCourse>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #endregion
    }
}