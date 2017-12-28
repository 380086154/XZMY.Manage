
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XZMY.Manage.Model.Enum;
    using T2M.CoastLine.Utils.Model.Attributes;
    using T2M.Common.Utils.ADONET.SQLServer;
    using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.OnlineAssessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("OnlineAssessmentStudent")]
    public class OnlineAssessmentStudent : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生姓名 
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 登录名 
        /// </summary>
        public String LoginName { get; set; }
        /// <summary>
        /// 密码 
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 状态 
        /// </summary>
        public EState State { get; set; }
    }
}
