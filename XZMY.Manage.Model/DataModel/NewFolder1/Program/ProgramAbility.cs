using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Program
{
    /// <summary>
    /// 活动课程历练的能力名称及描述表
    /// </summary>
    [DBTable("program_Ability")]
    public class ProgramAbility : EntityBase, IDataModel
    {
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        public EProgramType Type { get; set; }
        /// <summary>
        /// 能力名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 能力描述
        /// </summary>
        public String Description { get; set; }
    }
}
