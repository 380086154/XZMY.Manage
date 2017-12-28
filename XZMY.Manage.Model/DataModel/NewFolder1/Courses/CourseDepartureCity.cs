using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Courses
{
    public class CourseDepartureCity : EntityBase, IDataModel
    {
        #region Properties 
        
        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("活动Id")] 
        public Guid CourseId { get; set; }
        /// <summary>
        /// 活动时间id
        /// </summary>
        //[EntAttributes.DBColumn("CourseDateId")] 
        //[DisplayName("活动时间id")] 
        public Guid CourseDateId { get; set; }
        /// <summary>
        /// 出发地Id
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCityLocationId")] 
        //[DisplayName("出发地Id")] 
        public Guid DepartureCityLocationId { get; set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCity")] 
        //[DisplayName("出发地名称")] 
        public String DepartureCity { get; set; }

        #endregion

    }
}
