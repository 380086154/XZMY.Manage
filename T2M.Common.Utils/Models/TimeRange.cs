using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2M.Common.Utils.Models
{
    /// <summary>
    /// 时间区间
    /// </summary>
    [Serializable]
    [DataContract]
    public class TimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime? EndTime { get; set; }
    }
}
