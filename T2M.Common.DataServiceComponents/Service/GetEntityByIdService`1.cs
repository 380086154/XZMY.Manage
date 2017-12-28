using System;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    /// <summary>
    /// Id查询基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetEntityByIdService<T> : IInvokeService<Guid, T> where T : class, IDataModel, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public GetEntityByIdService(Guid vm, string tablename = null)
        {
            ViewModel = vm;
            _tableName = tablename;
        }


        private String _tableName;
        /// <summary>
        /// 
        /// </summary>
        public Guid ViewModel { get; set; }

        public bool UseCache { get; set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public T Invoke()
        {
            var query = new GetEntityById<T>(_tableName);
            query.Id = ViewModel;
            var res = query.Execute();

            return res;
        }
    }

}
