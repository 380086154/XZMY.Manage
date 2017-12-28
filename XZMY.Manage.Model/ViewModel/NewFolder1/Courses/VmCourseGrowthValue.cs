using System;
using XZMY.Manage.Model.DataModel.Courses;

namespace XZMY.Manage.Model.ViewModel.Courses
{
    public class VmCourseGrowthValue : IActionViewModel<CourseGrowthValue>
    {
        public Guid DataId { get; set; }
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("")] 
        public Guid CourseId { get; set; }
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

        public CourseGrowthValue CreateNewDataModel()
        {
            var model = new CourseGrowthValue();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.CourseId = CourseId;
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemsName;
            model.Visible = Visible;
            model.Score = Score;
            return model;
        }

        public CourseGrowthValue MergeDataModel(CourseGrowthValue model)
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