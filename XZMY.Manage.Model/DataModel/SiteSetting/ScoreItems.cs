using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    public class ScoreItems : EntityBase, IDataModel
    {
        /// <summary>
        /// 分值名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分值编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 分值项目类型     1英语分值  2学科分值  3素质分值
        /// </summary>
        public ScoreItemType Type { get; set; }
    }
    /// <summary>
    /// 分值项目类型     1英语分值  2学科分值  3素质分值
    /// </summary>
    public enum ScoreItemType
    {
        未知 = 0,
        英语 = 1,
        学科 = 2,
        素质 = 3
    }
}
