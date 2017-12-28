using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("StudentPlan")]
    public class StudentPlan : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        public Guid PlanningNoteId { get; set; }
        public Guid PlanRecordId { get; set; }
        public String Grade { get; set; }
        public String SchoolType { get; set; }
        public String SchoolPlace { get; set; }
        public Decimal Fee { get; set; }
        public Decimal EnglishScore { get; set; }
        public Decimal LearnScore { get; set; }
        public Decimal QualityScore { get; set; }
        public Decimal AddEnglishScore { get; set; }
        public Decimal AddLearnScore { get; set; }
        public Decimal AddQualityScore { get; set; }
        public int Sort { get; set; }
    }
}
