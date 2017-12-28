using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("V_ProjectCourseTemplateList")]
    public class V_ProjectCourseTemplateList : EntityBase, IDataModel
    {

        public String Type { get; set; }

        public String TypeName { get; set; }

        public String Name { get; set; }
        public String Code { get; set; }

        public String Pictures { get; set; }
        public String SuitablePerson { get; set; }
        /// <summary>
        /// 面向人群
        /// </summary>
        public String SuitablePersonName { get; set; }
        public String PlaceName { get; set; }
        public String Sponsor { get; set; }
        public Int32 RecommendedIndex { get; set; }
        public String Service { get; set; }
        public Decimal MarketPrice { get; set; }
        public Decimal ActualPrice { get; set; }
        public Decimal DepositPrice { get; set; }

        public Decimal Discount { get; set; }
        public Int32 DifficultyValue { get; set; }

        public Int32 CompletionValue { get; set; }

        public Int32 FeeValue { get; set; }

        public Decimal EnglishScore { get; set; }
        public Decimal LearnScore { get; set; }
        public Decimal QualityScore { get; set; }
        public String ScoreItemNames { get; set; }
        public EState State { get; set; }
    }
}
