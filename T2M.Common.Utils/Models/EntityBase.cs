using System;

namespace T2M.Common.Utils.Models
{
    /// <summary>
    /// 创建者，修改者基础信息
    /// </summary>
    [Serializable]
    public class EntityBase : IEntity<Guid>, IActorInfomationReader
    {
        #region Constructors

        /// <summary>
        /// 构建<see cref="EntityBase"/>类的一个实例化对象。
        /// </summary>
        public EntityBase()
        {

        }

        #endregion

        #region Members of LI.Common.Utils.Model.IEntity

        /// <summary>
        /// 获取或设置唯一标识的Id。
        /// </summary>
        public virtual Guid DataId { get; set; }

        ///// <summary>
        ///// 获取或设置用户帐户创建人Id。
        ///// </summary>
        //public virtual Guid CreatorId { get; set; }

        ///// <summary>
        ///// 获取或设置用户帐户创建人姓名。
        ///// </summary>
        //public virtual String CreatorName { get; set; }

        /// <summary>
        /// 获取或设置用户帐户创建时间。
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        ///// <summary>
        ///// 获取或设置用户帐户最后修改人Id。
        ///// </summary>
        //public virtual Guid ModifierId { get; set; }

        ///// <summary>
        ///// 获取或设置用户帐户最后修改人姓名。
        ///// </summary>
        //public virtual String ModifierName { get; set; }

        ///// <summary>
        ///// 获取或设置用户帐户最后修改时间。
        ///// </summary>
        //public virtual DateTime ModifiedTime { get; set; }

        #endregion

        #region Public Method
        /// <summary>
        /// 设置基础字段信息
        /// </summary>
        /// <param name="entity">基础字段信息</param>
        public void SetActorInfomation(ActorInfomationSynchronizer entity)
        {

            //CreatorId = entity.Id;
            //CreatorName = entity.Name;
            CreatedTime = entity.Time;

            //ModifierId = entity.Id;
            //ModifierName = entity.Name;
            //ModifiedTime = entity.Time;
        }

        /// <summary>
        /// 设置基础字段信息
        /// </summary>
        /// <param name="entity">基础字段信息</param>
        public void SetModifier(ActorInfomationSynchronizer entity)
        {
            //ModifierId = entity.Id;
            //ModifierName = entity.Name;
            //ModifiedTime = entity.Time;
        }

        #endregion

        public IActorInfomationReader GetActorInfomation()
        {
            return new EntityBase
            {
                DataId = DataId,
                //CreatorId = CreatorId,
                //CreatorName = CreatorName,
                CreatedTime = DateTime.Now,

                //ModifierId = ModifierId,
                //ModifierName = ModifierName,
                //ModifiedTime = DateTime.Now,
            };
        }
    }
}
