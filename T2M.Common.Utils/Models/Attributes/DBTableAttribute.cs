using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2M.CoastLine.Utils.Model.Attributes
{
    /// <summary>
    /// 标记一个表名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DBTableAttribute : Attribute
    {
        /// <summary>
        /// 设置数据库表名
        /// </summary>
        /// <param name="tableName">Name of the Table</param>
        public DBTableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; set; }

        public override string ToString()
        {
            return "DBTable";
        }
    }
}
