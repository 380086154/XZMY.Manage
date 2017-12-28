using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Query.Module;
using T2M.Common.Utils.ADONET.SQLServer;

namespace XZMY.Manage.Data.Impl.Query.Module
{
    public class ModifyActionOnModuleModified : IModifyActionOnModuleModified
    {
        private const string QueryTemplate1 = @"UPDATE {0} SET ModuleCode=@ModuleCode WHERE ModuleId=@ModuleId";
        public Guid ModuleId
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string TableName
        {
            get; set;
        }

        public int Execute(IDbTransaction transaction)
        {
            var cmd1 = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text,
                String.Format(QueryTemplate1, TableName), GetSqlParameters());
            throw new NotImplementedException();
        }

        private SqlParameter[] GetSqlParameters()
        {
            return new[]
              {
                new SqlParameter("ModuleId", ModuleId)
                {
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter("ModuleCode", Code)
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                }
            };
        }
    }
}
