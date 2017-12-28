using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Program
{
    /// <summary>
    /// 历练和订单对应表
    /// </summary>
    [DBTable("V_ProgramOrder")]
    public class ProgramOrder : EntityBase, IDataModel
    {
        /// <summary>
        /// 历练ID
        /// </summary>
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 历练模板ID
        /// </summary>
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 历练名称
        /// </summary>
        public String ProgramName { get; set; }
        /// <summary>
        /// 报名学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 报名学生MemberID
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }
    }
}
