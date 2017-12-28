using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Auth.Models;

namespace XZMY.Manage.Service.Auth
{
    public class AuthCenter
    {
        private static AuthDataSource _dataSource;
        static AuthCenter()
        {
            _dataSource = new AuthDataSource();
        }
        public static AuthDataSource GetDataSource()
        {
            return _dataSource;
        }
        public static bool ModuleAuthorityCheck(Guid userid, string moduleName)
        {
            var resource = GetUserResource(userid);
            if (resource == null) return false;
            return (resource.Resources.ContainsKey(moduleName));
        }
        public static bool ActionAuthorityCheck(Guid userid, string moduleName, string actionName)
        {
            var resource = GetUserResource(userid);
            if (resource == null) return false;
            if (!resource.Resources.ContainsKey(moduleName)) return false;
            return resource.Resources[moduleName].Contains(actionName);
        }


        public static RoleResource GetRoleResource(Guid roleid)
        {
            return _dataSource.GetRoleResource(roleid);
        }
        public static UserResource GetUserResource(Guid userid)
        {
            return _dataSource.GetUserResource(userid);
        }

        public static void ClearUserResourceCache(Guid userid)
        {
            _dataSource.ClearUserResourceCache(userid);
        }

        public static void ClearRoleResourceCache(Guid roleid)
        {

            _dataSource.ClearRoleResourceCache(roleid);
        }
        public static void ClearAllCache()
        {
            _dataSource.ClearAllCache();
        }

    }
}
