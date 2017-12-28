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
    /// 学生申请集 学生申请集课外活动
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_Project")]
    public class StudentApply_Project : EntityBase, IDataModel
    {
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
    }
}
