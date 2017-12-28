using System;
using XZMY.Manage.Model.DataModel.Project;

namespace XZMY.Manage.Model.ViewModel.Project
{
    public class VmProjectDepartureCity : IActionViewModel<ProjectDepartureCity>
    {
        public Guid DataId { get; set; }
        #region Properties 

        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动Id")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 活动时间id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectDateId")] 
        //[DisplayName("活动时间id")] 
        public Guid ProjectDateId { get; set; }
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

        public ProjectDepartureCity CreateNewDataModel()
        {
            var model = new ProjectDepartureCity();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectId = ProjectId;
            model.ProjectDateId = ProjectDateId;
            model.DepartureCityLocationId = DepartureCityLocationId;
            model.DepartureCity = DepartureCity;
            return model;
        }

        public ProjectDepartureCity MergeDataModel(ProjectDepartureCity model)
        {
            model.ProjectDateId = ProjectDateId;
            model.DepartureCityLocationId = DepartureCityLocationId;
            model.DepartureCity = DepartureCity;
            return model;
        }
        #endregion
    }
}