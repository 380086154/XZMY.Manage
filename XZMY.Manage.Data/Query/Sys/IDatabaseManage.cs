using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query.Interface;

namespace XZMY.Manage.Data.Query.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseManage
    {
        /// <summary>
        /// 分店 Id
        /// </summary>
        Guid BranchNameDataId { get; set; }

        /// <summary>
        /// 表名称集合
        /// </summary>
        List<string> TablenameList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Execute();
    }
}
