using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;

namespace XZMY.Manage.Model.ViewModel.Assessment
{
    [Serializable]
    public class VmScore : IActionViewModel<Scores>
    {
        #region Properties 

        /// <summary>
        /// 主键
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("主键")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 分值项目
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemsId")] 
        //[DisplayName("分值项目")] 
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值项目名称
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemName")] 
        //[DisplayName("分值项目名称")] 
        public String ScoreItemName { get; set; }
        /// <summary>
        /// 分值来源ID
        /// </summary>
        //[EntAttributes.DBColumn("SourceId")] 
        //[DisplayName("分值来源ID")] 
        public Guid SourceId { get; set; }
        /// <summary>
        /// 分值来源类型
        /// </summary>
        //[EntAttributes.DBColumn("SourceType")] 
        //[DisplayName("分值来源类型")] 
        public String SourceType { get; set; }
        /// <summary>
        /// 具体分值
        /// </summary>
        //[EntAttributes.DBColumn("Score")] 
        //[DisplayName("具体分值")] 
        public Decimal ScoreValue { get; set; }

        #endregion

        #region Extendsions

        public Scores CreateNewDataModel()
        {
            var model = new Scores();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemName;
            model.SourceId = SourceId;
            model.SourceType = SourceType;
            model.ScoreValue = ScoreValue;
            return model;
        }

        public Scores MergeDataModel(Scores model)
        {
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemName;
            model.SourceId = SourceId;
            model.SourceType = SourceType;
            model.ScoreValue = ScoreValue;
            return model;
        }
        #endregion
    }
}
