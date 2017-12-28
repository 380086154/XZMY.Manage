using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Order
{
    [Serializable]
    [DataContract]
    public class SmOrderWeb
    {
        /// <summary>
        /// 类型  1课程还是  2活动
        /// </summary>
        [DataMember]
        public int type { get; set; }
        /// <summary>
        /// 活动或是课程ID
        /// </summary>
        [DataMember]
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 出发城市
        /// </summary>
        [DataMember]
        public String DepartureCity { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        [DataMember]
        public Guid StudentId{ get; set; }
        /// <summary>
        /// 学生学历
        /// </summary>
        [DataMember]
        public String StudentEducation { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        [DataMember]
        public String StudentName { get; set; }
        /// <summary>
        /// 学生Email
        /// </summary>
        [DataMember]
        public String StudentEmail { get; set; }
        /// <summary>
        /// 学生手机号
        /// </summary>
        [DataMember]
        public String StudentMobile { get; set; }
        /// <summary>
        /// 学生所在地区ID
        /// </summary>
        [DataMember]
        public Guid StudentLocationId { get; set; }
        /// <summary>
        /// 学生所在地区名字
        /// </summary>
        [DataMember]
        public String StudentLocationName { get; set; }
        /// <summary>
        /// 学生地址
        /// </summary>
        [DataMember]
        public String StudentAddress { get; set; }
        /// <summary>
        /// 学生身份证号码
        /// </summary>
        [DataMember]
        public String IdentityCard { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [DataMember]
        public String ContactName { get; set; }
        /// <summary>
        /// 联系人邮件
        /// </summary>
        [DataMember]
        public String ContactEmail { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [DataMember]
        public String ContactMobile { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        [DataMember]
        public String Remark { get; set; }

    }
}
