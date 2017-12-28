using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Auth.Models;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using T2M.Common.DataServiceComponents.Data.Impl.Query;

namespace XZMY.Manage.Service.Auth.Data.SqlServer
{
    /// <summary>
    /// 角色数据加载
    /// </summary>
    public class SqlAuthDataLoader : IAuthDataLoader
    {

        public RoleResource GetAllResource()
        {

            var query = new GetEntityTable<Sys_Action>();
            var actions = query.Execute();
            var query2 = new GetEntityTable<Sys_Module>();
            var modules = query2.Execute();

            var rolemenu = new RoleMenu { RoleId = Guid.Empty, Modules = new List<MenuModule>() };

            rolemenu.Modules.AddRange(modules.Select(m => new MenuModule() { Id = m.DataId, Code = m.Code, Name = m.Name, State = m.State, Visible = m.Visible, Items = new List<MenuItem>() }));
            foreach (var action in actions)
            {
                var rm = rolemenu.Modules.FirstOrDefault(m => m.Id == action.ModuleId);
                if (rm == null) continue;
                rm.Items.Add(new MenuItem
                {
                    Id = action.DataId,
                    Code = action.Code,
                    ModuleCode = action.ModuleCode,
                    Name = action.Name,
                    State = action.State,
                    Url = action.Url,
                    Visible = action.Visible
                });
            }
            return new RoleResource(rolemenu);
        }

        public RoleResource GetRoleResource(Guid roleid)
        {
            var query1 = new GetEntityById<Sys_Role>();
            query1.Id = roleid;
            var role = query1.Execute();

            if (role == null) return null;

            var query = new GetEntityTable<Sys_Action>();
            var actions = query.Execute();
            var query2 = new GetEntityTable<Sys_Module>();
            var modules = query2.Execute();
            var query3 = new GetEntityByForeignId<Sys_RoleAction>()
            {
                ForeignId = roleid,
                ForeignMember = m => m.RoleId
            };
            var role_actions = query3.Execute();
            var query4 = new GetEntityByForeignId<Sys_RoleModule>()
            {
                ForeignId = roleid,
                ForeignMember = m => m.RoleId
            };
            var role_modules = query4.Execute();


            var rolemenu = new RoleMenu { RoleId = role.DataId, Modules = new List<MenuModule>() };
            var rms = role_modules.Where(m => m.RoleId == role.DataId).Distinct(m => m.ModuleId)
                .Select(m => modules.FirstOrDefault(n => n.DataId == m.ModuleId)).Where(m => m != null);
            rolemenu.Modules.AddRange(rms.Select(m => new MenuModule
            {
                Id = m.DataId,
                Code = m.Code,
                Name = m.Name,
                State = m.State,
                Visible = m.Visible,
                Sort = m.Sort,
                FontIconsClass = m.FontIconsClass,
                Items = new List<MenuItem>()
            }));

            var ras = role_actions.Where(m => m.RoleId == role.DataId).Distinct(m => m.ActionId)
                .Select(m => actions.FirstOrDefault(n => n.DataId == m.ActionId)).Where(m => m != null);
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
                    State = action.State,
                    Url = action.Url,
                    Visible = action.Visible,
                    Sort = action.Sort
                });
            }
            return new RoleResource(rolemenu);
        }

        public UserResource GetUserResource(Guid userid, IDictionary<Guid, RoleResource> rolemenu)
        {
            var query = new GetEntityByForeignId<Sys_UserRole>
            {
                ForeignId = userid,
                ForeignMember = m => m.UserId
            };
            var roles = query.Execute();


            var res = new UserResource() { UserId = userid, Roles = roles.Select(m => m.RoleId).Distinct().ToList() };
            var rolemenus = roles.Where(m => rolemenu.ContainsKey(m.RoleId)).Select(m => rolemenu[m.RoleId]).ToList();

            rolemenus.ForEach(res.Merge);

            return res;
        }
    }
}
