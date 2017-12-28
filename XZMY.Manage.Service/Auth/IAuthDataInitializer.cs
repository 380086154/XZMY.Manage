using System;
using System.Collections.Generic;
using XZMY.Manage.Service.Auth.Models;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using T2M.Common.DataServiceComponents.Data.Impl.Query;

namespace XZMY.Manage.Service.Auth
{
    internal interface IAuthDataInitializer
    {
        Dictionary<Guid, RoleResource> LoadRoleResource();
        IList<Sys_Module> GetModuleList();

        IList<Sys_Action> GetActionList();
    }
}