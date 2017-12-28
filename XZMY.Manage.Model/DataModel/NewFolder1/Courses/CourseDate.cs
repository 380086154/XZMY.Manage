using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Courses
{
    [Serializable]
    [DataContract]
    public class CourseDate : EntityBase, IDataModel
    {
        #region Properties 
        
        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("活动Id")] 
        public Guid CourseId { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        //[EntAttributes.DBColumn("BeginDate")] 
        //[DisplayName("开始日期")] 
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        //[EntAttributes.DBColumn("EndDate")] 
        //[DisplayName("结束日期")] 
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 出发城市
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCity")] 
        //[DisplayName("出发城市")] 
        public String DepartureCity { get; set; }
        #endregion
    }
}
