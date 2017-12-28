using System;
using System.Collections.Generic;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetEntityByIdListService<T> : IInvokeService<IList<Guid>, IList<T>> where T : class, IDataModel, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public GetEntityByIdListService(IList<Guid> vm, string tablename = null)
        {
            ViewModel = vm;
            _tableName = tablename;
        }
        private String _tableName;

        /// <summary>
        /// 
        /// </summary>
        public IList<Guid> ViewModel { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<T> Invoke()
        {

            var query = new GetEntityByIdList<T>(_tableName);
            query.IdList = ViewModel;

            return query.Execute();
        }
        
    }
}