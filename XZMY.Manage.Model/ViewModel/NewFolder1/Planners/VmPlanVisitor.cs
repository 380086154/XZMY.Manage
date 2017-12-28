using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Planners;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmPlanVisitor : IActionViewModel<PlanVisitor>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public String Mobile { get; set; }
        /// <summary>
        /// 联系人邮件
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// 意向国家名字
        /// </summary>
        public String Country { get; set; }
        /// <summary>
        /// 出国时间
        /// </summary>
        public DateTime AbroadDate { get; set; }
        public String AbroadDateName {
            get {
                return AbroadDate.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 当前年级
        /// </summary>
        public String Grade { get; set; }
        public DateTime CreatedTime { get; set; }
        public String CreatedTimeName {
            get {
                return CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #region Extendsions

        public PlanVisitor CreateNewDataModel()
        {
            var model = new PlanVisitor();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name= Name;
            model.Mobile= Mobile;
            model.Email= Email;
            model.Country= Country;
            model.AbroadDate= AbroadDate;
            model.Grade = Grade;
            model.CreatedTime = CreatedTime;
            return model;
        }

        public PlanVisitor MergeDataModel(PlanVisitor model)
        {
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.Country = Country;
            model.AbroadDate = AbroadDate;
            model.Grade = Grade;
            model.CreatedTime = CreatedTime;
            return model;
        }
        #endregion
    }
}
