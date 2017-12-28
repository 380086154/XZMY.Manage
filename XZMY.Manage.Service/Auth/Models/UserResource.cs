using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Auth.Models
{

    [Serializable]
    [DataContract]
    public class UserResource : RoleResource
    {

        public UserResource()
        {
            Menu = new UserMenu();
            Resources = Menu.CreateResource();
        }
        public UserResource(UserMenu menu) : base(menu)
        {
            UserId = menu.UserId;
        }

        [DataMember]
        public Guid UserId { get; set; }
        
        [IgnoreDataMember]
        public List<Guid> Roles
        {
            get { return Menu.As<UserMenu>().Roles; }
            set { Menu.As<UserMenu>().Roles = value; }
        }
        public void Merge(RoleResource resource)
        {
            if (!Roles.Contains(resource.RoleId)) Roles.Add(resource.RoleId);
            Menu.Merge(resource.Menu);
            foreach (var item in resource.Resources)
            {
                if (!Resources.ContainsKey(item.Key))
                    Resources.Add(item.Key, item.Value);
                else
                {
                    Resources[item.Key].AddRange(item.Value);
                    Resources[item.Key] = Resources[item.Key].Distinct().ToList();
                }
            }
        }
    }
}
