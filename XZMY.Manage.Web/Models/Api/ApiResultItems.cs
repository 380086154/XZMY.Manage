using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XZMY.Manage.Web.Models.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiResultItems
    {
    }

    /// <summary>
    /// APi Items
    /// </summary>
    /// <typeparam name="T">约束 继承T </typeparam>
    public class ApiItemModel<T> where T : class
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="items">集合</param>
        public ApiItemModel(List<T> items)
        {
            Items = items;
            ItemsCount = Items.Count;
        }

        /// <summary>
        /// 集合条数
        /// </summary>
        public int ItemsCount { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public List<T> Items { get; set; }
    }
}