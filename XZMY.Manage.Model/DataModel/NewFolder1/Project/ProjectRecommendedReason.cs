using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;


namespace XZMY.Manage.Model.DataModel.Project
{
    public class ProjectRecommendedReason : EntityBase, IDataModel
    {
        #region Properties 
        
        /// <summary>
        /// 活动id 外键
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动id 外键")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 推荐理由
        /// </summary>
        //[EntAttributes.DBColumn("Reason")] 
        //[DisplayName("推荐理由")] 
        public String Reason { get; set; }
      
        #endregion

    }
}
