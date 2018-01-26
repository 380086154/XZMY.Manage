using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Sys
{
    public class VmBranchEdit : ViewBase, IActionViewModel<BranchDto>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 特征值
        /// <remark>通过 CpuId|DiskId|MAC 地址，外网 IP 地址等信息动态判断店名</remark>
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
        /// 发件人
        /// </summary>
        public string ToEmail { get; set; }

        public BranchDto CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return this.ConvertTo<BranchDto>();
        }

        public BranchDto MergeDataModel(BranchDto model)
        {

            return model;
        }
    }
}
