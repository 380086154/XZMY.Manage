using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    [Serializable]
    [DataContract]
    public class SmStudentPlanProgram : IActionServiceModel2C<StudentPlanProgram>
    {
        #region Properties 
        /// <summary>
        /// 年级活动主键ID
        /// </summary>
        [DataMember]
        public Guid DataId { get; set; }
        /// <summary>
        /// 规划年级ID
        /// </summary>
        [DataMember]
        public Guid StudentPlanId { get; set; }
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        [DataMember]
        public int Type { get; set; }
        /// <summary>
        /// 获取 类型  活动 课程
        /// </summary>
        [DataMember]
        public String TypeName {
            get
            {
                string strType = "活动";
                switch (Type) {
                    case 1:
                        strType = "活动";
                        break;
                    case 2:
                        strType = "课程";
                        break;
                }
                return strType;
            }
        }
        /// <summary>
        /// 活动或是 课程的ID
        /// </summary>
        [DataMember]
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 活动或是课程名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 项目分类名字 领导能力
        /// </summary>
        [DataMember]
        public String ItemName { get; set; }
        /// <summary>
        /// 规划活动或课程图片
        /// </summary>
        [DataMember]
        public String Images { get; set; }
        /// <summary>
        /// 成长英语
        /// </summary>
        [DataMember]
        public Decimal AddEnglishScore { get; set; }
        /// <summary>
        /// 成长学术
        /// </summary>
        [DataMember]
        public Decimal AddLearnScore { get; set; }
        /// <summary>
        /// 成长素质
        /// </summary>
        [DataMember]
        public Decimal AddQualityScore { get; set; }
        #endregion


        public StudentPlanProgram CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new StudentPlanProgram();
            //model.Id = Id;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.Name = Name;
            model.ItemName = ItemName;
            model.Images = Images;
            model.ProgramId = ProgramId;
            model.StudentPlanId = StudentPlanId;
            model.Type = Type;
            
            return model;
        }
    }
}
