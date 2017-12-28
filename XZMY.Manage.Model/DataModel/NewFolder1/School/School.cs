using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.School
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class School : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 学校名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学校名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 学校英文名称
        /// </summary>
        //[EntAttributes.DBColumn("EnglishName")] 
        //[DisplayName("学校英文名称")] 
        public String EnglishName { get; set; }
        /// <summary>
        /// 学校所在国家
        /// </summary>
        //[EntAttributes.DBColumn("CountryLocationId")] 
        //[DisplayName("学校所在国家")] 
        public Guid CountryLocationId { get; set; }
        /// <summary>
        /// 学校所在国家名字
        /// </summary>
        //[EntAttributes.DBColumn("CountryLocationName")] 
        //[DisplayName("学校所在国家名字")] 
        public String CountryLocationName { get; set; }
        /// <summary>
        /// 学校性质 1公立 2私立
        /// </summary>
        //[EntAttributes.DBColumn("Nature")] 
        //[DisplayName("学校性质 1公立 2私立")] 
        public Int32 Nature { get; set; }
        public String NatureName
        {
            get
            {
                String strValue = "公立";
                switch (Nature)
                {
                    case 1:
                        strValue = "公立";
                        break;
                    case 2:
                        strValue = "私立";
                        break;
                }
                return strValue;
            }
        }
        /// <summary>
        /// 学校类别
        /// </summary>
        //[EntAttributes.DBColumn("SchoolCategoryId")] 
        //[DisplayName("学校类别")] 
        public Guid SchoolCategoryId { get; set; }
        /// <summary>
        /// 学校类别名称
        /// </summary>
        //[EntAttributes.DBColumn("SchoolCategoryName")] 
        //[DisplayName("学校类别名称")] 
        public String SchoolCategoryName { get; set; }
        /// <summary>
        /// 学校类别层级名称
        /// </summary>
        //[EntAttributes.DBColumn("SchoolCategoryPathName")] 
        //[DisplayName("学校类别层级名称")] 
        public String SchoolCategoryPathName { get; set; }
        /// <summary>
        /// 课程 用,逗号分隔
        /// </summary>
        //[EntAttributes.DBColumn("Courses")] 
        //[DisplayName("课程 用,逗号分隔")] 
        public String Courses { get; set; }
        /// <summary>
        /// 建校日期
        /// </summary>
        //[EntAttributes.DBColumn("EstablishDate")] 
        //[DisplayName("建校日期")] 
        public DateTime EstablishDate { get; set; }
        /// <summary>
        /// 教师人数
        /// </summary>
        //[EntAttributes.DBColumn("TeacherCount")] 
        //[DisplayName("教师人数")] 
        public Int32 TeacherCount { get; set; }
        /// <summary>
        /// 学生人数
        /// </summary>
        //[EntAttributes.DBColumn("StudentCount")] 
        //[DisplayName("学生人数")] 
        public Int32 StudentCount { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        //[EntAttributes.DBColumn("Coordinate")] 
        //[DisplayName("坐标")] 
        public String Coordinate { get; set; }
        /// <summary>
        /// 学校地址区域Id
        /// </summary>
        //[EntAttributes.DBColumn("LocationId")] 
        //[DisplayName("学校地址区域Id")] 
        public Guid LocationId { get; set; }
        /// <summary>
        /// 学校地址区域层级名称
        /// </summary>
        //[EntAttributes.DBColumn("LocationPathName")] 
        //[DisplayName("学校地址区域层级名称")] 
        public String LocationPathName { get; set; }
        /// <summary>
        /// 学校地址
        /// </summary>
        //[EntAttributes.DBColumn("Address")] 
        //[DisplayName("学校地址")] 
        public String Address { get; set; }
        /// <summary>
        /// 学区
        /// </summary>
        //[EntAttributes.DBColumn("SchoolDistrict")] 
        //[DisplayName("学区")] 
        public String SchoolDistrict { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        //[EntAttributes.DBColumn("Ranking")] 
        //[DisplayName("排名")] 
        public String Ranking { get; set; }
        /// <summary>
        /// 学校描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("学校描述")] 
        public String Description { get; set; }
        /// <summary>
        /// 学校电话
        /// </summary>
        public String Moblie { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid SchoolTypeId { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}