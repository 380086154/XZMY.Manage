using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Project
{
    public class VmProjectDate :ViewBase, IActionViewModel<ProjectDate>
    {

        #region Properties 

        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("ProjectId")] 
        //[DisplayName("活动Id")] 
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        //[EntAttributes.DBColumn("BeginDate")] 
        //[DisplayName("开始日期")] 
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        //[EntAttributes.DBColumn("EndDate")] 
        //[DisplayName("结束日期")] 
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 出发城市 用, 逗号分隔
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCity")] 
        //[DisplayName("出发城市")] 
        public String DepartureCity
        {
            get; set;
        }
        #endregion
        #region Extendsions

        public ProjectDate CreateNewDataModel()
        {
            var model = new ProjectDate();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectId = ProjectId;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.DepartureCity = DepartureCity;
            return model;
        }

        public ProjectDate MergeDataModel(ProjectDate model)
        {
            model.ProjectId = ProjectId;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.DepartureCity = DepartureCity;
            return model;
        }
        #endregion
    }
}
