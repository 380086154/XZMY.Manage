using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XZMY.Manage.Service.Utils.DataDictionary
{
    /// <summary>
    /// 单个数据字典管理基类
    /// </summary>
    public class DataDictionarySingleItemBase
    {
        public DataDictionaryItem SingleItem { get; set; }

        /// <summary>
        /// 保存 或 更新
        /// </summary>
        /// <param name="item"></param>
        public void SaveOrUpdate()
        {
            //if (string.IsNullOrWhiteSpace(model.)) return;

            var entity = DataDictionaryManager.GetDataById(SingleItem.EName, SingleItem.DataId);

            if (entity == null)
            {
                entity = SingleItem;
            }
            else
            {
                entity = SingleItem.ConvertTo<DataDictionaryItem>();
            }

            DataDictionaryManager.SaveOrUpdateData(SingleItem.EName, entity);
        }

        /// <summary>
        /// 获取 Value
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            var item = DataDictionaryManager.GetDataById(SingleItem.EName, SingleItem.DataId);
            return item != null ? HttpUtility.UrlDecode(item.Value) : string.Empty;
        }
    }
}
