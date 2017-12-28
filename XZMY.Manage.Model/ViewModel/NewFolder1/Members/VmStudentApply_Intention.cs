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
    /// 学生申请集 学生申请集留学意向
    /// </summary>
    [Serializable]
    public class VmStudentApply_Intention :ViewBase, IActionViewModel<StudentApply_Intention>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 目标国家ID
        /// </summary>
        public Guid TargetCountryId { get; set; }
        /// <summary>
        /// 目标国家名字
        /// </summary>
        public String TargetCountryName { get; set; }
        /// <summary>
        /// 申请学历  初中、高中、大学、硕士、博士
        /// </summary>
        public String Education { get; set; }
        /// <summary>
        /// 出国年级ID
        /// </summary>
        public Guid GoAbroadEducationId { get; set; }
        /// <summary>
        /// 出国年级
        /// </summary>
        public String GoAbroadEducationIName { get; set; }
        /// <summary>
        /// 留学预算  1：0-10W ，2：10-20W，3：20-30W，4:30-40W，5：40W以上
        /// </summary>
        public int BudgetCost { get; set; }
        /// <summary>
        /// 学校排名 1：top1-10,2：top11-20,3：top21-50,4：top51以上
        /// </summary>
        public int IntentionalSchoolTop { get; set; }
        /// <summary>
        /// 意向学校名称
        /// </summary>
        public String IntentionalSchoolName { get; set; }
        /// <summary>
        /// 意向专业
        /// </summary>
        public String major { get; set; }
        /// <summary>
        /// 移民计划  1有 2无 3尚未决定
        /// </summary>
        public int ImmigrationProgram { get; set; }
        /// <summary>
        /// 目标学校类型ID
        /// </summary>
        public Guid SchoolTypeId { get; set; }
       
        /// <summary>
        /// 目标学校类型名称
        /// </summary>
        public String SchoolTypeName { get; set; }


        #region Extendsions

        public StudentApply_Intention CreateNewDataModel()
        {
            var model = new StudentApply_Intention();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.TargetCountryId = TargetCountryId;
            model.TargetCountryName = TargetCountryName;
            model.Education = Education;
            model.GoAbroadEducationId = GoAbroadEducationId;
            model.GoAbroadEducationIName = GoAbroadEducationIName;
            model.BudgetCost = BudgetCost;
            model.IntentionalSchoolTop = IntentionalSchoolTop;
            model.IntentionalSchoolName = IntentionalSchoolName;
            model.major = major;
            model.ImmigrationProgram = ImmigrationProgram;
            model.SchoolTypeId = SchoolTypeId;
            model.SchoolTypeName = SchoolTypeName;
            return model;
        }
        public StudentApply_Intention MergeDataModel(StudentApply_Intention model)
        {
            model.StudentId = StudentId;
            model.TargetCountryId = TargetCountryId;
            model.TargetCountryName = TargetCountryName;
            model.Education = Education;
            model.GoAbroadEducationId = GoAbroadEducationId;
            model.GoAbroadEducationIName = GoAbroadEducationIName;
            model.BudgetCost = BudgetCost;
            model.IntentionalSchoolTop = IntentionalSchoolTop;
            model.IntentionalSchoolName = IntentionalSchoolName;
            model.major = major;
            model.ImmigrationProgram = ImmigrationProgram;
            model.SchoolTypeId = SchoolTypeId;
            model.SchoolTypeName = SchoolTypeName;
            return model;
        }
        #endregion
    }
}
