
using System;
using System.Runtime.Serialization;

namespace XZMY.Manage.Service.Utils.DataDictionary
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataDictionaryItem
    {
        [DataMember]
        public Guid DataId { get; set; }
        /// <summary>
        /// 数据项名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// 数据项[英文]，由分类名称和数据字典项英文名组成，格式为：CatagoryName_EName
        /// </summary>
        [DataMember]
        public String EName { get; set; }

        /// <summary>
        /// 是否默认字典项
        /// </summary>
        [DataMember]
        public Boolean IsDefault { get; set; }

        /// <summary>
        /// 是否为系统数据字典项（注：该项不能被编辑）
        /// </summary>
        [DataMember]
        public Boolean IsSystem { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public Int32 Sort { get; set; }

        /// <summary>
        /// 状态：1正常，2逻辑删除
        /// </summary>
        [DataMember]
        public Int32 State { get; set; }

        /// <summary>
        /// State(状态)逻辑删除枚举
        /// </summary>
        public enum StateEnum
        {
            /// <summary>
            /// //启用/正常
            /// </summary>
            Enable = 1,
            /// <summary>
            /// //禁用/删除
            /// </summary>
            LogicDeleted = 2
        }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public String Descr { get; set; }
    }
}
