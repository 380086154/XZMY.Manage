using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Impl.Query.Sys;

namespace XZMY.Manage.Service.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseManageService
    {
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="branchNameDataId"></param>
        public void ClearDatabase(Guid branchNameDataId)
        {
            var databaseManage = new DatabaseManage();

            databaseManage.BranchNameDataId = branchNameDataId;
            databaseManage.TablenameList = new List<string>
            {"xfxx", "hyczk", "rz","zkk", "czk", "hyxx"};

            databaseManage.Execute();
        }
    }
}
