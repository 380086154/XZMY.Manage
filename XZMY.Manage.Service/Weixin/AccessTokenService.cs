using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Service.Utils.DataDictionary;

namespace XZMY.Manage.Service.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccessTokenService
    {
        /// <summary>
        /// Access_Token 数据 Id
        /// </summary>
        private static Guid DataId = Guid.Parse("9ADBE702-88FC-4C65-83CD-752BA7578FDE");//

        private static string Key = "AccessToken";

        /// <summary>
        /// 监控 Access_Token
        /// </summary>
        public static void Watch()
        {
            var thread = new Thread(CheckStatus) { IsBackground = true };
            thread.Start();
        }

        #region Private method

        private static void CheckStatus()
        {
            var item = DataDictionaryManager.GetDataById(string.Empty, DataId);
            if (item == null)
            {//数据不存在，创建
                Create();
            }
            else
            {//更新

                Update(item);
            }
        }

        /// <summary>
        /// 创建 AccessToken
        /// </summary>
        /// <returns></returns>
        private static void Create()
        {
            var item = new DataDictionaryItem
            {
                DataId = DataId,
                Name = "AccessToken",
                EName = "AccessToken",
                IsDefault = false,
                IsSystem = true,
                Sort = 0,
                State = 1,
                Descr = "微信 Access_Token，站点启动后会定期检查并更新"
            };

            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        /// <summary>
        /// 更新 AccessToken
        /// </summary>
        /// <param name="item"></param>
        private static void Update(DataDictionaryItem item)
        {
            DataDictionaryManager.SaveOrUpdateData(Key, item);
        }

        private static string GetAccessToken()
        {
            var token = string.Empty;


            return token;
        }

        #endregion
    }
}
