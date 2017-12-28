using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Web.Content.Code;
using XZMY.Manage.Web.Controllers.Planners;
using XZMY.Manage.Web.Controllers.Sys;
using XZMY.Manage.Web.Controllers.WebApis;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Customers
{
    public class VipController : ControllerBase
    {
        #region 页面
        /// <summary>
        /// VIP学生列表页面
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "学生管理", Code = "VipStudentList", ModuleCode = "PLANNER", Url = "/Vip/StudentList", Visible = true)]
        public ActionResult StudentList()
        {
            VmPlannerEdit modelVm = new VmPlannerEdit();
            Guid PlannerId = Guid.Empty;
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
            }


            var service = new GetEntityByIdService<Planner>(PlannerId);
            var entity = service.Invoke() ?? new Planner();
            modelVm = entity.CreateViewModel<Planner, VmPlannerEdit>();
            return View(modelVm);
        }
        /// <summary>
        /// 学生详细页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StudentDetails(Guid? id, string refUrl)
        {
            refUrl = refUrl ?? "/Student/List";
            ViewBag.refUrl = refUrl;
            VmStudent model = new VmStudent();
            if (id.HasValue)
            {
                StudentController bllStudent = new StudentController();
                model = bllStudent.GetStudent(id.Value);
            }
            return View(model);
        }
        /// <summary>
        /// 学生规划查看页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StudentPlan(Guid? id)
        {
            Guid PlanRecordId = Guid.Empty;
            if (Request.QueryString["StudentId"] != null)
            {
                Guid StudentId = Request.QueryString["StudentId"].ToGuid(Guid.Empty);
                if (StudentId != Guid.Empty)
                {
                    PlanController bllPlan = new PlanController();
                    List<SmPlanRecord> listPlanRecord = bllPlan.GetListPlanRecord(new SmPlanRecord() { StudentId = StudentId });
                    if (listPlanRecord.Count > 0)
                    {
                        listPlanRecord = listPlanRecord.OrderByDescending(x => x.CreatedTime).ToList();
                        PlanRecordId = listPlanRecord[0].DataId;
                    }
                }
            }
            if (PlanRecordId == Guid.Empty)
            {
                if (id.HasValue)
                {
                    PlanRecordId = id.Value;
                }
            }
            XZMY.Manage.Model.ServiceModel.Plan.SmPlanRecord model = new Model.ServiceModel.Plan.SmPlanRecord();
            APlanController bllap = new APlanController();
            var model1 = bllap.GetPlanRecord(new GetPlanRecordQuery() { PlanRecordId = PlanRecordId });
            model = model1.Output["SmPlanRecord"];
            return View(model);
        }
        /// <summary>
        /// 学生规划列表页面
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult StudentPlanList(Guid? StudentId)
        {
            VmStudent modelVmStudent = new VmStudent();
            if (StudentId.HasValue)
            {
                var service = new GetEntityByIdService<Student>(StudentId.Value);
                var entity = service.Invoke();
                if (entity == null) entity = new Student();
                modelVmStudent = entity.CreateViewModel<Student, VmStudent>();
            }
            return View(modelVmStudent);
        }
        /// <summary>
        /// 规划师列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PlannerList()
        {
            return View();
        }
        /// <summary>
        /// 查看学生VIP申请记录列表
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "学生申请VIP列表", Code = "StudentVipApplyList", ModuleCode = "PLANNER", Url = "/Vip/StudentVipApplyList", Visible = true)]
        public ActionResult StudentVipApplyList(Guid? PlannerId)
        {
            VmPlannerEdit modelVmPlanner = new VmPlannerEdit();
            if (PlannerId.HasValue)
            {
                var service = new GetEntityByIdService<Planner>(PlannerId.Value);
                var entity = service.Invoke();
                if (entity == null) entity = new Model.DataModel.Planners.Planner();
                modelVmPlanner = entity.CreateViewModel<Model.DataModel.Planners.Planner, Model.ViewModel.Planners.VmPlannerEdit>();
            }
            return View(modelVmPlanner);
        }

        /// <summary>
        /// 学生申请VIP页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult StudentVipApplyDetails(Guid Id)
        {
            VmStudentVipApply model = new VmStudentVipApply();

            var serviceStudentVipApply = new GetEntityByIdService<StudentVipApply>(Id);
            var entityStudentVipApply = serviceStudentVipApply.Invoke();
            if (entityStudentVipApply == null) entityStudentVipApply = new StudentVipApply();
            model = entityStudentVipApply.CreateViewModel<StudentVipApply, VmStudentVipApply>();


            if (model.StudentId != Guid.Empty)
            {
                var serviceStudent = new GetEntityByIdService<Student>(model.StudentId);
                var entityStudent = serviceStudent.Invoke();
                if (entityStudent == null) entityStudent = new Student();
                model.Student = entityStudent.CreateViewModel<Student, VmStudent>();
            }


            if (model.PlannerId != Guid.Empty)
            {
                var servicePlanner = new GetEntityByIdService<Planner>(model.PlannerId);
                var entityPlanner = servicePlanner.Invoke();
                if (entityPlanner == null) entityPlanner = new Planner();
                model.Planner = entityPlanner.CreateViewModel<Planner, VmPlanner>();
            }

            return View(model);
        }
        public ActionResult Change()
        {
            return View();
        }
        public int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
        public Stream FileToStream(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                // 打开文件
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[]
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            else {
                return null;
            }
                
        }
        /// <summary>
        /// 导出规划WORD
        /// </summary>
        /// <returns></returns>
        public ActionResult PlanRecordToWord()
        {
            PlanController bllplan = new PlanController();
            StudentController bllStudent = new StudentController();
             PlannerController bllPlanner = new PlannerController();
            SchoolTypeController bllSchoolType = new SchoolTypeController();
            SchoolController bllSchool = new SchoolController();

            Guid PlanRecordId = Request.QueryString["PlanRecordId"].ToGuid(Guid.Empty);

            XZMY.Manage.Service.Utils.StudentPlanExporter toword = new StudentPlanExporter();
            PlanTemplate modelTemplate = new PlanTemplate();
            int TotalCount = 0;
            var modelPlanRecord = bllplan.GetSmPlanRecord(PlanRecordId);
            var modelStudent = bllStudent.GetStudent(modelPlanRecord.StudentId);
            var modelPlanner = bllPlanner.GetPlanner(modelStudent.PlannerId);
            var modelListSchool = bllSchool.GetList(new Model.ViewModel.School.VmSchool() { SchoolTypeId = modelPlanRecord.SchoolTypeId }, out TotalCount);
            var modelPlanningPath = new PlanningPath();
            #region modelPlanningPath
            var listStudentPlan = bllplan.GetStudentPlanList(modelPlanRecord.DataId);
            modelPlanningPath.Remark = modelPlanRecord.Remark;
            List<PathPoint> listTempPathPoint = new List<PathPoint>();
            listStudentPlan = listStudentPlan.OrderBy(x => x.Sort).ToList();
            foreach (var m in listStudentPlan)
            {
                PathPoint mpp = new PathPoint();
                mpp.Name = string.Format("年级：{0}， 学校类型：{1} ，地点：{2}", m.Grade,m.SchoolType,m.SchoolPlace);
                mpp.Sort = m.Sort;
                List<string> listName = new List<string>();
                var listStudentPlanProgram = bllplan.StudentPlanProgramGetList(new SmStudentPlanProgram() { StudentPlanId = m.DataId });
                foreach (var mProgram in listStudentPlanProgram)
                {
                    listName.Add(mProgram.Name);
                }
                mpp.Projects = listName;
                listTempPathPoint.Add(mpp);
            }
            modelPlanningPath.Points = listTempPathPoint;
            #endregion
            #region 学生基本信息
            modelTemplate.StudentName = modelPlanRecord.StudentName;
            if (modelStudent.BirthDate > DateTime.Parse("1970-01-02")) {
                modelTemplate.StudentAge = CalculateAgeCorrect(modelStudent.BirthDate, DateTime.Now).ToString();
            }
            else {
                modelTemplate.StudentAge = "无";
            }
            modelTemplate.StudentGender = modelStudent.GenderName;
            modelTemplate.PlanCreatedTime = modelPlanRecord.CreatedTime.ToString("yyyy-MM-dd");
            #endregion
            #region 规划师信息
            #region PlannerImage
            if (modelPlanner.Pictures.Length == 0)
            {
                modelPlanner.Pictures = "/Upload/20160817141529562.jpg";
            }
            if (modelPlanner.Pictures.Length > 0)
            {
                modelTemplate.PlannerImage = FileToStream(Server.MapPath(modelPlanner.Pictures.Split(",")[0]));
            }
            #endregion
            modelTemplate.PlannerName = modelPlanner.Name;
            #region QualificationsName
            List<String> listPlannerQuality = new List<string>();
            listPlannerQuality.Add(modelPlanner.QualificationsName);
            modelTemplate.PlannerQuality = listPlannerQuality;
            #endregion
            #region LevelName
            List<String> listPlannerHonor = new List<string>();
            listPlannerHonor.Add(modelPlanner.LevelName);
            modelTemplate.PlannerHonor = listPlannerHonor;
            #endregion
            modelTemplate.PlannerDescription = modelPlanner.Description;
            #endregion
            #region 学生留学意向信息
            modelTemplate.TargetCountry = modelPlanRecord.TargetCountryName;
            modelTemplate.TargetSchoolType = bllSchoolType.GetModel(modelPlanRecord.SchoolTypeId).Name;
            modelTemplate.Budget = modelPlanRecord.Fee;
            #endregion
            #region 学生信息
            modelTemplate.Hobby = modelPlanRecord.Hobby;
            modelTemplate.Skill = modelPlanRecord.Skill;
            modelTemplate.School = modelPlanRecord.School;
            modelTemplate.Grade = modelPlanRecord.EducationName;
            modelTemplate.Planning = modelStudent.StudentsEvaluation;
            #endregion
            #region 学校信息
            List<PlanningSchool> listSchool = new List<PlanningSchool>();
            foreach (var m in modelListSchool)
            {
                PlanningSchool mPlanningSchool = new PlanningSchool();
                mPlanningSchool.Name = m.Name;
                List<String> ListImage = StringHtml.PickupImgUrl(m.Description);
                if (ListImage.Count == 0)
              
            {
                    ListImage.Add("/Upload/20160817141529562.jpg");
                }

                    
                if (ListImage.Count > 1)
                {
                    mPlanningSchool.Logo = FileToStream(Server.MapPath(ListImage[0]));
                }
                List<Stream> listStream = new List<Stream>();
                foreach (var mImage in ListImage)
                {
                    listStream.Add(FileToStream(Server.MapPath(mImage)));
                }
                mPlanningSchool.Images = listStream;
                mPlanningSchool.Location = m.LocationPathName;
                mPlanningSchool.Ranking = m.Ranking;
                mPlanningSchool.CreatedTime = m.EstablishDate.ToString("yyyy-MM-dd");
                mPlanningSchool.Description = m.Description;
                listSchool.Add(mPlanningSchool);
            }
            modelTemplate.Schools = listSchool;
            #endregion
            #region 评估结果
            modelTemplate.EnglishScore = modelPlanRecord.EnglishScore;
            //modelTemplate.listEnglishIndex;
            modelTemplate.LearnScore = modelPlanRecord.LearnScore;
            //modelTemplate.listLearnIndex;
            modelTemplate.QualityScore = modelPlanRecord.QualityScore;
            //modelTemplate.listQualityIndex;
            #endregion
            #region 规划
            modelTemplate.PlanningPath = modelPlanningPath;
            modelTemplate.PlannerRecommand = "";
            modelTemplate.PlannerRecommand2 = "";
            modelTemplate.PlannerRecommand3 = modelStudent.StudentsEvaluation;
            #endregion
            var v = toword.ExportWordFileStream(modelTemplate, Server.MapPath("/plantemplate.docx"));

            string WordUrl = string.Format("/Upload/Plan/Word/{0}{1}.docx", modelTemplate.StudentName, DateTime.Now.Ticks);

            using (FileStream fs = new FileStream(Server.MapPath(WordUrl), FileMode.Create, FileAccess.Write))
            {
                byte[] data = v.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            return Redirect(WordUrl);
        }
        #endregion

        /// <summary>
        /// AJAX获取申请学生申请VIP列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PlannerId"></param>
        /// <returns></returns>
        public ActionResult AjaxStudentVipApplyList(VmSearchBase model, Guid? PlannerId)
        {
            var service = new CustomSearchWithPaginationService<StudentVipApply>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<StudentVipApply>>
                {
                    new CustomConditionBase<StudentVipApply>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = x => x.StudentName
                    }
                },
                SortMember = new Expression<Func<StudentVipApply, object>>[] { x => x.ApplyTime }
            };
            service.CustomConditions.AddRange(
                new List<CustomCondition<StudentVipApply>>
                    {
                    new CustomConditionPlus<StudentVipApply>
                    {
                        Value = EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentVipApply, object>>[] { x => x.State}
                    }
                    });
            if (PlannerId.HasValue)
            {
                if (PlannerId.Value != Guid.Empty)
                {
                    //添加参数
                    service.CustomConditions.AddRange(
                    new List<CustomCondition<StudentVipApply>>
                        {
                    new CustomConditionPlus<StudentVipApply>
                    {
                        Value = PlannerId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentVipApply, object>>[] { x => x.PlannerId}
                    }
                        });
                }
            }
            var result = service.Invoke();
            List<VmStudentVipApply> listVmStudentVipApply = new List<VmStudentVipApply>();
            VmStudentVipApply modelVmStudentVipApply = new VmStudentVipApply();
            foreach (var m in result.Results)
            {
                modelVmStudentVipApply = m.CreateViewModel<StudentVipApply, VmStudentVipApply>();
                listVmStudentVipApply.Add(modelVmStudentVipApply);
            }
            return Json(new { success = true, total = result.TotalCount, rows = listVmStudentVipApply, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// AJAX  修改学生申请VIP的状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxStudentVipApplyEdit(VmStudentVipApply model)
        {
            if (model.DataId != Guid.Empty)
            {
                VmStudentVipApply modelVip = new VmStudentVipApply();
               
                var serviceStudentVipApply = new GetEntityByIdService<StudentVipApply>(model.DataId);
                var entityStudentVipApply = serviceStudentVipApply.Invoke();
                if (entityStudentVipApply == null) entityStudentVipApply = new StudentVipApply();
                modelVip = entityStudentVipApply.CreateViewModel<StudentVipApply, VmStudentVipApply>();
                modelVip.State = EState.禁用;
                var handlerVip = new BaseModifyHandler<StudentVipApply>(modelVip);
                var resVip = handlerVip.Invoke();




                var serviceStudent = new GetEntityByIdService<Student>(modelVip.StudentId);
                var entityStudent = serviceStudent.Invoke();
                if (entityStudent == null) entityStudent = new Student();
                VmStudent modelStudent = entityStudent.CreateViewModel<Student, VmStudent>();
                modelStudent.IsVip = 1;
                modelStudent.PlannerId = modelVip.PlannerId;
                var handlerStudent = new BaseModifyHandler<Student>(modelStudent);
                var resStudent = handlerStudent.Invoke();




                return Json(new { success = resVip.Success, Id = modelVip.DataId, PlannerId= modelVip.PlannerId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = false, errors = GetErrors() });
            }
        }

        
      
        //列表 Ajax 获取数据
        public ActionResult AjaxStudentList(VmSearchBase model, Guid? PlannerId)
        {
            var service = new CustomSearchWithPaginationService<V_VipStudent_List>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<V_VipStudent_List>>
                {
                    new CustomConditionBase<V_VipStudent_List>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = x => x.Name
                    }
                },
                SortMember = new Expression<Func<V_VipStudent_List, object>>[] { x => x.IsHelp, x => x.CreatedTime }
            };

            if (PlannerId.HasValue)
            {
                service.CustomConditions.Add(new CustomConditionPlus<V_VipStudent_List>
                {
                    Value = PlannerId.Value,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<V_VipStudent_List, object>>[] { x => x.PlannerId }
                });
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxPlannerList(VmSearchBase model)
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
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// AJAX 规划师查看学生规划的历史规划列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxPlanRecordList(VmSearchBase model, Guid? StudentId)
        {
            var service = new CustomSearchWithPaginationService<PlanRecord>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<PlanRecord>>()
                {
                    new CustomConditionBase<PlanRecord>()
                    {
                        Value = model.Keyword,
                        Operation = SqlOperation.Like,
                        Member = x => x.EducationName
                    }
                },
                SortMember = new Expression<Func<PlanRecord, object>>[] { x => x.CreatedTime }
            };
            if (StudentId.HasValue)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord>
                {
                    Value = StudentId.Value,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord, object>>[] { x => x.StudentId }
                });
            }
            var result = service.Invoke();

            List<SmPlanRecord> listSmPlanRecord = new List<SmPlanRecord>();
            IList<PlanRecord> listPlanRecord = result.Results;
            SmPlanRecord modelSmPlanRecord;
            foreach (var mPlanRecord in listPlanRecord)
            {
                modelSmPlanRecord = new SmPlanRecord();
                modelSmPlanRecord = mPlanRecord.CreateViewModel<PlanRecord, SmPlanRecord>();
                listSmPlanRecord.Add(modelSmPlanRecord);
            }
            listSmPlanRecord = listSmPlanRecord.OrderByDescending(x => x.PlanTime).ToList();
            return Json(new { success = true, total = result.TotalCount, rows = listSmPlanRecord, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxStudentPlan(SmPlanRecord model)
        {
            Guid PlanRecordId = Request.Params["Id"].ToGuid(Guid.Empty);
            Guid StudentId = Request.Params["StudentId"].ToGuid(Guid.Empty);
            Decimal EnglishScore = Request.Params["EnglishScore"].ToDecimal(0M);
            Decimal LearnScore = Request.Params["LearnScore"].ToDecimal(0M);
            Decimal QualityScore = Request.Params["QualityScore"].ToDecimal(0M);
            StudentController bllStudent = new StudentController();
            PlanController bllPlan = new PlanController();
            var modelPlanRecord = bllPlan.GetSmPlanRecord(PlanRecordId);
            modelPlanRecord.EnglishScore= EnglishScore;
            modelPlanRecord.LearnScore= LearnScore;
            modelPlanRecord.QualityScore= QualityScore;
            bllPlan.CreateEditPlanRecord(modelPlanRecord);

            var modelStudent = bllStudent.GetStudent(StudentId);
            modelStudent.EnglishScore = EnglishScore;
            modelStudent.LearnScore = LearnScore;
            modelStudent.QualityScore = QualityScore;
            bllStudent.StudentAddEdit(modelStudent);
            return Json(new { success = true, Id = PlanRecordId, errors = GetErrors() });
        }
        /***
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxStudentPlan(SmPlanRecord model)
        {
            //model.Id = Guid.NewGuid();
            model.IsComplete = 2;
            model.IsValid = 1;
            model.PlanTime = DateTime.Now;
            //Request.Params["TargetCountry"];
            if (Request.Params["ddlIntentionalSchoolTop"] != null)
            {
                int IntentionalSchoolTop = model.IntentionalSchoolTop;
                int.TryParse(Request.Params["ddlIntentionalSchoolTop"].ToString(), out IntentionalSchoolTop);
                model.IntentionalSchoolTop = IntentionalSchoolTop;
            }
            
            if (Request.Params["planSort[]"] != null)
            {
                var planSort = Request.Params["planSort[]"].Split(",");
                var planSchoolPlace = Request.Params["planSchoolPlace[]"].Split(",");
                var planGrade = Request.Params["planGrade[]"].Split(",");
                var planPlanningNoteId = Request.Params["planPlanningNoteId[]"].Split(",");
                var planSchoolType = Request.Params["planSchoolType[]"].Split(",");
                var planFee = Request.Params["planFee[]"].Split(",");
                var planAddEnglishScore = Request.Params["planAddEnglishScore[]"].Split(",");
                var planAddLearnScore = Request.Params["planAddLearnScore[]"].Split(",");
                var planAddQualityScore = Request.Params["planAddQualityScore[]"].Split(",");
                var planEnglishScore = Request.Params["planEnglishScore[]"].Split(",");
                var planLearnScore = Request.Params["planLearnScore[]"].Split(",");
                var planQualityScore = Request.Params["planQualityScore[]"].Split(",");

                List<SmStudentPlan> listStudentPlan = new List<SmStudentPlan>();
                SmStudentPlan modelSmStudentPlan;
                for (int i = 0; i < planSort.Length; i++)
                {
                    
                    modelSmStudentPlan = new SmStudentPlan();
                    modelSmStudentPlan.Id = Guid.NewGuid();
                    modelSmStudentPlan.PlanningNoteId = Guid.Parse(planPlanningNoteId[i]);
                    //modelSmStudentPlan.PlanRecordId;
                    modelSmStudentPlan.StudentId = model.StudentId;
                    modelSmStudentPlan.Fee = decimal.Parse(planFee[i]);
                    modelSmStudentPlan.Grade = planGrade[i];
                    modelSmStudentPlan.SchoolPlace = planSchoolPlace[i];
                    modelSmStudentPlan.SchoolType = planSchoolType[i];
                    modelSmStudentPlan.Sort = int.Parse(planSort[i]);
                    modelSmStudentPlan.AddEnglishScore = decimal.Parse(planAddEnglishScore[i]);
                    modelSmStudentPlan.AddLearnScore = decimal.Parse(planAddLearnScore[i]);
                    modelSmStudentPlan.AddQualityScore = decimal.Parse(planAddQualityScore[i]);
                    modelSmStudentPlan.EnglishScore = decimal.Parse(planEnglishScore[i]);
                    modelSmStudentPlan.LearnScore = decimal.Parse(planLearnScore[i]);
                    modelSmStudentPlan.QualityScore = decimal.Parse(planQualityScore[i]);
                    
                    List<SmStudentPlanProgram> listStudentPlanProgram = new List<SmStudentPlanProgram>();
                    if (Request.Params[String.Format("planProgramProgramId{0}[]", modelSmStudentPlan.Sort)] != null)
                    {
                        var planProgramProgramId = Request.Params[String.Format("planProgramProgramId{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        var planProgramName = Request.Params[String.Format("planProgramName{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        var planProgramType = Request.Params[String.Format("planProgramType{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        var planProgramAddEnglishScore = Request.Params[String.Format("planProgramAddEnglishScore{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        var planProgramAddLearnScore = Request.Params[String.Format("planProgramAddLearnScore{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        var planProgramAddQualityScore = Request.Params[String.Format("planProgramAddQualityScore{0}[]", modelSmStudentPlan.Sort)].Split(",");
                        SmStudentPlanProgram modelSmStudentPlanProgram;
                        for (int j = 0; j < planProgramProgramId.Length; j++)
                        {
                            modelSmStudentPlanProgram = new SmStudentPlanProgram();
                            //modelSmStudentPlanProgram.StudentPlanId;
                            modelSmStudentPlanProgram.ProgramId = Guid.Parse(planProgramProgramId[j]);
                            modelSmStudentPlanProgram.Name = planProgramName[j];
                            modelSmStudentPlanProgram.Type = int.Parse(planProgramType[j]);
                            modelSmStudentPlanProgram.AddEnglishScore = decimal.Parse(planProgramAddEnglishScore[j]);
                            modelSmStudentPlanProgram.AddLearnScore = decimal.Parse(planProgramAddLearnScore[j]);
                            modelSmStudentPlanProgram.AddQualityScore = decimal.Parse(planProgramAddQualityScore[j]);
                            listStudentPlanProgram.Add(modelSmStudentPlanProgram);
                        }
                    }
                    
                    modelSmStudentPlan.listStudentPlanProgram = listStudentPlanProgram;
                    listStudentPlan.Add(modelSmStudentPlan);
                    
                }
                model.listStudentPlan = listStudentPlan;
            }
           
            //APlanController bllPlan = new APlanController();
            //var res = bllPlan.CreatePlan(model);
            //if (res.Code != 0)
            //{
            //    return Json(new { success = false, errors = res.Message });
            //}
            return Json(new { success = true, Id = model.Id, errors = GetErrors() });
        }

    ***/
    }
}