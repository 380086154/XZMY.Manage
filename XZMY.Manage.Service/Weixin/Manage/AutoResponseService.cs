using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XZMY.Manage.Service.Utils.DataDictionary;

namespace XZMY.Manage.Service.Weixin.Manage
{
    /// <summary>
    /// 自动回复内容管理
    /// </summary>
    public class AutoResponseService : DataDictionarySingleItemBase
    {
        private Guid DataId = Guid.Parse("CBA56F06-950F-4DB7-B881-A17CE09EB900");//
        private string Key = "SubscribeAutoResponse";//关注自动回复

        public AutoResponseService()
        {
            this.SingleItem = new DataDictionaryItem
            {
                DataId = DataId,
                Name = "关注自动回复",
                EName = Key,
                Value = string.Empty,
                IsDefault = false,
                IsSystem = true,
                Sort = 0,
                State = 1,
                Descr = "用户关注后自动回复内容"
            };
        }
    }
}
