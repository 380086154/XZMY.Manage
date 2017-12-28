using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
namespace XZMY.Manage.Model.DataModel.Members
{
    /// <summary>
    /// 学生申请集教育背景信息
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_SchoolGrade")]
    public class StudentApply_SchoolGrade : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 学校信息ID
        /// </summary>
        public Guid SchoolInformationId { get; set; }
        /// <summary>
        /// 年级 如高中一年级  高中二年级
        /// </summary>
        public String Grade { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime GraduationTime { get; set; }
        /// <summary>
        /// 毕业证明
        /// </summary>
        public String Prove { get; set; }
        /// <summary>
        /// GPAtype
        /// </summary>
        public String GPAtype { get; set; }
        /// <summary>
        /// 班级排名
        /// </summary>
        public Int32 ClassRanking { get; set; }
        /// <summary>
        /// 班级规模
        /// </summary>
        public String ClassScale { get; set; }
        /// <summary>
        /// 年级排序由低到高
        /// </summary>
        public Int32 Sort { get; set; }
    }
}
