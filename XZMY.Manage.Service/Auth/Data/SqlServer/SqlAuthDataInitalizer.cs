using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Auth.Models;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using System.Web.Configuration;
using System.Web;

namespace XZMY.Manage.Service.Auth.Data.SqlServer
{
    public class SqlAuthDataInitalizer : IAuthDataInitializer
    {
        public Dictionary<Guid, RoleResource> LoadRoleResource()
        {
            var query = new GetEntityTable<Sys_Action>();
            var actions = query.Execute();
            var query1 = new GetEntityTable<Sys_Role>();
            var roles = query1.Execute();
            var query2 = new GetEntityTable<Sys_Module>();
            var modules = query2.Execute();
            var query3 = new GetEntityTable<Sys_RoleAction>();
            var role_actions = query3.Execute();
            var query4 = new GetEntityTable<Sys_RoleModule>();
            var role_modules = query4.Execute();
            //var siteUrl = WebConfigurationManager.AppSettings["SiteUrl"];
            //var url = HttpContext.Current.Request.Url.ToString();
            //var httpContext = HttpContext.Current;
            //if (httpContext != null)
            //{
            //    var prefix = "http://" + httpContext.Request.UrlReferrer.Authority + "/";
            //    WebConfigurationManager.AppSettings["SiteUrl"] = prefix;
            //    siteUrl = prefix;
            //}

            var res = new Dictionary<Guid, RoleResource>();
            foreach (var role in roles)
            {
                var rolemenu = new RoleMenu { RoleId = role.DataId, Modules = new List<MenuModule>() };
                var rms = role_modules.Where(m => m.RoleId == role.DataId).Distinct(m => m.ModuleId)
                    .Select(m => modules.FirstOrDefault(n => n.DataId == m.ModuleId)).Where(m => m != null).OrderBy(x => x.Sort);
                rolemenu.Modules.AddRange(rms.Select(m => new MenuModule
                {
                    Id = m.DataId,
                    FontIconsClass = m.FontIconsClass,
                    Code = m.Code,
                    Name = m.Name,
                    State = m.State,
                    Visible = m.Visible,
                    Sort = m.Sort,
                    Items = new List<MenuItem>()
                }));

                var ras = role_actions.Where(m => m.RoleId == role.DataId).Distinct(m => m.ActionId)
                    .Select(m => actions.FirstOrDefault(n => n.DataId == m.ActionId)).Where(m => m != null).OrderBy(x => x.Sort);
                foreach (var action in ras)
                {
                    var rm = rolemenu.Modules.FirstOrDefault(m => m.Id == action.ModuleId);
                    if (rm == null) continue;
                    rm.Items.Add(new MenuItem
                    {
                        Id = action.DataId,
                        Code = action.Code,
                        ModuleCode = action.ModuleCode,
                        Name = action.Name,
                        Sort = action.Sort,
                        State = action.State,
                        Url = action.Url,
                        Visible = action.Visible
                    });
                }
                res[role.DataId] = new RoleResource(rolemenu);
            }


            return res;
        }
        public IList<Sys_Module> GetModuleList()
        {
            var query2 = new GetEntityTable<Sys_Module>();
            var modules = query2.Execute();
            return modules;
        }
        public IList<Sys_Action> GetActionList()
        {
            var query2 = new GetEntityTable<Sys_Action>();
            var modules = query2.Execute();
            return modules;
        }
    }

}
