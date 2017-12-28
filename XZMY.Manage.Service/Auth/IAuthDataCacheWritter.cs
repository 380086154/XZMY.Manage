using System;
using System.Collections.Generic;
using XZMY.Manage.Service.Auth.Models;

namespace XZMY.Manage.Service.Auth
{
    internal interface IAuthDataCacheWritter
    {
        object Gate { get; }

        void SaveRoleResource(Dictionary<Guid, RoleResource> resource);
        void DeleteAll();
        void SaveUserResource(UserResource ur);
        void DeleteUserResource(Guid userid);
    }
}