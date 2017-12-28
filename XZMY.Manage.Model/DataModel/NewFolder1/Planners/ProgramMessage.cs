using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    /// <summary> 
    /// x消息
    /// </summary> 
    [Serializable]
    [DBTable("ProgramMessage")]
    public class ProgramMessage : EntityBase, IDataModel
    {
        /// <summary>
        /// 接收人ID
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        /// 接收学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// 历练ID
        /// </summary>
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 历练类型
        /// </summary>
        public EProgramType ProgramType { get; set; }
        /// <summary>
        /// 规划师ID
        /// </summary>
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        public String PlannerName { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public Boolean IsRead { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime MessageTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMessageType MessageType { get; set; }
    }
}
