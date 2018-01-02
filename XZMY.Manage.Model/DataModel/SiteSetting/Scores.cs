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
    public class Scores : EntityBase, IDataModel
    {

        /// <summary>
        /// 分值项目Id
        /// </summary>
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值名称
        /// </summary>
        public string ScoreItemsName { get; set; }
        /// <summary>
        /// 分值来源项目Id  如ProjectID
        /// </summary>
        public Guid SourceId { get; set; }
        /// <summary>
        /// 分值来源表名称
        /// </summary>
        public String SourceType { get; set; }
        /// <summary>
        /// 具体分值
        /// </summary>
        public decimal ScoreValue { get;set;}

    }
}
