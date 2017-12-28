using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using XZMY.Manage.Service.Auth.Models;
using Newtonsoft.Json;

namespace XZMY.Manage.Service.Auth.Data
{
    class BasicAuthDataCacheWritter : IAuthDataCacheWritter
    {
        public static object _gate = new object();
        private static string _dirpath = HttpContext.Current.Server.MapPath("~/") + "/App_Data/AuthCache/";

        public object Gate
        {
            get
            {
                return _gate;
            }
        }

        public void DeleteAll()
        {
            lock (_gate)
            {
                if (Directory.Exists(_dirpath))
                    Directory.Delete(_dirpath, true);

                Directory.CreateDirectory(_dirpath);
            }
        }

        public void DeleteUserResource(Guid userid)
        {
            lock (_gate)
            {
                if (!Directory.Exists(_dirpath)) Directory.CreateDirectory(_dirpath);
                File.Delete(_dirpath + userid);
            }
        }

        public void SaveRoleResource(Dictionary<Guid, RoleResource> resource)
        {
            lock (_gate)
            {
                if (!Directory.Exists(_dirpath)) Directory.CreateDirectory(_dirpath);
                var path = _dirpath + "Role";
                var json = JsonConvert.SerializeObject(resource);
                File.WriteAllText(path, json);
            }
        }

        public void SaveUserResource(UserResource ur)
        {
            lock (_gate)
            {
                if (!Directory.Exists(_dirpath)) Directory.CreateDirectory(_dirpath);
                var path = _dirpath + ur.UserId;
                var json = JsonConvert.SerializeObject(ur);
                File.WriteAllText(path, json);
            }
        }
    }
}
