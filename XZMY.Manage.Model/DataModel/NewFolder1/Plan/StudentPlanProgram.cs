using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("StudentPlanProgram")]
    public class StudentPlanProgram : EntityBase, IDataModel
    {
        public Guid StudentPlanId { get; set; }
        public int Type { get; set; }
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 项目分类名字 领导能力
        /// </summary>
        public String ItemName { get; set; }
        /// <summary>
        /// 规划活动或课程图片
        /// </summary>
        public String Images { get; set; }
        public String Name { get; set; }
        public Decimal AddEnglishScore { get; set; }
        public Decimal AddLearnScore { get; set; }
        public Decimal AddQualityScore { get; set; }

    }
}
