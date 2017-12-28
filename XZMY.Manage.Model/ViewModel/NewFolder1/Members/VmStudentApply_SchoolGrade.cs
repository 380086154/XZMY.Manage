using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Members
{
    /// <summary>
    /// 学生申请集 学生申请集兴趣
    /// </summary>
    [Serializable]
    public class VmStudentApply_SchoolGrade :ViewBase, IActionViewModel<StudentApply_SchoolGrade>
    {
        public Guid DataId { get; set; }
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

        /// <summary>
        /// 设置 或 获取 课程列表
        /// </summary>
        public List<VmStudentApply_SchoolCourse> listSchoolCourse { get; set; }
        #region Extendsions

        public StudentApply_SchoolGrade CreateNewDataModel()
        {
            var model = new StudentApply_SchoolGrade();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.SchoolInformationId = SchoolInformationId;
            model.Grade = Grade;
            model.GraduationTime = GraduationTime;
            model.GPAtype = GPAtype;
            model.ClassRanking = ClassRanking;
            model.ClassScale = ClassScale;
            model.Sort = Sort;
            return model;
        }
        public StudentApply_SchoolGrade MergeDataModel(StudentApply_SchoolGrade model)
        {
            model.StudentId = StudentId;
            model.SchoolInformationId = SchoolInformationId;
            model.Grade = Grade;
            model.GraduationTime = GraduationTime;
            model.GPAtype = GPAtype;
            model.ClassRanking = ClassRanking;
            model.ClassScale = ClassScale;
            model.Sort = Sort;
            return model;
        }
        #endregion
    }
}
