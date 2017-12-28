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
    /// 学生申请集 监护人员家庭成员
    /// </summary>
    [Serializable]
    public class VmStudentApply_Guardian :ViewBase, IActionViewModel<StudentApply_Guardian>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public String LastName { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        public String FullName { get; set; }
        /// <summary>
        /// 与你的关系  父亲、母亲、哥哥、妹妹
        /// </summary>
        public String Relationship { get; set; }
        /// <summary>
        /// 关系 1家长 2兄弟姐妹
        /// </summary>
        public Int32 Relation { get; set; }
        /// <summary>
        /// Mobile
        /// </summary>
        public String Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public String HomeAddress { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        public String HighestEducation { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public String Position { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public String WorkPlace { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        #region Extendsions

        public StudentApply_Guardian CreateNewDataModel()
        {
            var model = new StudentApply_Guardian();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.FullName = FullName;
            model.Relationship = Relationship;
            model.Relation = Relation;
            model.Mobile = Mobile;
            model.Email = Email;
            model.HomeAddress = HomeAddress;
            model.HighestEducation = HighestEducation;
            model.Position = Position;
            model.WorkPlace = WorkPlace;
            model.Birthday = Birthday;
            return model;
        }
        public StudentApply_Guardian MergeDataModel(StudentApply_Guardian model)
        {
            model.StudentId = StudentId;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.FullName = FullName;
            model.Relationship = Relationship;
            model.Relation = Relation;
            model.Mobile = Mobile;
            model.Email = Email;
            model.HomeAddress = HomeAddress;
            model.HighestEducation = HighestEducation;
            model.Position = Position;
            model.WorkPlace = WorkPlace;
            model.Birthday = Birthday;
            return model;
        }
        #endregion
    }
}
