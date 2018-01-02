using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class PlannerAnswerTemplate : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 模板标题
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("模板标题")] 
        public String Title { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("模板编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        //[EntAttributes.DBColumn("Content")] 
        //[DisplayName("模板内容")] 
        public String Content { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}
