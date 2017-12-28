using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Service
{

    /// <summary>
    /// 用户信息实体
    /// </summary>
    [Serializable]
    public class CurrentUserAccountModel : IActorInfomationSynchronizer
    {
        public String IP { get; set; }
        public Guid AccountId { get; set; }

        public Guid CreatorId { get; set; }

        /// <summary>
        /// 获取或设置事件创建人姓名。
        /// </summary>
        public String CreatorName { get; set; }

        /// <summary>
        /// 获取或设置事件创建时间。
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置事件最后修改人Id。
        /// </summary>
        public Guid ModifierId { get; set; }

        /// <summary>
        /// 获取或设置事件最后修改人姓名。
        /// </summary>
        public String ModifierName { get; set; }

        /// <summary>
        /// 获取或设置事件最后修改时间。
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        #region General settings
        /// <summary>
        /// 获取或设置姓名
        /// </summary>
        public String AccountName { get; set; }
        /// <summary>
        /// 获取或设置姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 获取或设置电子邮件
        /// </summary>
        public String Email { get; set; }

        #endregion
        
        /// <summary>
        /// 获取当前登录人信息
        /// </summary>
        /// <returns></returns>
        public ActorInfomationSynchronizer GetActorInfomationSynchronizer()
        {
            return new ActorInfomationSynchronizer
            {
                Id = AccountId,
                Name = AccountName,
                Time = DateTime.Now
            };
        }
    }
}
