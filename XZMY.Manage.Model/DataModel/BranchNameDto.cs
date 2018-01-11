using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel
{
    /// <summary>
    /// 分店信息
    /// </summary>
    [Serializable]
    [DBTable("BranchName")]
    public class BranchNameDto : EntityBase, IDataModel
    {
        public BranchNameDto()
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 判断值
        /// <remark>通过 CpuId|DiskId|MAC 地址，外网 IP 地址等信息动态判断店名</remark>
        /// </summary>
        public string Value { get; set; }

    }
}
