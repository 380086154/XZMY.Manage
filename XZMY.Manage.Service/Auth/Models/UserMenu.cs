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
    public class UserMenu : RoleMenu
    {
        public UserMenu() : base()
        {
            Roles = new List<Guid>();
        }

        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public List<Guid> Roles { get; set; }
    }
}
