using System;
using System.Text.RegularExpressions;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Assessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("AssessmentQuestions")]
    public class AssessmentQuestions : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 标题
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("标题")] 
        public String Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("内容")] 
        public string Description { get; set; }

        /// <summary>
        /// 内容(清除HTML)
        /// </summary>
        public string DescriptionText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Description))
                    return string.Empty;
                return new Regex(@"<[^>]+>|</[^>]+>").Replace(Description, string.Empty);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        //[EntAttributes.DBColumn("State")] 
        //[DisplayName("状态")] 
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName  { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        //[EntAttributes.DBColumn("EnableTime")] 
        //[DisplayName("启用时间")] 
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 废弃时间
        /// </summary>
        //[EntAttributes.DBColumn("DisableTime")] 
        //[DisplayName("废弃时间")] 
        public DateTime DisableTime { get; set; }
        /// <summary>
        /// 所属试卷ID
        /// </summary>
        public Guid AssessmentId { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}