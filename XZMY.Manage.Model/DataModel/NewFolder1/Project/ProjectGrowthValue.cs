using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Project
{
    public class ProjectGrowthValue : EntityBase, IDataModel
    {
        #region Properties 
        
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemsId")] 
        //[DisplayName("")] 
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值项目名
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemsName")] 
        //[DisplayName("分值项目名")] 
        public String ScoreItemsName { get; set; }
        /// <summary>
        /// 是否显示为标签页
        /// </summary>
        //[EntAttributes.DBColumn("Visible")] 
        //[DisplayName("是否显示为标签页")] 
        public Int32 Visible { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        //[EntAttributes.DBColumn("Score")] 
        //[DisplayName("分值")] 
        public Decimal Score { get; set; }

        #endregion
    }
}
