using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 学生成绩的总分数
    /// </summary>
    [DBTable("StudentTotalScore")]
    public class StudentTotalScore : EntityBase, IDataModel
    {
        /// <summary>
        /// 英语总分
        /// </summary>
        public int EnglishScore { get; set; }
        /// <summary>
        /// 学术总分
        /// </summary>
        public int LearnScore { get; set; }
        /// <summary>
        /// 素质总分
        /// </summary>
        public int QualityScore { get; set; }
    }
}
