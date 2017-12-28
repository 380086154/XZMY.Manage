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
    /// 学生申请集课程信息成绩单
    /// </summary>
    [Serializable]
    public class VmStudentApply_SchoolCourse :ViewBase, IActionViewModel<StudentApply_SchoolCourse>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 课程所属学校年级
        /// </summary>
        public Guid StudentApply_SchoolGradeId { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        public Int32 SchoolYear { get; set; }
        /// <summary>
        /// 课程名字
        /// </summary>
        public String CourseName { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public String CourseCode { get; set; }
        /// <summary>
        /// 成绩单上的课程名称
        /// </summary>
        public String ProveCourseName { get; set; }
        /// <summary>
        /// 上学期学分
        /// </summary>
        public Decimal CreditUp { get; set; }
        /// <summary>
        /// 上学期成绩单
        /// </summary>
        public String ProveUp { get; set; }
        /// <summary>
        /// 下学期学分
        /// </summary>
        public Decimal CreditDown { get; set; }
        /// <summary>
        /// 下学期成绩单
        /// </summary>
        public String ProveDown { get; set; }


        #region Extendsions

        public StudentApply_SchoolCourse CreateNewDataModel()
        {
            var model = new StudentApply_SchoolCourse();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.StudentApply_SchoolGradeId = StudentApply_SchoolGradeId;
            model.SchoolYear = SchoolYear;
            model.CourseName = CourseName;
            model.CourseCode = CourseCode;
            model.ProveCourseName = ProveCourseName;
            model.CreditUp = CreditUp;
            model.ProveUp = ProveUp;
            model.CreditDown = CreditDown;
            model.ProveDown = ProveDown;
            return model;
        }
        public StudentApply_SchoolCourse MergeDataModel(StudentApply_SchoolCourse model)
        {
            model.StudentId = StudentId;
            model.StudentApply_SchoolGradeId = StudentApply_SchoolGradeId;
            model.SchoolYear = SchoolYear;
            model.CourseName = CourseName;
            model.CourseCode = CourseCode;
            model.ProveCourseName = ProveCourseName;
            model.CreditUp = CreditUp;
            model.ProveUp = ProveUp;
            model.CreditDown = CreditDown;
            model.ProveDown = ProveDown;
            return model;
        }
        #endregion
    }
}
