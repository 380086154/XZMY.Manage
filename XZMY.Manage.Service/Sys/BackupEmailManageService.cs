using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Utils.DataDictionary;

namespace XZMY.Manage.Service.Sys
{
    /// <summary>
    /// 数据被备份收发邮件管理
    /// </summary>
    public class BackupEmailManageService : DataDictionarySingleItemBase
    {
        private Guid dataId = Guid.Parse("5ABC3D38-4ACD-49F5-A7E1-DA8085F7C945");//
        private string key = "BackupEmailManage";//数据备份邮件管理

        public BackupEmailManageService()
        {
            this.SingleItem = new DataDictionaryItem
            {
                DataId = dataId,
                Name = "收发邮件管理",
                EName = key,
                Value = string.Empty,
                IsDefault = false,
                IsSystem = true,
                Sort = 0,
                State = 1,
                Descr = "数据备份收发邮件管理"
            };
        }

        /// <summary>
        /// 获取过滤后的列表，主要是排除注释的邮件
        /// </summary>
        /// <returns></returns>
        public string GetFilterList()
        {
            var arr = GetValue().Split('|');

            var result = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                var item = arr[i];
                if (item.Contains("//") || item.Contains("--"))
                {
                    arr[i] = string.Join("\r\n", arr[i].Split("\r\n").Where(x => (!x.Contains("--") || item.Contains("//"))).ToArray());
                }
            }

            return string.Join("|", arr);
        }
    }
}
