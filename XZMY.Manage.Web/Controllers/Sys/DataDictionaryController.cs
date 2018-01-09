using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Program;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Service.Weixin;
using XZMY.Manage.Web.Controllers.Program;
using XZMY.Manage.Web.Controllers.SiteSetting;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class DataDictionaryController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "系统参数配置", Code = "SystemList", ModuleCode = "SYSTEM", Url = "/DataDictionary/Index", Visible = true)]
        public ActionResult Index()
        {
            ViewData["AccessToken"] = AccessTokenService.GetAccessToken();
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 数据字典分类
        /// </summary>
        /// <returns></returns>
        public ActionResult CatagoriesList()
        {
            return View();
        }
        /// <summary>
        /// 数据字典页面
        /// </summary>
        /// <param name="CatagoryKey"></param>
        /// <returns></returns>
        public ActionResult DataDictionaryItemList(String CatagoryKey)
        {

            return View();
        }
        /// <summary>
        /// AJAX 获取数据字典
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CatagoryKey">数据字典类型KEY</param>
        /// <returns></returns>
        public ActionResult AjaxDataDictionaryItemList(VmSearchBase model, String CatagoryKey, Int32? State)
        {
            int iState = 1;
            if (State.HasValue)
            {
                iState = State.Value;
            }
            List<DataDictionaryItem> listItem = new List<DataDictionaryItem>();
            var listCatagory = DataDictionaryManager.GetCatagories(m => m.Key == CatagoryKey);
            Dictionary<string, List<DataDictionaryItem>> list = new Dictionary<string, List<DataDictionaryItem>>();
            foreach (var file in listCatagory)
            {
                var dict = file.Value.Select(x => x.Value).ToList().Where(x => x.State == iState).ToList();
                list.Add(file.Key, dict);
                listItem = dict;
            }
            return Json(new { success = true, total = listItem.Count, rows = listItem, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加或编辑数据字典 页面
        /// </summary>
        /// <param name="CatagoryKey"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DataDictionaryItemEdit(String CatagoryKey, Guid? Id)
        {
            DataDictionaryItem model = new DataDictionaryItem();
            if (Id.HasValue)
            {
                model = DataDictionaryManager.GetDataById(CatagoryKey, Id.Value);
            }
            else
            {
                //model.Id = Guid.NewGuid();
            }
            return View(model);
        }

        public ActionResult AjaxDataDictionaryItem(DataDictionaryItem model)
        {
            String CatagoryKey = "";
            int State = 1;
            if (Request.Params["CatagoryKey"] != null)
            {
                CatagoryKey = Request.Params["CatagoryKey"].ToString();
            }
            if (Request.Params["State"] != null)
            {
                int.TryParse(Request.Params["State"].ToString(), out State);
            }
            model.IsDefault = true;
            model.IsSystem = true;
            model.State = State;
            model.Sort = 0;
            bool b = DataDictionaryManager.SaveOrUpdateData(CatagoryKey, model);

            if (b)
            {
                return Json(new { success = b, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = b, Id = model.DataId, errors = GetErrors() });
            }
        }

        public ActionResult DefaultDataPage()
        {
            DefaultData();
            return Redirect("/DataDictionary/Index/");
        }

        /// <summary>
        /// 恢复默认数据 初始化数据
        /// </summary>
        public void DefaultData()
        {
            return;
            int TotalCount = 0;
            GradeRankingController bllGradeRanking = new GradeRankingController();
            SchoolLevelController bllSchoolLevel = new SchoolLevelController();
            ProgramAbilityController bllProgramAbility = new ProgramAbilityController();
            EnglishScoreDefaultController bllEnglishScoreDefault = new EnglishScoreDefaultController();
            LearnScoreDefaultController bllLearnScoreDefault = new LearnScoreDefaultController();
            PlanningNoteController bllPlanningNote = new PlanningNoteController();
            #region program_Ability 活动课程历练的能力名称及描述表
            var listProgramAbility = bllProgramAbility.GetList(new Model.ViewModel.Program.VmProgramAbility() { }, out TotalCount);
            foreach (var m in listProgramAbility)
            {
                bllProgramAbility.Delete(m.DataId);
            }
            VmProgramAbility modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.活动;
            modelProgramAbility.Name = "素质能力";
            modelProgramAbility.Description = "素质能力描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);

            modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.课程;
            modelProgramAbility.Name = "学术能力";
            modelProgramAbility.Description = "学术能力描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);

            modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.课程;
            modelProgramAbility.Name = "英语能力";
            modelProgramAbility.Description = "英语能力描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);

            modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.课程;
            modelProgramAbility.Name = "升学准备";
            modelProgramAbility.Description = "升学准备描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);

            modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.课程;
            modelProgramAbility.Name = "兴趣职业";
            modelProgramAbility.Description = "兴趣职业描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);

            modelProgramAbility = new VmProgramAbility();
            modelProgramAbility.Type = EProgramType.课程;
            modelProgramAbility.Name = "其他活动";
            modelProgramAbility.Description = "其他活动描述";
            bllProgramAbility.CreateEdit(modelProgramAbility);
            #endregion
            #region GradeRanking 年级排名
            var listGradeRanking = bllGradeRanking.GetList();
            foreach (var m in listGradeRanking)
            {
                bllGradeRanking.Delete(m.DataId);
            }
            VmGradeRanking modelGradeRanking = new VmGradeRanking();
            modelGradeRanking.Name = "前10名";
            bllGradeRanking.CreateEdit(modelGradeRanking);

            modelGradeRanking = new VmGradeRanking();
            modelGradeRanking.Name = "11到20名";
            bllGradeRanking.CreateEdit(modelGradeRanking);

            modelGradeRanking = new VmGradeRanking();
            modelGradeRanking.Name = "21到30名";
            bllGradeRanking.CreateEdit(modelGradeRanking);

            modelGradeRanking = new VmGradeRanking();
            modelGradeRanking.Name = "31到40名";
            bllGradeRanking.CreateEdit(modelGradeRanking);

            modelGradeRanking = new VmGradeRanking();
            modelGradeRanking.Name = "40名以后";
            bllGradeRanking.CreateEdit(modelGradeRanking);
            #endregion
            #region SchoolLevel 学校等级
            var listSchoolLevel = bllSchoolLevel.GetList();
            foreach (var m in listSchoolLevel)
            {
                bllSchoolLevel.Delete(m.DataId);
            }
            VmSchoolLevel modelSchoolLevel = new VmSchoolLevel();
            modelSchoolLevel.Name = "普通学校";
            modelSchoolLevel.Code = 1;
            modelSchoolLevel.Description = "按照国家规定的设置标准和审批程序批准举办的学校";
            modelSchoolLevel.Score = 0.8M;
            modelSchoolLevel.State = Model.Enum.EState.启用;
            bllSchoolLevel.CreateEdit(modelSchoolLevel);

            modelSchoolLevel = new VmSchoolLevel();
            modelSchoolLevel.Name = "省重点学校";
            modelSchoolLevel.Code = 2;
            modelSchoolLevel.Description = "按照国家规定的设置标准和审批程序批准举办的学校";
            modelSchoolLevel.Score = 0.9M;
            modelSchoolLevel.State = Model.Enum.EState.启用;
            bllSchoolLevel.CreateEdit(modelSchoolLevel);

            modelSchoolLevel = new VmSchoolLevel();
            modelSchoolLevel.Name = "国家重点学校";
            modelSchoolLevel.Code = 3;
            modelSchoolLevel.Description = "按照国家规定的设置标准和审批程序批准举办的学校";
            modelSchoolLevel.Score = 1M;
            modelSchoolLevel.State = Model.Enum.EState.启用;
            bllSchoolLevel.CreateEdit(modelSchoolLevel);
            #endregion
            #region EnglishScoreDefault 规划师英语默认成绩
            var listEnglishScoreDefault = bllEnglishScoreDefault.GetList();
            foreach (var m in listEnglishScoreDefault)
            {
                bllEnglishScoreDefault.Delete(m.DataId);
            }
            VmEnglishScoreDefault modelEnglishScoreDefault = new VmEnglishScoreDefault();
            var listEnglishScoreDefaultPlanningNote = bllPlanningNote.PlanningNoteGetList(new Model.ViewModel.Plan.VmPlanningNote() { SchoolTypeId = 1 });
            var listEnglishScoreDefaultGradeRanking = bllGradeRanking.GetList();
            foreach (var m in listEnglishScoreDefaultPlanningNote)
            {
                foreach (var n in listEnglishScoreDefaultGradeRanking)
                {
                    modelEnglishScoreDefault = new VmEnglishScoreDefault();
                    modelEnglishScoreDefault.PlanningNoteId = m.DataId;
                    modelEnglishScoreDefault.GradeName = m.Grade;
                    modelEnglishScoreDefault.Sort = m.Sort;
                    modelEnglishScoreDefault.GradeRankingId = n.DataId;
                    modelEnglishScoreDefault.GradeRankingName = n.Name;
                    modelEnglishScoreDefault.EnglishScore = 10 + m.Sort;
                    bllEnglishScoreDefault.CreateEdit(modelEnglishScoreDefault);
                }
            }
            #endregion
            #region LearnScoreDefault 规划师素质默认成绩
            var listLearnScoreDefault = bllLearnScoreDefault.GetList();
            foreach (var m in listSchoolLevel)
            {
                bllLearnScoreDefault.Delete(m.DataId);
            }
            VmLearnScoreDefault modelLearnScoreDefault = new VmLearnScoreDefault();
            var listLearnScoreDefaultSchoolLevel = bllSchoolLevel.GetList();
            var listLearnScoreDefaultGradeRanking = bllGradeRanking.GetList();
            foreach (var m in listLearnScoreDefaultSchoolLevel)
            {
                foreach (var n in listEnglishScoreDefaultGradeRanking)
                {
                    modelLearnScoreDefault = new VmLearnScoreDefault();
                    modelLearnScoreDefault.SchoolLevelId = m.DataId;
                    modelLearnScoreDefault.SchoolLevelName = m.Name;
                    modelLearnScoreDefault.GradeRankingId = n.DataId;
                    modelLearnScoreDefault.GradeRankingName = n.Name;
                    modelLearnScoreDefault.LearnScore = 20;
                    bllLearnScoreDefault.CreateEdit(modelLearnScoreDefault);
                }
            }
            #endregion
        }

    }
}