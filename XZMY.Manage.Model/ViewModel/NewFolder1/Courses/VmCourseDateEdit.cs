using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Courses
{
    public class VmCourseDate:ViewBase, IActionViewModel<CourseDate>
    {
        #region Properties 

        /// <summary>
        /// 活动Id
        /// </summary>
        //[EntAttributes.DBColumn("CourseId")] 
        //[DisplayName("活动Id")] 
        public Guid CourseId { get; set; }
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
        /// 出发城市
        /// </summary>
        //[EntAttributes.DBColumn("DepartureCity")] 
        //[DisplayName("出发城市")] 
        public String DepartureCity { get; set; }
        #endregion
        #region Extendsions

        public CourseDate CreateNewDataModel()
        {
            var model = new CourseDate();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.CourseId = CourseId;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.DepartureCity = DepartureCity;
            return model;
        }

        public CourseDate MergeDataModel(CourseDate model)
        {
            model.CourseId = CourseId;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.DepartureCity = DepartureCity;
            return model;
        }
        #endregion
    }
}
