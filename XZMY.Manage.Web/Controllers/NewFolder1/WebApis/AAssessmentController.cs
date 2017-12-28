using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ServiceModel.Assessment;
using XZMY.Manage.Model.ViewModel.Assessment;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Assessment;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    
    public class AAssessmentController : ApiController
    {
        private List<AnswerScore> GetListScores(Guid AnswersId, IList<Scores> listScores)
        {
            List<AnswerScore> list = new List<AnswerScore>();
            AnswerScore model = new AnswerScore();
            var listAnswerScores = listScores.Where(x => x.SourceId == AnswersId).ToList();
            foreach (var m in listAnswerScores)
            {
                //model.Id = m.Id;
                model.ScoreItemsId = m.ScoreItemsId;
                model.ScoreItemsName = m.ScoreItemsName;
                model.ScoreValue = m.ScoreValue;
                model.SourceType = m.SourceType;
                list.Add(model);
            }
            return list;
        }
        private List<SmAssessmentAnswers> GetListAnswers(Guid QuestionsId, IList<AssessmentAnswers> listAnswers, IList<Scores> listScores)
        {
            List<SmAssessmentAnswers> list = new List<SmAssessmentAnswers>();
            SmAssessmentAnswers model = new SmAssessmentAnswers();
            List<AssessmentAnswers> listAssessmentAnswers = listAnswers.Where(x => x.QuestionsId == QuestionsId).ToList();
            int iSort = 0;
            foreach (var m in listAssessmentAnswers)
            {
                iSort++;
                model = new SmAssessmentAnswers();
                //model.Id=m.Id;
                model.Description= m.Description;
                model.Sort= iSort;
                model.ListScores= GetListScores(m.DataId, listScores);
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 获取评估中心问题
        /// </summary>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Dictionary<string, List<SmAssessment>>> GetAssessmentQuestions()
        {
            List<SmAssessment> listModel = new List<SmAssessment>();
            IList<AssessmentQuestions> listQuestions = GetAssessmentQuestionsList();
            IList<AssessmentAnswers> listAnswers = GetAssessmentAnswersList();
            IList<Scores> listScores = GetScoresList();
            int iQuestionSort = 0;
            SmAssessment model = new SmAssessment();
            SmAssessmentAnswers modelAnswers = new SmAssessmentAnswers();
            foreach (var mQ in listQuestions)
            {
                iQuestionSort++;
                model = new SmAssessment();
                //model.Id = mQ.Id;
                model.Sort = iQuestionSort;
                model.Title = mQ.Description.Replace("src=\"/Upload/Temp", string.Format("src=\"{0}/Upload/Temp", WebConfigurationManager.AppSettings["SiteUrl"]));
                model.listAnswers = GetListAnswers(mQ.DataId, listAnswers, listScores);
                listModel.Add(model);
            }
            var res = new Dictionary<string, List<SmAssessment>>();
            res.Add("Assessment", listModel);

            return new ApiHandlerInvokeResult<Dictionary<string, List<SmAssessment>>>()
            {
                Output = res,
                Success = true
            };
        }


        private IList<AssessmentQuestions> GetAssessmentQuestionsList()
        {
            var service = new CustomSearchWithPaginationService<AssessmentQuestions>
            {
                PageIndex = 1,
                PageSize = 50,
                CustomConditions = new List<CustomCondition<AssessmentQuestions>>
                {
                    new CustomConditionPlus<AssessmentQuestions>
                    {
                        Value =1,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<AssessmentQuestions, object>>[] { x => x.State}
                    }
                },
                SortMember = new Expression<Func<AssessmentQuestions, object>>[] { x => x.EnableTime }
            };

            var result = service.Invoke();
            return result.Results;
            //List<VmAssessmentQuestion> listQuestion = new List<VmAssessmentQuestion>();
            //foreach (var m in result.Results)
            //{
                
            //}
            //return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        private IList<AssessmentAnswers> GetAssessmentAnswersList()
        {
            var service = new CustomSearchWithPaginationService<AssessmentAnswers>
            {
                PageIndex = 1,
                PageSize = 9999,
                CustomConditions = new List<CustomCondition<AssessmentAnswers>>
                {
                    new CustomConditionPlus<AssessmentAnswers>
                    {
                        Value ="",
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<AssessmentAnswers, object>>[] { x => x.Description}
                    }
                }
            };
            var result = service.Invoke();
            return result.Results;
        }
        private IList<Scores> GetScoresList()
        {
            var service = new CustomSearchWithPaginationService<Scores>
            {
                PageIndex = 1,
                PageSize = 999999,
                CustomConditions = new List<CustomCondition<Scores>>
                {
                    new CustomConditionPlus<Scores>
                    {
                        Value = "AssessmentAnswer",
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Scores, object>>[] { x => x.SourceType}
                    }
                }
            };
            var result = service.Invoke();
            return result.Results;
        }
    }
}