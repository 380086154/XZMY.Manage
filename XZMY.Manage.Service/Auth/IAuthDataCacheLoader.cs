using System;
using System.Collections.Generic;
using XZMY.Manage.Service.Auth.Models;

namespace XZMY.Manage.Service.Auth
{
    internal interface IAuthDataCacheLoader
    {
        Dictionary<Guid, RoleResource> LoadRoleResource(object gate);
        UserResource LoadUserResource(Guid userid, object gate);

    }
}