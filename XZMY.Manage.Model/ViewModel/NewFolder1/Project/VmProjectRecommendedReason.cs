using System;
using XZMY.Manage.Model.DataModel.Project;

namespace XZMY.Manage.Model.ViewModel.Project
{
    public class VmProjectRecommendedReason : IActionViewModel<ProjectRecommendedReason>
    {
        #region Properties 

        /// <summary>
        /// 活动推荐理由id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("活动推荐理由id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 活动id 外键
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动id 外键")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 推荐理由
        /// </summary>
        //[EntAttributes.DBColumn("Reason")] 
        //[DisplayName("推荐理由")] 
        public String Reason { get; set; }

        #endregion

        #region Extendsions

        public ProjectRecommendedReason CreateNewDataModel()
        {
            var model = new ProjectRecommendedReason();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectId = ProjectId;
            model.Reason = Reason;
            return model;
        }

        public ProjectRecommendedReason MergeDataModel(ProjectRecommendedReason model)
        {
            model.Reason = Reason;
            return model;
        }
        #endregion
    }
}