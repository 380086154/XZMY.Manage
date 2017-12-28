using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetEntityByIdList<T> : MappingQuery<T>, IGetEntityByIdList<T> where T : class,IDataModel, IEntity<Guid>, new()
    {


        public GetEntityByIdList() { }
        public GetEntityByIdList(string tableName) { TableName = tableName; }
        public IList<Guid> IdList { get; set; }

        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<T> Execute()
        {
            if (IdList.Count == 0) return new List<T>();

            var condition = GetCondition();

            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT_WITH_CLAUSE,
                    String.Join(",", GetMappedProperties()), TableName, condition);

            var res = new List<T>();
            using (var reader = SqlServerHelper.ExecuteReader(SqlServerHelper.CONNECTION_STRING, CommandType.Text, sqlStatement))
            {
                while (reader.Read())
                    res.Add(reader.ToModel<T>());

                return res;
            }
        }

        private String GetCondition()
        {
            return String.Format("[DataId] in ({0})", String.Join(",", IdList.Select(m => "'" + m + "'")));
        }
    }
}
