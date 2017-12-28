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
    [DBTable("OnlineAssessmentSecurity")]
    public class OnlineAssessmentSecurity : EntityBase, IDataModel
    {
        /// <summary>
        /// 试卷名称 
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 试卷编码
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 用时（秒）
        /// </summary>
        public int UseTime { get; set; }
    }
}
