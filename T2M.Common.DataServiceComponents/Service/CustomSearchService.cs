using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    public class CustomSearchService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public CustomSearchService(string tablename = null)
        {
            _tableName = tablename;
        }
        private String _tableName;


        public IList<CustomCondition<T>> CustomConditions { get; set; }

        public IList<T> Invoke()
        {
            var query = new CustomSearch<T>(_tableName);
            query.CustomConditions = CustomConditions;
            return query.Execute();
        }
    }


    /// <summary>
    /// 获取集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomSearchWithPaginationService<T> : IInvokeService<PagedResult<T>> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public CustomSearchWithPaginationService(string tablename = null)
        {
            _tableName = tablename;
        }
        private String _tableName;
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数。
        /// </summary>
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Expression<Func<T, object>>[] SortMember { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortType SortType { get; set; }

        /// <summary>
        /// 自定义条件
        /// </summary>
        public IList<CustomCondition<T>> CustomConditions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual PagedResult<T> Invoke()
        {
            var query = new CustomSearchWithPagination<T>(_tableName)
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                SortMember = SortMember,
                SortType = SortType,
                CustomConditions = CustomConditions
            };

            return query.Execute();
        }
    }

}
