using System;
using XZMY.Manage.Model.DataModel.Courses;

namespace XZMY.Manage.Model.ViewModel.Courses
{
    public class VmCourseDepartureCity : IActionViewModel<CourseDepartureCity>
    {
        public Guid DataId { get; set; }
        #region Properties 

        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("活动Id")] 
        public Guid CourseId { get; set; }
        /// <summary>
        /// 活动时间id
        /// </summary>
        //[EntAttributes.DBColumn("CourseDateId")] 
        //[DisplayName("活动时间id")] 
        public Guid CourseDateId { get; set; }
        /// <summary>
        /// 出发地Id
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCityLocationId")] 
        //[DisplayName("出发地Id")] 
        public Guid DepartureCityLocationId { get; set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCity")] 
        //[DisplayName("出发地名称")] 
        public String DepartureCity { get; set; }

        #endregion
        #region Extendsions

        public CourseDepartureCity CreateNewDataModel()
        {
            var model = new CourseDepartureCity();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.CourseId = CourseId;
            model.CourseDateId = CourseDateId;
            model.DepartureCityLocationId = DepartureCityLocationId;
            model.DepartureCity = DepartureCity;
            return model;
        }

        public CourseDepartureCity MergeDataModel(CourseDepartureCity model)
        {
            model.CourseDateId = CourseDateId;
            model.DepartureCityLocationId = DepartureCityLocationId;
            model.DepartureCity = DepartureCity;
            return model;
        }
        #endregion
    }
}