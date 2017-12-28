using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class PagedResult<T>
    {      
        #region Constructors

        /// <summary>
        /// 初始化一个泛型<see>
        ///         <cref>PagedResult</cref>
        ///     </see>
        ///     对象，该构造方法只能由子类调用
        /// </summary>
        public PagedResult()
        {
            
        }

        /// <summary>
        /// 初始化一个泛型<see>
        ///         <cref>PagedResult</cref>
        ///     </see>
        ///     对象，该构造方法只能由子类调用
        /// </summary>
        /// <param name="totalCount">所有信息条数</param>
        /// <param name="totalPages">所有页数</param>
        /// <param name="pageIndex">不小于1的页码</param>
        /// <param name="pageSize">分页显示的内容条数</param>
        /// <param name="results">保存分页数据集的泛型IList对象</param>
        public PagedResult(Int32 totalCount, Int32 totalPages, Int32 pageIndex, Int32 pageSize, IList<T> results)
        {
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Results = results;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置所有信息条数
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        /// 获取或设置所有页数
        /// </summary>
        [DataMember]
        public Int32 TotalPages { get; set; }

        /// <summary>
        /// 获取或设置页码，页码不小于1
        /// </summary>
        [DataMember]
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数
        /// </summary>
        [DataMember]
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 获取或设置保存分页数据集的泛型<see cref="System.Collections.IList"/>对象
        /// </summary>
        [DataMember]
        public IList<T> Results { get; set; }

        #endregion

    }
}