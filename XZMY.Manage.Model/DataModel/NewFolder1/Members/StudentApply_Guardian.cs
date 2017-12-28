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
    /// 学生申请集 监护人和家庭人员
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_Guardian")]
    public class StudentApply_Guardian : EntityBase, IDataModel
    {
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
    }
}
