using System;
using System.Linq.Expressions;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 提供了根据数据库查询结果来计算分页的业务对象接口。
    /// </summary>
    public interface IPaginationQuery<T> : IQuery<PagedResult<T>>
    {
        /// <summary>
        /// 获取或设置页码，页码不小于1。
        /// </summary>
        Int32 PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数。
        /// </summary>
        Int32 PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        Expression<Func<T, object>>[] SortMember { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        SortType SortType { get; set; }
    }
}
