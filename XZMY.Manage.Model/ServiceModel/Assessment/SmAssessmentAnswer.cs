using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Assessment
{
    [Serializable]
    [DataContract]
    public class SmAssessmentAnswer
    {
        /// <summary>
        /// 问题ID
        /// </summary>
        [DataMember]
        public String QuestionId { get; set; }
        /// <summary>
        /// 问题标题
        /// </summary>
        [DataMember]
        public String QuestionTitle { get; set; }
        /// <summary>
        /// 问题序号
        /// </summary>
        [DataMember]
        public String QuestionSort { get; set; }
        /// <summary>
        /// 答案ID
        /// </summary>
        [DataMember]
        public String AnswerId { get; set; }
        /// <summary>
        /// 答案内容
        /// </summary>
        [DataMember]
        public String AnswerDescription { get; set; }
    }
}
