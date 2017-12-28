using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Assessment
{
    /// <summary>
    /// 评估中文问题和答案
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmAssessment
    {
        /// <summary>
        /// 评估问题ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 评估问题标题
        /// </summary>
        [DataMember]
        public String Title { get; set; }
        /// <summary>
        /// 评估问题标题描述
        /// </summary>
        [DataMember]
        public String Description { get; set; }
        /// <summary>
        /// 评估问题排序从小到大
        /// </summary>
        [DataMember]
        public Int32 Sort { get; set; }
        /// <summary>
        /// 问题答案列表
        /// </summary>
        [DataMember]
        public List<SmAssessmentAnswers> listAnswers { get; set; }
    }
    /// <summary>
    /// 评估答案模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmAssessmentAnswers
    {
        /// <summary>
        /// 答案ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 答案排序从小到大
        /// </summary>
        [DataMember]
        public Int32 Sort { get; set; }
        /// <summary>
        /// 答案内容
        /// </summary>
        [DataMember]
        public String Description { get; set; }
        /// <summary>
        /// 答案获得分值列表
        /// </summary>
        [DataMember]
        public List<AnswerScore> ListScores { get; set; }
    }
    /// <summary>
    /// 答案分值模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class AnswerScore {
        /// <summary>
        /// 分值ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 分值项目ID
        /// </summary>
        [DataMember]
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值项目名字
        /// </summary>
        [DataMember]
        public String ScoreItemsName { get; set; }
        /// <summary>
        /// 分值类型
        /// </summary>
        [DataMember]
        public String SourceType { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        [DataMember]
        public Decimal ScoreValue { get; set; }
    }

}
