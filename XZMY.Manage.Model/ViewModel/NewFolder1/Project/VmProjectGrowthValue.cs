using System;
using XZMY.Manage.Model.DataModel.Project;

namespace XZMY.Manage.Model.ViewModel.Project
{
    public class VmProjectGrowthValue : IActionViewModel<ProjectGrowthValue>
    {
        public Guid DataId { get; set; }
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemsId")] 
        //[DisplayName("")] 
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值项目名
        /// </summary>
        //[EntAttributes.DBColumn("ScoreItemsName")] 
        //[DisplayName("分值项目名")] 
        public String ScoreItemsName { get; set; }
        /// <summary>
        /// 是否显示为标签页
        /// </summary>
        //[EntAttributes.DBColumn("Visible")] 
        //[DisplayName("是否显示为标签页")] 
        public Int32 Visible { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        //[EntAttributes.DBColumn("Score")] 
        //[DisplayName("分值")] 
        public Decimal Score { get; set; }
        #endregion
        #region Extendsions

        public ProjectGrowthValue CreateNewDataModel()
        {
            var model = new ProjectGrowthValue();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectId = ProjectId;
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemsName;
            model.Visible = Visible;
            model.Score = Score;
            return model;
        }

        public ProjectGrowthValue MergeDataModel(ProjectGrowthValue model)
        {
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemsName;
            model.Visible = Visible;
            model.Score = Score;
            return model;
        }
        #endregion
    }
}