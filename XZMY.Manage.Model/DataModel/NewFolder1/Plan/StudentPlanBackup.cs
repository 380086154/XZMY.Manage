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
    [DBTable("StudentPlanBackup")]
    public class StudentPlanBackup : EntityBase, IDataModel
    {
        public Guid StudentPlanId { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public String Grade { get; set; }
        /// <summary>
        /// 学校类型名称  普通学校 重点学校
        /// </summary>
        public String SchoolType { get; set; }
        /// <summary>
        /// 学校地点 国外 国内
        /// </summary>
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 预算学习费用
        /// </summary>
        public Decimal Fee { get; set; }
        /// <summary>
        /// 英语分值
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学术分值
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质分值
        /// </summary>
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 增加英语分值
        /// </summary>
        public Decimal AddEnglishScore { get; set; }
        /// <summary>
        /// 增加学术分值
        /// </summary>
        public Decimal AddLearnScore { get; set; }
        /// <summary>
        /// 增加素质分值
        /// </summary>
        public Decimal AddQualityScore { get; set; }
        /// <summary>
        /// 年级ID 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
