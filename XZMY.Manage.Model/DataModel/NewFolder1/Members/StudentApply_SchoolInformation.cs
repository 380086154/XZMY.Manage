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
    /// 学生申请集学校信息
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_SchoolInformation")]
    public class StudentApply_SchoolInformation : EntityBase, IDataModel
    {
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
    }
}
