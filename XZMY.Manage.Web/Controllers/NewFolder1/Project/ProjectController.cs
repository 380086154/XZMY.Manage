using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Project;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Attributes;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.Utils.Helper;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.DataModel.Planners;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.Utils.ADONET.SQLServer;
using XZMY.Manage.Web.Controllers.SiteSetting;
using XZMY.Manage.Model.ViewModel.SiteSetting;

namespace XZMY.Manage.Web.Controllers.Project
{
    //项目
    public class ProjectController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "活动列表", Code = "ProjectList", ModuleCode = "PROJECT", Url = "/Project/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        [AutoCreateAuthAction(Name = "创建活动", Code = "ProjectEdit", ModuleCode = "PROJECT", Url = "/Project/Edit", Visible = true)]
        public ActionResult Edit(Guid? id, Guid? TemplateId)
        {
            var entity = new Model.DataModel.Project.Project();
            if (TemplateId.HasValue)
            {
                //获取模板数据
                var service = new GetEntityByIdService<ProjectTemplate>(TemplateId.Value);
                var modelProjectTemplate = service.Invoke();
                //填充到活动页面中
                entity.ProjectTemplateId = modelProjectTemplate.DataId;
                entity.Name = modelProjectTemplate.Name;
                entity.Code = modelProjectTemplate.Code;
                entity.Pictures = modelProjectTemplate.Pictures;
                entity.ProjectTypeId = modelProjectTemplate.ProjectTypeId;
                entity.ProjectTypeName = modelProjectTemplate.ProjectTypeName;
                entity.ProjectPlaceLocationId = modelProjectTemplate.ProjectTemplatePlaceLocationId;
                entity.ProjectPlaceName = modelProjectTemplate.ProjectTemplatePlaceName;
                entity.Sponsor = modelProjectTemplate.Sponsor;
                entity.RecommendedIndex = modelProjectTemplate.RecommendedIndex;
                entity.SuitablePerson = modelProjectTemplate.SuitablePerson;
                entity.Service = modelProjectTemplate.Service;
                entity.MarketPrice = modelProjectTemplate.MarketPrice;
                entity.ActualPrice = modelProjectTemplate.ActualPrice;
                entity.Discount = modelProjectTemplate.Discount;
                entity.DepositPrice = modelProjectTemplate.DepositPrice;
                entity.DifficultyValue = modelProjectTemplate.DifficultyValue;
                entity.CompletionValue = modelProjectTemplate.CompletionValue;
                entity.FeeValue = modelProjectTemplate.FeeValue;
                entity.EnglishScore = modelProjectTemplate.EnglishScore;
                entity.LearnScore = modelProjectTemplate.LearnScore;
                entity.QualityScore = modelProjectTemplate.QualityScore;
                //
                entity.ProjectDescription = modelProjectTemplate.ProjectTemplateDescription;
                entity.Schedule = modelProjectTemplate.Schedule;
                entity.Fee = modelProjectTemplate.Fee;
                entity.Stay = modelProjectTemplate.Stay;
                entity.Visa = modelProjectTemplate.Visa;
                entity.Stroke = modelProjectTemplate.Stroke;
                entity.Security = modelProjectTemplate.Security;


                entity.ActualPrice = modelProjectTemplate.ActualPrice;
            }

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Model.DataModel.Project.Project>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Model.DataModel.Project.Project, VmProjectEdit>());
        }
        public ActionResult Details(Guid? id)
        {
            var entity = new Model.DataModel.Project.Project();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Model.DataModel.Project.Project>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Model.DataModel.Project.Project, VmProjectEdit>());
        }

        /// <summary>
        /// 修改活动状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxUpdateProjectState(Guid id, int state)
        {
            var service = new GetEntityByIdService<Model.DataModel.Project.Project>(id);
            var entity = service.Invoke();
            if (entity != null && entity.DataId != Guid.Empty)
            {
                entity.State = (EState)state;
                var model = entity.CreateViewModel<Model.DataModel.Project.Project, VmProjectEdit>();
                var handler = new BaseModifyHandler<Model.DataModel.Project.Project>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    return Json(new { success = true, Id = entity.DataId, errors = GetErrors() });
                }
            }
            return Json(new { success = false, errors = GetErrors() });
        }

        /// <summary>
        /// 通过ID获取活动信息对象
        /// </summary>
        /// <param name="ProjectId">活动ID</param>
        /// <returns></returns>
        public VmProjectEdit GetVmProject(Guid ProjectId)
        {
            var service = new GetEntityByIdService<Model.DataModel.Project.Project>(ProjectId);
            var entity = service.Invoke();
            return entity.CreateViewModel<Model.DataModel.Project.Project, VmProjectEdit>();
        }

        //用户 创建/编辑 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmProjectEdit model)
        {
            //if (ModelState.IsValid)
            // {

            bool bSuccess = false;
            Guid ReturnId = Guid.Empty;
            model.SuitablePerson = string.Format(",{0},", Request.Params["dllFitGrade[]"]);
            model.ScoreItemNames = Request.Form["ckScoreItemNames[]"];
            model.DifficultyValue = Request.Params["dllDifficultyValue"].ToInt32(5);
            model.CompletionValue = 10 - model.DifficultyValue;
            model.FeeValue = Request.Params["dllFeeValue"].ToInt32(5);
            model.State = (EState)Request.Params["State"].ToInt32(1);
            model.Service = Request.Form["chkService[]"];
            model.QualityScore = Request.Params["QualityScore"].ToDecimal(0M);
            


            if (Request.Params["dllCountry"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectPlaceLocationId = Request.Params["dllCountry"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllProvince"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectPlaceLocationId = Request.Params["dllProvince"].ToGuid(Guid.Empty);
            }
            if (Request.Params["dllCity"].ToGuid(Guid.Empty) != Guid.Empty)
            {
                model.ProjectPlaceLocationId = Request.Params["dllCity"].ToGuid(Guid.Empty);
            }

            model.ProjectPlaceName = model.ProjectPlaceName.Replace(",--请选择--", "").Replace(",请选择", "");
            if (model.DataId == Guid.Empty)
            {
                Guid OutputId = Guid.Empty;
                var handler = new BaseCreateHandler<Model.DataModel.Project.Project>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {

                    return Json(new { success = false, errors = GetErrors() });
                }
                else {
                    OutputId = res.Output;
                    bSuccess = true;
                    ReturnId = OutputId;
                }
                SaveProjectDate(model.DataId);

                if (model.State == EState.启用)
                {

                    //活动推送
                    CreateProgramMessage(new ProgramMessage
                    {
                        Message = "新的历练:" + model.Name,
                        ProgramId = OutputId,
                        ProgramType = EProgramType.活动,
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
                bSuccess = res.Success;
                ReturnId = model.DataId;
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Project.Project>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    bSuccess = false;
                    ReturnId = Guid.Empty;
                }
                else
                {
                    bSuccess = true;
                    ReturnId = model.DataId;
                }
                SaveProjectDate(model.DataId);
            }
            #region 分值
            ScoresController bllScores = new ScoresController();
            String strScoreItemValueList = Request.Params["ScoreItemValueList"].ToString();
            var listItem = strScoreItemValueList.Split("|");
            foreach (var Item in listItem)
            {
                if (!String.IsNullOrEmpty(Item))
                {
                    var ItemValue = Item.Split(",");
                    VmScoresEdit modelScores = new VmScoresEdit();
                    modelScores.DataId = ItemValue[0].ToGuid(Guid.Empty);
                    modelScores.ScoreItemsId = ItemValue[2].ToGuid(Guid.Empty);
                    modelScores.ScoreItemsName = ItemValue[3];
                    modelScores.SourceId = ReturnId;
                    modelScores.SourceType = "Project";
                    modelScores.ScoreValue = ItemValue[1].ToDecimal(0M);
                    bllScores.CreateEdit(modelScores);
                }
            }
            #endregion
            #region 出发日期和出发地点
            //DateTimeDepartureCityList 主键ID_开始时间_结束时间_出发地点|
            string DateTimeDepartureCity = Request.Params["DateTimeDepartureCityList"];
            ProjectDateController bllProjectDate = new ProjectDateController();
            int OldTotalCount = 0;
            var OldList = bllProjectDate.GetList(new VmProjectDate() { ProjectId = ReturnId }, out OldTotalCount);
            foreach (var m in OldList)
            {
                bllProjectDate.Delete(m.DataId);
            }

            var mList = DateTimeDepartureCity.Split("|");
            foreach (var mRow in mList)
            {
                if (!String.IsNullOrEmpty(mRow))
                {
                    var mCol = mRow.Split("_");
                    if (mCol.Length > 3)
                    {
                        if (mCol[1].ToDateTime(DateTime.MaxValue) != DateTime.MaxValue)
                        {
                            VmProjectDate modelNewProjectDate = new VmProjectDate();
                            modelNewProjectDate.ProjectId = ReturnId;
                            modelNewProjectDate.BeginDate = mCol[1].ToDateTime(DateTime.Now);
                            modelNewProjectDate.EndDate = mCol[2].ToDateTime(DateTime.Now);
                            modelNewProjectDate.DepartureCity = mCol[3];
                            bllProjectDate.CreateEdit(modelNewProjectDate);
                        }
                    }
                }
            }
            #endregion

            return Json(new { success = bSuccess, Id = ReturnId, errors = GetErrors() });
        }

        //保存时间
        public void SaveProjectDate(Guid id)
        {
            var beginDate = (Request.Form["BeginDate[]"] ?? "").Split(',');
            var endDate = (Request.Form["EndDate[]"] ?? "").Split(',');
            var departureCity = (Request.Form["DepartureCity[]"] ?? "").Split(',');
            var projectTimeId = (Request.Form["ProjectTimeId[]"] ?? "").Split(',');

            var list = new List<VmProjectDate>();
            for (var i = 0; i < projectTimeId.Length; i++)
            {
                if (projectTimeId[i].ToGuid(Guid.Empty) != Guid.Empty)
                {
                    var vm = new VmProjectDate
                    {
                        DataId = projectTimeId[i].ToGuid(Guid.Empty),
                        ProjectId = id,
                        BeginDate = beginDate[i].ToDateTime(DateTimePlus.GetMinDateTime()),
                        EndDate = endDate[i].ToDateTime(DateTimePlus.GetMinDateTime()),
                        DepartureCity = departureCity[i] ?? ""
                    };

                    list.Add(vm);
                }
            }

            var handler = new ProjectDateRefreshHandler(list);
            var res = handler.Invoke();
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmProjectEdit model)
        {
            var service = new CustomSearchWithPaginationService<ProjectList>
            {
                PageIndex = model.PageIndex==0?1: model.PageIndex,
                PageSize = model.PageSize == 0 ? 10 : model.PageSize,
                CustomConditions = new List<CustomCondition<ProjectList>>()
                {
                    new CustomConditionPlus<ProjectList>()
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<ProjectList, object>>[] { x => x.ProjectName }
                    }
                },
                SortMember = new Expression<Func<ProjectList, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProjectList>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProjectList, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();
            
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(VmProjectEdit model)
        {
            var handler = new ProjectCreateHandler(model);
            handler.Invoke();

            return View();
        }
        public ActionResult Modify(VmProjectEdit model)
        {
            var handler = new BaseModifyHandler<Model.DataModel.Project.Project>(model);
            handler.Invoke();

            return View();
        }
        public ActionResult Enable(Guid id)
        {
            var service = new BaseSetSingleStateService<Model.DataModel.Project.Project, int>()
            {
                ModelId = id,
                Value = 1,
                Member = m => m.State
            };
            service.Invoke();

            return View();
        }
        public ActionResult Disable(Guid id)
        {
            var service = new BaseSetSingleStateService<Model.DataModel.Project.Project, int>()
            {
                ModelId = id,
                Value = 2,
                Member = m => m.State
            };
            service.Invoke();

            return View();
        }
        public ActionResult CreateDate(VmProjectDate model)
        {
            var handler = new BaseCreateHandler<ProjectDate>(model);
            handler.Invoke();

            return View();
        }
        public ActionResult ModifyDate(VmProjectDate model)
        {
            var handler = new BaseModifyHandler<ProjectDate>(model);
            handler.Invoke();

            return View();
        }
        public ActionResult DeleteDate(Guid id)
        {
            var service = new BaseDeleteService<ProjectDate>(id);
            service.Invoke();

            return View();
        }

        /// <summary>
        /// 推送活动或课程
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回执行SQL受影响的行数</returns>
        public Int32 CreateProgramMessage(ProgramMessage model)
        {
            string cmdText = string.Format(@"
            INSERT INTO [Coastline].[dbo].[ProgramMessage]
                       ([DataId]
                       ,[MemberId]
                       ,[StudentId]
                       ,[Message]
                       ,[ProgramId]
                       ,[ProgramType]
                       ,[PlannerId]
                       ,[PlannerName]
                       ,[IsRead]
                       ,[MessageTime]
                       ,[MessageType]
                       ,[CreatedTime]
                       ,[CreatorId]
                       ,[CreatorName]
                       ,[ModifiedTime]
                       ,[ModifierId]
                       ,[ModifierName])
               SELECT 
               NEWID(),
               MemberId,
               Id,
               @Message,
               @ProgramId,
               @ProgramType,
               @PlannerId,
               @PlannerName,
               @IsRead,
               @MessageTime,
               @MessageType,
               @CreatedTime,
               @CreatorId,
               @CreatorName,  
               @ModifiedTime,
               @ModifierId,
               @ModifierName
                FROM dbo.Student ");

            var sqlParams = new SqlParameter[] {
                new SqlParameter("@Message",SqlDbType.NVarChar) { Value=model.Message},
                new SqlParameter("@ProgramId",SqlDbType.UniqueIdentifier) { Value=model.ProgramId},
                new SqlParameter("@ProgramType",SqlDbType.Int) { Value=model.ProgramType.GetHashCode()},
                new SqlParameter("@PlannerId",SqlDbType.UniqueIdentifier) { Value=model.PlannerId},
                new SqlParameter("@PlannerName",SqlDbType.NVarChar) { Value=model.PlannerName},
                new SqlParameter("@IsRead",SqlDbType.Bit) { Value=model.IsRead},
                new SqlParameter("@MessageTime",SqlDbType.DateTime) { Value=model.MessageTime},
                new SqlParameter("@MessageType",SqlDbType.Int) { Value=model.MessageType.GetHashCode()},
                new SqlParameter("@CreatedTime",SqlDbType.DateTime) { Value=model.CreatedTime},
                //new SqlParameter("@CreatorId",SqlDbType.UniqueIdentifier) { Value=model.CreatorId},
                //new SqlParameter("@CreatorName",SqlDbType.NVarChar) { Value=model.CreatorName},
                //new SqlParameter("@ModifiedTime",SqlDbType.DateTime) { Value=model.ModifiedTime},
                //new SqlParameter("@ModifierId",SqlDbType.UniqueIdentifier) { Value=model.ModifierId},
                //new SqlParameter("@ModifierName",SqlDbType.NVarChar) { Value=model.ModifierName}
            };
            return SqlServerHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, CommandType.Text, cmdText, sqlParams);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TempProjectDateJson : VmProjectDate
    {
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; }
    }
}