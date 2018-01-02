using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    /// <summary>
    /// 分页相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PaginationQuery<T> : MappingQuery<T>, IPaginationQuery<T> where T : class, IDataModel, IEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        protected PaginationQuery()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Expression<Func<T, object>>[] SortMember { get; set; }

        /// <summary>
        /// 排序方式：正序/倒序
        /// </summary>
        public SortType SortType { get; set; }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <returns></returns>
        public abstract PagedResult<T> Execute();

        /// <summary>
        /// 获取排序方式
        /// </summary>
        /// <returns></returns>
        protected String GetSortTypeString()
        {
            if (SortMember == null)
            {
                SortMember = new Expression<Func<T, object>>[] { x => x.CreatedTime };
            }

            return string.Join(",", SortMember.Select(m =>
            {
                var member = m.GetExpressionMemberName();
                return member + " " + SortType;
            }));

        }

        /// <summary>
        /// 实例化分页信息
        /// </summary>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        protected PagedResult<T> InitPagedResult(Int32 totalCount)
        {
            var pagedResult = new PagedResult<T>
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                Results = new List<T>(totalCount > PageSize ? PageSize : totalCount),
                //Results = new List<T>(),
                TotalCount = totalCount,
                //TotalPages = totalCount / PageSize + 1
            };

            if (totalCount > 0 && PageSize > 0)
            {
                pagedResult.TotalPages = totalCount / PageSize + 1;
            }
            else pagedResult.TotalPages = 0;

            return pagedResult;

            //return new PagedResult<T>
            // {
            //     PageIndex = PageIndex,
            //     PageSize = PageSize,
            //     Results = new List<T>(totalCount > PageSize ? PageSize : totalCount),
            //     TotalCount = totalCount,
            //     //TotalPages = totalCount / PageSize + 1
            // };
        }
    }
}
