using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using T2M.Common.DataServiceComponents.Data.Query.Interface;

namespace XZMY.Manage.Service.Auth.Data.SqlServer.Query
{
    public interface IUpdateActionBase : ISubQuery<int>
    {
        Sys_Action Model { get; set; }
    }
}
