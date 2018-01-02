using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel.Courses;

namespace XZMY.Manage.Model.ViewModel.Order
{
    [Serializable]
    public class VmOrderCourseDetails 
    {

        /// <summary>
        /// 课程订单信息
        /// </summary>
        public VmOrderCourse modelVmOrderCourse { get; set; }
        /// <summary>
        /// 课程信息
        /// </summary>
        public VmCourseEdit modelVmCourse { get; set; }
        /// <summary>
        /// 是否支持编辑评论
        /// </summary>
        public Boolean IsEditComment { get; set; } 
    }

}
