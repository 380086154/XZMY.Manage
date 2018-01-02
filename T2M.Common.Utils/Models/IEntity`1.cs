using System;

namespace T2M.Common.Utils.Models
{
    /// <summary>
    /// 代表了构建业务实体模型的基类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// 获取或设置唯一标识事件的Id。
        /// </summary>
        T DataId { get; set; }

        ///// <summary>
        ///// 获取或设置事件创建人Id。
        ///// </summary>
        //Guid CreatorId { get; set; }

        ///// <summary>
        ///// 获取或设置事件创建人姓名。
        ///// </summary>
        //String CreatorName { get; set; }

        /// <summary>
        /// 获取或设置事件创建时间。
        /// </summary>
        DateTime CreatedTime { get; set; }

        ///// <summary>
        ///// 获取或设置事件最后修改人Id。
        ///// </summary>
        //Guid ModifierId { get; set; }

        ///// <summary>
        ///// 获取或设置事件最后修改人姓名。
        ///// </summary>
        //String ModifierName { get; set; }

        ///// <summary>
        ///// 获取或设置事件最后修改时间。
        ///// </summary>
        //DateTime ModifiedTime { get; set; }
    }
}