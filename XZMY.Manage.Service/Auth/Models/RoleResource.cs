using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

namespace XZMY.Manage.Service.Auth.Models
{
    /// <summary>
    /// 角色资源
    /// </summary>
    [Serializable]
    [DataContract]
    public class RoleResource
    {
        public RoleResource()
        {
            Menu = new RoleMenu();
            Resources = Menu.CreateResource();
        }
        public RoleResource(RoleMenu menu)
        {
            RoleId = menu.RoleId;
            Menu = menu;
            Resources = menu.CreateResource();
        }

        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public RoleMenu Menu { get; set; }

        [DataMember]
        public Dictionary<string,List<string>> Resources { get; set; }

        /// <summary>
        /// 获取模块Id集合
        /// </summary>
        /// <returns></returns>
        public Guid[] GetModuleIdList()
        {
            return Menu.GetModuleIdList();
        }

        /// <summary>
        /// 获取行为Id集合
        /// </summary>
        /// <returns></returns>
        public Guid[] GetActionIdList()
        {
            return Menu.GetActionIdList();
        }
    }
}
