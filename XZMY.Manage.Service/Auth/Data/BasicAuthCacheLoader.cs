using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XZMY.Manage.Service.Auth.Models;

namespace XZMY.Manage.Service.Auth.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicAuthDataCacheLoader : IAuthDataCacheLoader
    {
        private static string _dirpath = HttpContext.Current.Server.MapPath("~/") + "/App_Data/AuthCache/";
        public Dictionary<Guid, RoleResource> LoadRoleResource(object gate)
        {
            lock (gate)
            {
                try
                {
                    var path = _dirpath + "Role";
                    if (!File.Exists(path)) return null;
                    var str = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<Dictionary<Guid, RoleResource>>(str);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public UserResource LoadUserResource(Guid userid, object gate)
        {
            lock (gate)
            {
                try
                {
                    var path = _dirpath + userid;
                    if (!File.Exists(path)) return null;
                    var str = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<UserResource>(str);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
