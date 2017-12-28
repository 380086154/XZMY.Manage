using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Service.Auth.Models
{
    [Serializable]
    [DataContract]
    public class MenuItem
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ModuleCode { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public EVisible Visible { get; set; }
        [DataMember]
        public EState State { get; set; }
        [DataMember]
        public int Sort { get; set; }
    }
}