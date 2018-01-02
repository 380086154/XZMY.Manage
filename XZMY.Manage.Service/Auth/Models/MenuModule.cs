using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Service.Auth.Models
{
    /// <summary>
    /// 角色模块
    /// </summary>
    [Serializable]
    [DataContract]
    public class MenuModule
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public List<MenuItem> Items { get; set; }

        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 字体图标样式
        /// </summary>
        [DataMember]
        public string FontIconsClass { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public EVisible Visible { get; set; }
        [DataMember]
        public EState State { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get { return State.ToString(); } }

        [DataMember]
        public int Sort { get; set; }
    }
}