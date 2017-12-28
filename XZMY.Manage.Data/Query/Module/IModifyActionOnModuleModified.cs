using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query.Interface;

namespace XZMY.Manage.Data.Query.Module
{
    public interface IModifyActionOnModuleModified : ISubQuery<int>
    {
        string TableName { get; set; }
        Guid ModuleId { get; set; }
        string Code { get; set; }
    }
}
