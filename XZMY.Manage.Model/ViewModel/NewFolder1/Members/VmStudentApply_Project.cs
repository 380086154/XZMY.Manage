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
    /// 学生申请集课外活动
    /// </summary>
    [Serializable]
    public class VmStudentApply_Project :ViewBase, IActionViewModel<StudentApply_Project>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 课外活动名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 课外活动类型ID
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// 课外活动类型名称
        /// </summary>
        public String TypeName { get; set; }
        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime ProjectTime { get; set; }
        /// <summary>
        /// 活动时长持续几周
        /// </summary>
        public String Duration { get; set; }
        /// <summary>
        /// 活动证书
        /// </summary>
        public String Certificate { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 活动留言
        /// </summary>
        public String Message { get; set; }


        #region Extendsions

        public StudentApply_Project CreateNewDataModel()
        {
            var model = new StudentApply_Project();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.Name = Name;
            model.TypeId = TypeId;
            model.TypeName = TypeName;
            model.ProjectTime = ProjectTime;
            model.Duration = Duration;
            model.Certificate = Certificate;
            model.Description = Description;
            model.Message = Message;
            return model;
        }
        public StudentApply_Project MergeDataModel(StudentApply_Project model)
        {
            model.StudentId = StudentId;
            model.Name = Name;
            model.TypeId = TypeId;
            model.TypeName = TypeName;
            model.ProjectTime = ProjectTime;
            model.Duration = Duration;
            model.Certificate = Certificate;
            model.Description = Description;
            model.Message = Message;
            return model;
        }
        #endregion
    }
}
