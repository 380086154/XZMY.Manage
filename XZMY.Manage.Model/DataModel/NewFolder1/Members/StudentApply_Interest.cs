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
    /// 学生申请集 学生申请集兴趣
    /// </summary>
    [Serializable]
    [DBTable("StudentApply_Interest")]
    public class StudentApply_Interest : EntityBase, IDataModel
    {
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
    }
}
