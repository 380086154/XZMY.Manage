using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.School
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class SchoolType : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 学校类型名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 类型学校费用
        /// </summary>
        public Int32 Fee { get; set; }
        /// <summary>
        /// 英语
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学术
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质
        /// </summary>
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}