using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Auth.Models
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    [Serializable]
    [DataContract]
    public class RoleMenu
    {
        public RoleMenu()
        {
            Modules = new List<MenuModule>();
        }

        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public List<MenuModule> Modules { get; set; }

        public void Merge(RoleMenu menu)
        {
            Modules.AddRange(menu.Modules);
            var groups = Modules.GroupBy(m => m.Id).ToList();
            var res = groups.Select(m =>
            {
                var r = m.First();
                foreach (var item in m)
                {
                    if (r != item)
                    {
                        r.Items.AddRange(item.Items);
                        r.Items = r.Items.Distinct(o => o.Id).ToList();
                    }
                }
                return r;
            }).ToList();
            Modules = res;
        }

        public Dictionary<string, List<string>> CreateResource()
        {
            var res = new Dictionary<string, List<string>>();

            foreach (var item in Modules)
            {
                res[item.Code] = item.Items.Select(m => m.Code).ToList();
            }

            return res;
        }

        public Guid[] GetModuleIdList()
        {
            return Modules.Select(m => m.Id).ToArray();
        }

        public Guid[] GetActionIdList()
        {
            var res = new List<Guid>();
            Modules.ForEach(m=>m.Items.ForEach(n=>res.Add(n.Id)));
            return res.ToArray();
        }
    }
}
