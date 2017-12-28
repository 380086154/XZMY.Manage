using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2M.Common.DataServiceComponents.Service;
using Newtonsoft.Json;
using XZMY.Manage.Service.Handlers;
namespace XZMY.Manage.Web.Content.Custom
{
    /// <summary>
    /// SiteSetting 的摘要说明
    /// </summary>
    public class SiteSetting : IHttpHandler
    {
        public string result = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["action"];

            switch (type)
            {
                
                case "GetScoreItems"://查询到全部分值项目
                    GetScoreItems(context);
                    break;
                case "GetScores"://查询到全部分值项目
                    GetScores(context);
                    break;
                case "AddScores"://添加项目分值
                    AddScores(context);
                    break;
                case "GetProjectTimeProjectId"://添加活动时间
                    GetProjectTimeProjectId(context);
                    break;
                case "SaveProjectTime"://保存活动时间
                    SaveProjectTime(context);
                    break;
                case "GetCourseTimeCourseId"://添加活动时间
                    GetCourseTimeCourseId(context);
                    break;
                case "SaveCourseTime"://保存活动时间
                    SaveCourseTime(context);
                    break;
                default:
                    //Uploader(context);
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }
        private void GetScoreItems(HttpContext context)
        {
            int Type = int.Parse(context.Request["Type"].ToString());
            var service = new GetEntityBySingleColumnService<Model.DataModel.SiteSetting.ScoreItems>()
            {
                ColumnMember = m => m.Type,
                ColumnValue = Type
            };
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        private void GetScores(HttpContext context)
        {
            Guid SourceId = Guid.Parse(context.Request["SourceId"].ToString());

            IList<Model.DataModel.SiteSetting.Scores> listScores = new List<Model.DataModel.SiteSetting.Scores>();
            var service = new GetEntityBySingleColumnService<Model.DataModel.SiteSetting.Scores>()
            {
                ColumnMember = m => m.SourceId,
                ColumnValue = SourceId
            };
            
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        /// <summary>
        /// 添加项目分值
        /// </summary>
        /// <param name="context"></param>
        private void AddScores(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (context.Request["Id"] != null)
            {
                Id = Guid.Parse(context.Request["Id"].ToString());
            }
            Guid SourceId = Guid.Parse(context.Request["SourceId"].ToString());
            var SourceType = context.Request["SourceType"].ToString();
            var ScoreItemsId = context.Request["ScoreItemsId"].ToString();
            var ScoreItemsName = context.Request["ScoreItemsName"].ToString();
            var Score = context.Request["ScoreValue"].ToString();
            string[] ScoreItemsIds = ScoreItemsId.Split(",");
            string[] ScoreItemsNames = ScoreItemsName.Split(",");
            string[] Scores = Score.Split(",");
            for (int i = 0; i < ScoreItemsIds.Length; i++)
            {
                if (!String.IsNullOrEmpty(ScoreItemsIds[i]))
                {
                    Model.ViewModel.SiteSetting.VmScoresEdit modelScore = new Model.ViewModel.SiteSetting.VmScoresEdit();
                    modelScore.DataId = Id;
                    modelScore.SourceId = SourceId;
                    modelScore.SourceType = SourceType;
                    modelScore.ScoreItemsId = Guid.Parse(ScoreItemsIds[i]);
                    modelScore.ScoreItemsName = ScoreItemsNames[i];
                    modelScore.ScoreValue = decimal.Parse(Scores[i]);
                    AddScoresData(modelScore);
                }
            }
        }
        /// <summary>
        /// 保存积分数据
        /// </summary>
        /// <param name="modelScores"></param>
        private void AddScoresData(Model.ViewModel.SiteSetting.VmScoresEdit modelScores)
        {
            if (modelScores.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.SiteSetting.Scores>(modelScores);
                handler.Invoke();
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.SiteSetting.Scores>(modelScores);
                handler.Invoke();
            }
        }

        /// <summary>
        /// 获取活动时间和出发城市
        /// </summary>
        /// <param name="context"></param>
        private void GetCourseTimeCourseId(HttpContext context)
        {
            Guid CourseId =  Guid.Parse(context.Request["CourseId"].ToString());
            var service = new GetEntityBySingleColumnService<Model.DataModel.Courses.CourseDate>()
            {
                ColumnMember = m => m.CourseId,
                ColumnValue = CourseId
            };
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        private void SaveCourseTime(HttpContext context)
        {

            Guid CourseId = Guid.Parse(context.Request["CourseId"]);

            string[] CourseTimeId = context.Request["CourseTimeId"].Split("{,}");
            string[] BeginDate = context.Request["BeginDate"].Split("{,}");
            string[] EndDate = context.Request["EndDate"].Split("{,}");
            string[] DepartureCity = context.Request["DepartureCity"].Split("{,}");
            for (int i = 0; i < CourseTimeId.Length; i++)
            {
                if (!string.IsNullOrEmpty(CourseTimeId[i]))
                {
                    Model.ViewModel.Courses.VmCourseDate modelCourseDate = new Model.ViewModel.Courses.VmCourseDate();
                    modelCourseDate.DataId = Guid.Parse(CourseTimeId[i]);
                    modelCourseDate.CourseId = CourseId;
                    modelCourseDate.EndDate = DateTime.Parse(EndDate[i]);
                    modelCourseDate.BeginDate = DateTime.Parse(BeginDate[i]);
                    if (!string.IsNullOrEmpty(DepartureCity[i]))
                        modelCourseDate.DepartureCity = DepartureCity[i];
                    else
                        modelCourseDate.DepartureCity = "不限";
                    SaveCourseTimeData(modelCourseDate);
                }
            }
        }
        private void SaveCourseTimeData(Model.ViewModel.Courses.VmCourseDate modelCourseDate)
        {
            if (modelCourseDate.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Courses.CourseDate>(modelCourseDate);
                handler.Invoke();
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Courses.CourseDate>(modelCourseDate);
                handler.Invoke();
            }
        }
        /// <summary>
        /// 获取活动时间和出发城市
        /// </summary>
        /// <param name="context"></param>
        private void GetProjectTimeProjectId(HttpContext context)
        {
            Guid ProjectId = Guid.Parse(context.Request["ProjectId"]);
            var service = new GetEntityBySingleColumnService<XZMY.Manage.Model.DataModel.Project.ProjectDate>()
            {
                ColumnMember = m => m.ProjectId,
                ColumnValue = ProjectId
            };
            result = JsonConvert.SerializeObject(service.Invoke());
        }
        private void SaveProjectTime(HttpContext context)
        {
            Guid ProjectTimeId = Guid.Empty;
            if (context.Request["ProjectTimeId"] != null)
            {
                ProjectTimeId = Guid.Parse(context.Request["ProjectTimeId"]);
            }
            Guid ProjectId = Guid.Parse(context.Request["ProjectId"]);
            DateTime BeginDate = DateTime.Parse(context.Request["BeginDate"]);
            DateTime EndDate = DateTime.Parse(context.Request["EndDate"]);
            String DepartureCity = context.Request["DepartureCity"];

            Model.ViewModel.Project.VmProjectDate modelProjectDate = new Model.ViewModel.Project.VmProjectDate();

            if (ProjectTimeId != Guid.Empty)
            {
                modelProjectDate.DataId = ProjectTimeId;
            }
            modelProjectDate.ProjectId = ProjectId;
            modelProjectDate.EndDate = EndDate;
            modelProjectDate.BeginDate = BeginDate;
            modelProjectDate.DepartureCity = DepartureCity;


            if (ProjectTimeId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.Project.ProjectDate>(modelProjectDate);
                handler.Invoke();
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.Project.ProjectDate>(modelProjectDate);
                handler.Invoke();
            }
        }
        public bool IsReusable { get; } = false;
    }
}