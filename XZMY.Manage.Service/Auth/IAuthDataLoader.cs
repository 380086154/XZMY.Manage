using System;
using System.Collections.Generic;
using XZMY.Manage.Service.Auth.Models;

namespace XZMY.Manage.Service.Auth
{
    internal interface IAuthDataLoader
    {
        UserResource GetUserResource(Guid userid, IDictionary<Guid, RoleResource> rolemenu);
        RoleResource GetRoleResource(Guid roleid);
        RoleResource GetAllResource();
    }
}