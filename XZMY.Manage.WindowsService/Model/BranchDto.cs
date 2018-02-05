using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Model
{
    public class BranchDto
    {
        //
        // 摘要: 
        //     获取或设置唯一标识的Id。
        public Guid DataId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关键值
        /// <remark>通过 CpuId|MAC|DiskId 地址，外网 IP 地址等信息动态判断店名</remark>
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 状态 1启用 2禁用
        /// </summary>
        public EState State { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// 获取或设置用户帐户创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }


    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum EState
    {
        其它 = 0,
        启用 = 1,
        禁用 = 2
    }
}
