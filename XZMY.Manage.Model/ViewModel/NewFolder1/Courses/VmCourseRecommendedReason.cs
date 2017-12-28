using System;
using XZMY.Manage.Model.DataModel.Courses;

namespace XZMY.Manage.Model.ViewModel.Courses
{
    public class VmCourseRecommendedReason : IActionViewModel<CourseRecommendedReason>
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
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("活动id 外键")] 
        public Guid CourseId { get; set; }
        /// <summary>
        /// 推荐理由
        /// </summary>
        //[EntAttributes.DBColumn("Reason")] 
        //[DisplayName("推荐理由")] 
        public String Reason { get; set; }

        #endregion

        #region Extendsions

        public CourseRecommendedReason CreateNewDataModel()
        {
            var model = new CourseRecommendedReason();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.CourseId = CourseId;
            model.Reason = Reason;
            return model;
        }

        public CourseRecommendedReason MergeDataModel(CourseRecommendedReason model)
        {
            model.Reason = Reason;
            return model;
        }
        #endregion
    }
}