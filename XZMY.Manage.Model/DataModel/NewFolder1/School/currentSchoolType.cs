using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.School
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class currentSchoolType : EntityBase, IDataModel
    {
        /// <summary>
        /// 当前就读学校类型数字ID
        /// </summary>
        public int currentSchoolTypeId { get; set; }
        /// <summary>
        /// 当前就读学校类型名称
        /// </summary>
        public String currentSchoolTypeName { get; set; }
        /// <summary>
        /// 系数
        /// </summary>
        public Decimal coefficient { get; set; }
    }
}
