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
    /// 学生申请证书
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_Certificate")]
    public class StudentApply_Certificate : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 证书标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 证书级别
        /// </summary>
        public String LevelValue { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public String Grade { get; set; }
        /// <summary>
        /// 证书照片
        /// </summary>
        public String Pictures { get; set; }
    }
}
