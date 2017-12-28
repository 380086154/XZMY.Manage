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
    public class VmStudentApply_Interest :ViewBase, IActionViewModel<StudentApply_Interest>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 兴趣名称 数据字典
        /// </summary>
        public Guid InterestId { get; set; }
        /// <summary>
        /// 兴趣名称 数据字典
        /// </summary>
        public String InterestName { get; set; }
        /// <summary>
        /// 兴趣程度 1-10
        /// </summary>
        public Int32 LevelValue { get; set; }

        #region Extendsions

        public StudentApply_Interest CreateNewDataModel()
        {
            var model = new StudentApply_Interest();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.InterestId = InterestId;
            model.InterestName = InterestName;
            model.LevelValue = LevelValue;
            return model;
        }
        public StudentApply_Interest MergeDataModel(StudentApply_Interest model)
        {
            model.StudentId = StudentId;
            model.InterestId = InterestId;
            model.InterestName = InterestName;
            model.LevelValue = LevelValue;
            return model;
        }
        #endregion
    }
}
