using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Plan;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Web.Controllers.SiteSetting;
using XZMY.Manage.Web.Controllers.Sys;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    public class PlanController : ControllerBase
    {
        #region 功能操作
        #region 操作功能
        /// <summary>
        /// 重新规划年级节点
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <returns></returns>
        public bool PlanStudnetGrade(Guid PlanRecordId)
        {
            SchoolTypeController bllSchoolType = new SchoolTypeController();
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            SchoolLevelController bllSchoolLevel = new SchoolLevelController();
            //全部年级节点
            var AllPlanningNote = bllPlanningNote.PlanningNoteGetList(new VmPlanningNote() { });
            var modelPlanRecord = GetSmPlanRecord(PlanRecordId);
            var Education = bllPlanningNote.GetPlanningNote(modelPlanRecord.EducationId);
            //目标
            var modelSchoolType = bllSchoolType.GetModel(modelPlanRecord.SchoolTypeId);
            #region 平均 每年需要成长分值
            int yearlyEnglishScore = ((int)modelSchoolType.EnglishScore - (int)modelPlanRecord.EnglishScore) / (19 - Education.Sort);
            int yearlyLearnScore = ((int)modelSchoolType.LearnScore - (int)modelPlanRecord.LearnScore) / (19 - Education.Sort);
            int yearlyQualityScore = ((int)modelSchoolType.QualityScore - (int)modelPlanRecord.QualityScore) / (19 - Education.Sort);
            #endregion
            EGrade mPGrade = (EGrade)AllPlanningNote.Where(x => x.DataId == modelPlanRecord.EducationId).ToList()[0].Sort;
            ESchoolType mPSchoolType = (ESchoolType)bllSchoolLevel.GetModel(modelPlanRecord.currentSchoolType).Code;


            EGradeAbroad mPGradeAbroad = (EGradeAbroad)AllPlanningNote.Where(x => x.DataId == modelPlanRecord.GoAbroadEducationId).ToList()[0].Sort;
            int mPGeneralBudget = (int)modelPlanRecord.Fee;
            List<VmPlanningNote> listTempNote = GetPlanningNoteStudnetGrade(mPGrade, mPSchoolType, mPGradeAbroad, mPGeneralBudget);
            listTempNote = listTempNote.OrderBy(x => x.Sort).ToList();
            decimal iEnglishScore = modelPlanRecord.EnglishScore;
            decimal iLearnScore = modelPlanRecord.LearnScore;
            decimal iQualityScore = modelPlanRecord.QualityScore;
            List<SmStudentPlan> listStudentPlan = new List<SmStudentPlan>();
            foreach (var TempNote in listTempNote)
            {
                iEnglishScore += yearlyEnglishScore;
                iLearnScore += yearlyLearnScore;
                iQualityScore += yearlyQualityScore;
                #region 组装年级对象添加 到集合内
                SmStudentPlan modelTemp = new SmStudentPlan();
                modelTemp.StudentId = modelPlanRecord.StudentId;
                modelTemp.PlanningNoteId = TempNote.DataId;
                modelTemp.PlanRecordId = modelPlanRecord.DataId;
                modelTemp.Grade = TempNote.Grade;
                modelTemp.SchoolType = TempNote.SchoolType;
                modelTemp.SchoolPlace = TempNote.SchoolPlace;
                modelTemp.Fee = TempNote.Fee;
                modelTemp.EnglishScore = iEnglishScore;
                modelTemp.LearnScore = iLearnScore;
                modelTemp.QualityScore = iQualityScore;
                modelTemp.AddEnglishScore = TempNote.AddEnglishScore;
                modelTemp.AddLearnScore = TempNote.AddLearnScore;
                modelTemp.AddQualityScore = TempNote.AddQualityScore;
                modelTemp.Sort = TempNote.Sort;
                listStudentPlan.Add(modelTemp);
                #endregion
            }
            //删除原来的数据
            DelStudentPlan(PlanRecordId);
            //创建新的
            Guid rId = Guid.Empty;
            foreach (var m in listStudentPlan)
            {
                rId = CreateEditStudentPlan(m);
            }
            if (rId == Guid.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// d
        /// </summary>
        /// <param name="Grade">当前年级</param>
        /// <param name="SchoolType">当前学校类型</param>
        /// <param name="GradeAbroad">出国年级</param>
        /// <param name="GeneralBudget">总预算费用</param>
        /// <returns></returns>
        private List<VmPlanningNote> GetPlanningNoteStudnetGrade(EGrade Grade, ESchoolType SchoolType, EGradeAbroad GradeAbroad, int GeneralBudget)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CommandType cmdType = CommandType.StoredProcedure;
            string cmdText = "P_PlanningNote";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Grade",Grade),
                new SqlParameter("@SchoolType",SchoolType),
                new SqlParameter("@AbroadGrade",GradeAbroad),
                new SqlParameter("@GeneralBudget",GeneralBudget)
            };

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
            List<VmPlanningNote> listVm = new List<VmPlanningNote>();
            foreach (var m in listPN)
            {
                listVm.Add(m.CreateViewModel<PlanningNote, VmPlanningNote>());
            }
            return listVm;
        }
        /// <summary>
        /// 规划历练模板排序
        /// </summary>
        /// <param name="listTemplate"></param>
        /// <param name="SuitablePersonName"></param>
        /// <param name="Route"></param>
        /// <param name="ScoreType"></param>
        /// <returns></returns>
        private List<V_ProjectCourseTemplateList> GetTemplateOrderBy(List<V_ProjectCourseTemplateList> listTemplate,string SuitablePersonName, ERoute Route,String ScoreType)
        {
            List<V_ProjectCourseTemplateList> listSuitablePersonTemplate = new List<V_ProjectCourseTemplateList>();
            if (Route == ERoute.学霸路线)
            {
                switch (ScoreType)
                {
                    case "EnglishScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.DifficultyValue).ToList();
                        break;
                    case "LearnScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.DifficultyValue).ToList();
                        break;
                    case "QualityScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.DifficultyValue).ToList();
                        break;
                    default:
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.DifficultyValue).ToList();
                        break;
                }
            }
            else if (Route == ERoute.经济路线)
            {
                switch (ScoreType)
                {
                    case "EnglishScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.FeeValue).ToList();
                        break;
                    case "LearnScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.FeeValue).ToList();
                        break;
                    case "QualityScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.FeeValue).ToList();
                        break;
                    default:
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.FeeValue).ToList();
                        break;
                }
                
            }
            else
            {
                //稳妥路线 ERoute.稳妥路线
                switch (ScoreType)
                {
                    case "EnglishScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.CompletionValue).ToList();
                        break;
                    case "LearnScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.CompletionValue).ToList();
                        break;
                    case "QualityScore":
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.CompletionValue).ToList();
                        break;
                    default:
                        listSuitablePersonTemplate = listTemplate.OrderByDescending(x => x.CompletionValue).ToList();
                        break;
                }
            }
            return listSuitablePersonTemplate;
        }
        /// <summary>
        /// 重新规划年级节点中的活动课程
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <returns></returns>
        public bool PlanStudentPlanProgram(Guid PlanRecordId)
        {
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            DelStudentPlanProgram(PlanRecordId);
            Guid returnId = Guid.Empty;
            var modelPlanRecord = GetSmPlanRecord(PlanRecordId);
            List<SmStudentPlan> listStudentPlan = GetStudentPlanList(PlanRecordId);
            listStudentPlan = listStudentPlan.OrderBy(x => x.Sort).ToList();
            List<SmStudentPlanProgram> listAllStudentPlanProgram = new List<SmStudentPlanProgram>();
            var listTemplate = GetListProjectCourseTemplate(new V_ProjectCourseTemplateList() {});
            decimal EnglishScore = modelPlanRecord.EnglishScore;
            decimal LearnScore = modelPlanRecord.LearnScore;
            decimal QualityScore = modelPlanRecord.QualityScore;
            foreach (var m in listStudentPlan)
            {
                //decimal EnglishMScore = m.EnglishScore - modelPlanRecord.EnglishScore;
                //decimal mLearnMScore = m.LearnScore - modelPlanRecord.LearnScore;
                //decimal mQualityMScore = m.QualityScore - modelPlanRecord.QualityScore;


                listTemplate = GetListProjectCourseTemplate(new V_ProjectCourseTemplateList() { SuitablePerson = String.Format(",{0},", m.Sort) });
                int countStudentPlanBackup = 0;
                var mspb = GetListStudentPlanBackup(new VmStudentPlanBackup() { StudentPlanId = m.DataId }, out countStudentPlanBackup);
                if (mspb.Count > 0)
                {
                    if (m.SchoolType != mspb[0].SchoolType || m.SchoolPlace != mspb[0].SchoolPlace)
                    {
                        var pNoteOld = bllPlanningNote.PlanningNoteGetList(new VmPlanningNote() { Sort = mspb[0].Sort, SchoolPlace = mspb[0].SchoolPlace, SchoolType = mspb[0].SchoolType });
                        var pNoteNew = bllPlanningNote.PlanningNoteGetList(new VmPlanningNote() { DataId = m.PlanningNoteId });
                        EnglishScore = EnglishScore - (pNoteOld[0].EnglishScore - pNoteNew[0].EnglishScore);
                        LearnScore = LearnScore - (pNoteOld[0].LearnScore - pNoteNew[0].LearnScore);
                        QualityScore = QualityScore - (pNoteOld[0].QualityScore - pNoteNew[0].QualityScore);
                    }
                }
                //规划年级增加分值
                EnglishScore += m.AddEnglishScore;
                LearnScore += m.AddLearnScore;
                QualityScore += m.AddQualityScore;


                List<SmStudentPlanProgram> listStudentPlanProgram = new List<SmStudentPlanProgram>();
                //List<V_ProjectCourseTemplateList> listSuitablePersonTemplate = new List<V_ProjectCourseTemplateList>();
                //#region 路线模板
                
                //#endregion
                #region 加载英语
                if (EnglishScore < m.EnglishScore)
                {
                    var tempListTemplate = GetTemplateOrderBy(listTemplate, m.SuitablePersonName, modelPlanRecord.Route, "EnglishScore");
                    #region 循环添加数据
                    foreach (var mt in tempListTemplate)
                    {
                        if (listAllStudentPlanProgram.Where(x => x.ProgramId == mt.DataId).ToList().Count == 0)
                        {
                            //分值不够还需要填充历练模板
                            if (EnglishScore < m.EnglishScore)
                            {
                                //历练的分值小于需求的分值
                                if (mt.EnglishScore<=((m.EnglishScore - EnglishScore)))
                                {
                                    if (mt.EnglishScore > mt.LearnScore && mt.EnglishScore > 0)
                                    {
                                        SmStudentPlanProgram modelStudentPlanProgram = new SmStudentPlanProgram
                                        {
                                            StudentPlanId = m.DataId,
                                            Type = mt.Type == "ProjectTemplate" ? 1 : 2,
                                            ProgramId = mt.DataId,
                                            Name = mt.Name,
                                            Images = mt.Pictures,
                                            ItemName = mt.ScoreItemNames,
                                            AddEnglishScore = mt.EnglishScore,
                                            AddLearnScore = mt.LearnScore,
                                            AddQualityScore = mt.QualityScore
                                        };
                                        listStudentPlanProgram.Add(modelStudentPlanProgram);
                                        listAllStudentPlanProgram.Add(modelStudentPlanProgram);
                                        EnglishScore += mt.EnglishScore;
                                        LearnScore += mt.LearnScore;
                                        QualityScore += mt.QualityScore;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                    
                }
                #endregion
                #region 加载学术
                if (LearnScore < m.LearnScore)
                {
                    var tempListTemplate = GetTemplateOrderBy(listTemplate, m.SuitablePersonName, modelPlanRecord.Route, "LearnScore");
                    foreach (var mt in tempListTemplate)
                    {
                        if (listAllStudentPlanProgram.Where(x => x.ProgramId == mt.DataId).ToList().Count == 0)
                        {
                            if (LearnScore < m.LearnScore)
                            {
                                //历练的分值小于需求的分值
                                if (mt.LearnScore <= ((m.LearnScore - LearnScore)))
                                {
                                    if (mt.LearnScore > mt.EnglishScore && mt.LearnScore > 0)
                                    {
                                        SmStudentPlanProgram modelStudentPlanProgram = new SmStudentPlanProgram
                                        {
                                            StudentPlanId = m.DataId,
                                            Type = mt.Type == "ProjectTemplate" ? 1 : 2,
                                            ProgramId = mt.DataId,
                                            Name = mt.Name,
                                            Images = mt.Pictures,
                                            ItemName = mt.ScoreItemNames,
                                            AddEnglishScore = mt.EnglishScore,
                                            AddLearnScore = mt.LearnScore,
                                            AddQualityScore = mt.QualityScore
                                        };
                                        listStudentPlanProgram.Add(modelStudentPlanProgram);
                                        listAllStudentPlanProgram.Add(modelStudentPlanProgram);
                                        EnglishScore += mt.EnglishScore;
                                        LearnScore += mt.LearnScore;
                                        QualityScore += mt.QualityScore;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    
                }
                #endregion
                #region 加载素质
                if (QualityScore < m.QualityScore)
                {
                    var tempListTemplate = GetTemplateOrderBy(listTemplate, m.SuitablePersonName, modelPlanRecord.Route, "QualityScore");
                    foreach (var mt in tempListTemplate)
                    {
                        if (listAllStudentPlanProgram.Where(x => x.ProgramId == mt.DataId).ToList().Count == 0)
                        {
                            if (QualityScore < m.QualityScore)
                            {
                                //历练的分值小于需求的分值
                                if (mt.QualityScore <= ((m.QualityScore - QualityScore)))
                                {
                                    if (mt.QualityScore > 0)
                                    {
                                        SmStudentPlanProgram modelStudentPlanProgram = new SmStudentPlanProgram
                                        {
                                            StudentPlanId = m.DataId,
                                            Type = mt.Type == "ProjectTemplate" ? 1 : 2,
                                            ProgramId = mt.DataId,
                                            Name = mt.Name,
                                            Images = mt.Pictures,
                                            ItemName = mt.ScoreItemNames,
                                            AddEnglishScore = mt.EnglishScore,
                                            AddLearnScore = mt.LearnScore,
                                            AddQualityScore = mt.QualityScore
                                        };
                                        listStudentPlanProgram.Add(modelStudentPlanProgram);
                                        listAllStudentPlanProgram.Add(modelStudentPlanProgram);
                                        EnglishScore += mt.EnglishScore;
                                        LearnScore += mt.LearnScore;
                                        QualityScore += mt.QualityScore;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    
                }
                #endregion

                foreach (var mPlanProgram in listStudentPlanProgram)
                {
                    returnId = CreateEditStudentPlanProgram(mPlanProgram);
                }
            }
            if (returnId == Guid.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region 规划信息
        /// <summary>
        /// 获取规划信息 单表
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <returns></returns>
        public SmPlanRecord GetSmPlanRecord(Guid PlanRecordId)
        {
            SmPlanRecord model = new SmPlanRecord();
            var entity = new PlanRecord();
            var service = new GetEntityByIdService<PlanRecord>(PlanRecordId);
            entity = service.Invoke();
            return entity.CreateViewModel<PlanRecord, SmPlanRecord>();
        }
        /// <summary>
        /// 规划列表  学生ID必填
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SmPlanRecord> GetListPlanRecord(SmPlanRecord model)
        {
            List<SmPlanRecord> list = new List<SmPlanRecord>();
            if (model.StudentId == Guid.Empty)
            {
                return list;
            }
            var service = new CustomSearchWithPaginationService<PlanRecord>
            {
                PageIndex = 1,
                PageSize = 9999,

                CustomConditions = new List<CustomCondition<PlanRecord>>
                {
                    new CustomConditionPlus<PlanRecord>
                    {
                        Value = model.StudentId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<PlanRecord, object>>[] { x => x.StudentId }
                    }
                },

                SortMember = new Expression<Func<PlanRecord, object>>[] { x => x.CreatedTime }
            };

            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<PlanRecord, SmPlanRecord>());
            }
            return list;
        }
        /// <summary>
        /// 创建或修改 PlanRecord 规划单表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditPlanRecord(SmPlanRecord model)
        {
            Guid returnId = Guid.Empty;
            if (model != null)
            {
                model.Hobby = model.Hobby ?? "";
                model.JsonQualityScore = model.JsonQualityScore ?? "";
                model.JsonScore = model.JsonScore ?? "";
                model.Planning = model.Planning ?? "";
                model.Remark = model.Remark ?? "";
                model.Skill = model.Skill ?? "";
                model.StudentName = model.StudentName ?? "";
                if((int)model.Route==0)
                {
                    model.Route = Model.Enum.ERoute.稳妥路线;
                }
                
                model.PlanTime = DateTime.Now;
                if (model.EducationId == Guid.Empty)
                {
                    return Guid.Empty;
                }
                if (model.StudentId == Guid.Empty)
                {
                    return Guid.Empty;
                }
                if (model.DataId == Guid.Empty)
                {
                    #region 创建
                    var handler = new BaseCreateHandler<Model.DataModel.Plan.PlanRecord>(model);
                    var res = handler.Invoke();
                    if (res.Success)
                        returnId = res.Output;
                    #endregion
                }
                else
                {
                    #region 修改
                    var handler = new BaseModifyHandler<PlanRecord>(model);
                    var res = handler.Invoke();
                    if (res.Success)
                        returnId = model.DataId;
                    #endregion
                }
            }
            return returnId;
        }
        #endregion
        #region 规划年级
        /// <summary>
        /// 获取指定规划年级对象
        /// </summary>
        /// <param name="Id">规划年级ID</param>
        /// <returns></returns>
        public SmStudentPlan GetModelStudentPlan(Guid Id)
        {
            var service = new GetEntityByIdService<StudentPlan>(Id);
            var entity = service.Invoke() ?? new StudentPlan();
            return entity.CreateViewModel<StudentPlan, SmStudentPlan>();
        }
        /// <summary>
        /// 创建或修改 规划年级节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditStudentPlan(SmStudentPlan model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                #region 创建
                var handler = new BaseCreateHandler<StudentPlan>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;

                    VmStudentPlanBackup modelBackup = model.ConvertTo<VmStudentPlanBackup>();
                    modelBackup.StudentPlanId = returnId;
                    modelBackup.DataId = Guid.Empty;
                    CreateEditStudentPlanBackup(modelBackup);
                }
                #endregion
            }
            else
            {
                #region 修改
                var handler = new BaseModifyHandler<StudentPlan>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = model.DataId;
                #endregion
            }
            return returnId;
        }
        /// <summary>
        /// 删除规划年级 和年级规划的活动
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <returns></returns>
        public bool DelStudentPlan(Guid PlanRecordId)
        {
            bool b = false;
            try
            {
                string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                CommandType cmdType = CommandType.Text;
                string cmdText = "";
                cmdText = String.Format("DELETE from StudentPlanProgram where StudentPlanId in (SELECT Id FROM StudentPlan WHERE PlanRecordId = '{0}')", PlanRecordId);
                T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteNonQuery(connString, cmdType, cmdText);
                cmdText = String.Format("DELETE StudentPlan WHERE PlanRecordId='{0}'", PlanRecordId);
                T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteNonQuery(connString, cmdType, cmdText);
                b = true;
            }
            catch
            {
                b = false;
            }
            return b;
        }
        /// <summary>
        /// 通过规划ID获取规划年级列表
        /// </summary>
        /// <param name="PlanRecordId">规划ID</param>
        /// <returns></returns>
        public List<SmStudentPlan> GetStudentPlanList(Guid PlanRecordId)
        {
            var service = new CustomSearchWithPaginationService<StudentPlan>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<StudentPlan>>()
                {
                    new CustomConditionPlus<StudentPlan>()
                    {
                        Value = PlanRecordId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentPlan, object>>[] { x => x.PlanRecordId}
                    }
                },
                SortMember = new Expression<Func<StudentPlan, object>>[] { x => x.Sort }
            };
            var result = service.Invoke();
            List<SmStudentPlan> modelList = new List<SmStudentPlan>();
            foreach (var m in result.Results)
            {
                SmStudentPlan modelStudentPlan = m.CreateViewModel<Model.DataModel.Plan.StudentPlan, SmStudentPlan>();
                modelList.Add(modelStudentPlan);
            }
            return modelList;
        }
        #endregion
        #region 规划活动
        /// <summary>
        /// 获取当前以及规划好的年级活动项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SmStudentPlanProgram> StudentPlanProgramGetList(SmStudentPlanProgram model)
        {
            List<SmStudentPlanProgram> modelList = new List<SmStudentPlanProgram>();
            if (model.StudentPlanId == Guid.Empty)
            {
                return modelList;
            }

            var service = new CustomSearchWithPaginationService<StudentPlanProgram>
            {
                PageIndex = 1,
                PageSize = 99999,
                CustomConditions = new List<CustomCondition<StudentPlanProgram>>()
                {
                    new CustomConditionPlus<StudentPlanProgram>()
                    {
                        Value = model.StudentPlanId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.StudentPlanId}
                    }
                },
                SortMember = new Expression<Func<StudentPlanProgram, object>>[] { x => x.CreatedTime }
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.DataId }
                });
            }
            if (model.ProgramId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = model.ProgramId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.ProgramId }
                });
            }
            if (model.Type > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = model.Type,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.Type }
                });
            }
            if (!String.IsNullOrEmpty( model.ItemName ))
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanProgram>
                {
                    Value = model.ItemName,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<StudentPlanProgram, object>>[] { x => x.ItemName }
                });
            }

           
            var result = service.Invoke();

            foreach (var m in result.Results)
            {
                SmStudentPlanProgram modelStudentPlanProgram = m.CreateViewModel<StudentPlanProgram, SmStudentPlanProgram>();
                modelList.Add(modelStudentPlanProgram);
            }
            return modelList;
        }
        /// <summary>
        /// 删除指定规划的全部规划活动
        /// </summary>
        /// <param name="PlanRecordId"></param>
        /// <returns></returns>
        public bool DelStudentPlanProgram(Guid PlanRecordId)
        {
            bool b = false;
            try
            {
                string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                CommandType cmdType = CommandType.Text;
                string cmdText = "";
                cmdText = String.Format("DELETE from StudentPlanProgram where StudentPlanId in (SELECT Id FROM StudentPlan WHERE PlanRecordId = '{0}')", PlanRecordId);
                T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteNonQuery(connString, cmdType, cmdText);
                b = true;
            }
            catch
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// 创建或修改 规划年级的活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditStudentPlanProgram(SmStudentPlanProgram model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                #region 创建
                var handler = new BaseCreateHandler<StudentPlanProgram>(model);
                var res = handler.Invoke();
                if (res.Success)
                    returnId = res.Output;
                #endregion
            }
            return returnId;
        }
        /// <summary>
        /// 删除指定规划活动
        /// </summary>
        /// <param name="StudentPlanProgramId">规划活动ID</param>
        /// <returns></returns>
        public void DeleteStudentPlanProgram(Guid StudentPlanProgramId)
        {
            var service = new BaseDeleteService<StudentPlanProgram>(StudentPlanProgramId);
            service.Invoke();
        }
        #endregion
        #region 活动模板
        /// <summary>
        /// 获取全部的模板集合
        /// </summary>
        /// <returns></returns>
        public List<V_ProjectCourseTemplateList> GetListProjectCourseTemplate(V_ProjectCourseTemplateList model)
        {
            var service = new CustomSearchWithPaginationService<V_ProjectCourseTemplateList>
            {
                PageIndex = 1,
                PageSize = 9999999,
                CustomConditions = new List<CustomCondition<V_ProjectCourseTemplateList>>()
                {
                    new CustomConditionPlus<V_ProjectCourseTemplateList>()
                    {
                        Value = 1,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_ProjectCourseTemplateList, object>>[] { x => x.State}
                    }
                },
                SortMember = new Expression<Func<V_ProjectCourseTemplateList, object>>[] { x => x.CreatedTime }
            };
            if (!String.IsNullOrEmpty(model.SuitablePerson))
            {
                service.CustomConditions.Add(new CustomConditionPlus<V_ProjectCourseTemplateList>
                {
                    Value = model.SuitablePerson,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<V_ProjectCourseTemplateList, object>>[] { x => x.SuitablePerson }
                });
            }
            var result = service.Invoke();
            return result.Results.ToList();
        }
        #endregion
        #region 规划年级模板
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmStudentPlanBackup GetModelStudentPlanBackup(Guid Id)
        {
            var service = new GetEntityByIdService<StudentPlanBackup>(Id);
            var entity = service.Invoke() ?? new StudentPlanBackup();
            return entity.CreateViewModel<StudentPlanBackup, VmStudentPlanBackup>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEditStudentPlanBackup(VmStudentPlanBackup model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<StudentPlanBackup>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<StudentPlanBackup>(model);
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
        public void DeleteStudentPlanBackup(Guid Id)
        {
            var service = new BaseDeleteService<StudentPlanBackup>(Id);
            service.Invoke();
        }

        public List<VmStudentPlanBackup> GetListStudentPlanBackup(VmStudentPlanBackup model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<StudentPlanBackup>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<StudentPlanBackup>>
                    {
                        new CustomConditionPlus<StudentPlanBackup>
                        {
                            Value = model.Grade,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<StudentPlanBackup, object>>[]
                            {
                                m => m.Grade
                            },
                        }
                    },
                SortMember = new Expression<Func<StudentPlanBackup, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanBackup>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanBackup, object>>[] { x => x.DataId }
                });
            }
            if (model.StudentPlanId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<StudentPlanBackup>
                {
                    Value = model.StudentPlanId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<StudentPlanBackup, object>>[] { x => x.StudentPlanId }
                });
            }
            var result = service.Invoke();
            List<VmStudentPlanBackup> list = new List<VmStudentPlanBackup>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentPlanBackup, VmStudentPlanBackup>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        #endregion
    }
}