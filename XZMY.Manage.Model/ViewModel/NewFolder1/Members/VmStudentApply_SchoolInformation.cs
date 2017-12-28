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
    /// 学生申请集学校信息
    /// </summary>
    [Serializable]
    public class VmStudentApply_SchoolInformation :ViewBase, IActionViewModel<StudentApply_SchoolInformation>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 类型高中还是大学
        /// </summary>
        public String Type { get; set; }
        /// <summary>
        /// 学校类型 数据字典 如 普通学校 、重点学校
        /// </summary>
        public String SchoolType { get; set; }
        /// <summary>
        /// 学校所在地区ID
        /// </summary>
        public Guid LocationId { get; set; }
        /// <summary>
        /// 学校所在地区
        /// </summary>
        public String LocationPathName { get; set; }
        /// <summary>
        /// 学校详细地址
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 学校邮编
        /// </summary>
        public String ZipCode { get; set; }
        /// <summary>
        /// 学校电话号码
        /// </summary>
        public String PhoneNumber { get; set; }
        /// <summary>
        /// 是否毕业学校
        /// </summary>
        public Boolean IsGraduateSchool { get; set; }
        /// <summary>
        /// 入学时间
        /// </summary>
        public DateTime AdmissionDate { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime GraduateDate { get; set; }
        /// <summary>
        /// 学校最高学历
        /// </summary>
        public String SchoolHighestEducation { get; set; }
        /// <summary>
        /// 是否双学位
        /// </summary>
        public Boolean IsDualDegree { get; set; }
        /// <summary>
        /// 设置或获取 学校年级列表
        /// </summary>
        public List<VmStudentApply_SchoolGrade> listSchoolGrade { get; set; }

        #region Extendsions

        public StudentApply_SchoolInformation CreateNewDataModel()
        {
            var model = new StudentApply_SchoolInformation();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.Name = Name;
            model.Type = Type;
            model.SchoolType = SchoolType;
            model.LocationId = LocationId;
            model.LocationPathName = LocationPathName;
            model.Address = Address;
            model.ZipCode = ZipCode;
            model.PhoneNumber = PhoneNumber;
            model.IsGraduateSchool = IsGraduateSchool;
            model.AdmissionDate = AdmissionDate;
            model.GraduateDate = GraduateDate;
            model.SchoolHighestEducation = SchoolHighestEducation;
            model.IsDualDegree = IsDualDegree;
            return model;
        }
        public StudentApply_SchoolInformation MergeDataModel(StudentApply_SchoolInformation model)
        {
            model.StudentId = StudentId;
            model.Name = Name;
            model.Type = Type;
            model.SchoolType = SchoolType;
            model.LocationId = LocationId;
            model.LocationPathName = LocationPathName;
            model.Address = Address;
            model.ZipCode = ZipCode;
            model.PhoneNumber = PhoneNumber;
            model.IsGraduateSchool = IsGraduateSchool;
            model.AdmissionDate = AdmissionDate;
            model.GraduateDate = GraduateDate;
            model.SchoolHighestEducation = SchoolHighestEducation;
            model.IsDualDegree = IsDualDegree;
            return model;
        }
        #endregion
    }
}
