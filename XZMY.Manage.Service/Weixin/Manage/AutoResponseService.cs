using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Utils.DataDictionary;

namespace XZMY.Manage.Service.Weixin.Manage
{
    /// <summary>
    /// 自动回复内容管理
    /// </summary>
    public class AutoResponseService
    {
        private static Guid DataId = Guid.Parse("CBA56F06-950F-4DB7-B881-A17CE09EB900");//
        private static string Key = "SubscribeAutoResponse";//关注自动回复

        /// <summary>
        /// 保存更新内容
        /// </summary>
        /// <param name="content"></param>
        public static void CreateOrUpdate(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return;

            var item = DataDictionaryManager.GetDataById(Key, DataId);

            if (item == null)
            {
                Create(content);
            }
            else
            {
                item.Name = content;
                Update(item);
            }
        }

        /// <summary>
        /// 获取关注自动回复内容
        /// </summary>
        /// <returns></returns>
        public static string GetContent()
        {
            var item = DataDictionaryManager.GetDataById(Key, DataId);
            return item != null ? item.Name : string.Empty;
        }

        #region Private method

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        private static void Create(string content)
        {
            var item = new DataDictionaryItem
            {
                DataId = DataId,
                Name = content,
                EName = Key,
                IsDefault = false,
                IsSystem = true,
                Sort = 0,
                State = 1,
                Descr = "用户关注后自动回复内容"
            };

            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        private static void Update(DataDictionaryItem item)
        {
            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        #endregion
    }
}
