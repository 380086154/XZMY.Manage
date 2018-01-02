using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Project
{
    
    [Serializable]
    [DBTable("ProjectDate")]
    public class ProjectDate : EntityBase, IDataModel
    {
        #region Properties 
        
        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动Id")] 
        public Guid ProjectId { get; set; }
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
        /// 出发城市 用, 逗号分隔
        /// </summary>
        public String DepartureCity { get; set; }
        #endregion
    }
}
