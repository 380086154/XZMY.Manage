using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    /// <summary>
    /// 年级模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmWebStudentPlan
    {
        /// <summary>
        /// ID 规划年级主键ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 规划年级名称 如 高中一年级
        /// </summary>
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 学校类型  普通学校、重点学校、国际学校
        /// </summary>
        [DataMember]
        public String SchoolType { get; set; }
        /// <summary>
        /// 国内
        /// </summary>
        [DataMember]
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 学校排序 从小到大
        /// </summary>
        [DataMember]
        public Int32 Sort { get; set; }
        /// <summary>
        /// 年级的描述
        /// </summary>
        [DataMember]
        public String GradeDescription { get; set; }
    }

    /// <summary>
    /// 年级活动模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmWebStudentPlanProgram {
        /// <summary>
        /// 年级活动ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 课程活动名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 课程活动描述
        /// </summary>
        [DataMember]
        public String Description { get; set; }
        /// <summary>
        /// 活动课程类型
        /// </summary>
        [DataMember]
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public String ProgramImage { get; set; }
        /// <summary>
        /// 类型名称  学习能力    领导能力
        /// </summary>
        [DataMember]
        public String ItemName { get; set; }
        /// <summary>
        /// 模板生成最新的活动ID
        /// </summary>
        [DataMember]
        public Guid ItemID { get; set; }
        /// <summary>
        /// 是否下订单
        /// </summary>
        [DataMember]
        public Int32 IsOrder { get; set; } 
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        [DataMember]
        public int Type { get; set; }
        /// <summary>
        /// 下订单 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
    }

    /// <summary>
    /// 模板模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmWebProgramTemplate {

        /// <summary>
        /// 模板ID
        /// </summary>
        [DataMember]
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 课程活动名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 课程活动描述
        /// </summary>
        [DataMember]
        public String Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public String ProgramImage { get; set; }
        /// <summary>
        /// 类型名称  学习能力    领导能力
        /// </summary>
        public String ItemName { get; set; }
    }
}
