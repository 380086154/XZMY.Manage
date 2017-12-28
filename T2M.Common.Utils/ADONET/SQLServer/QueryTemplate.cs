using System;
using System.Collections.Generic;
using System.Text;

namespace T2M.Common.Utils.ADONET.SQLServer
{
    /// <summary>
    /// SQL Server 基础语句模版
    /// </summary>
    public class QueryTemplate
    {
        #region basic T-SQL queries

        public const String QUERY_INSERT = "INSERT INTO [{0}] ({1}) VALUES ({2})";
        public const String QUERY_UPDATE = "UPDATE [{0}] SET {1}";
        public const String QUERY_UPDATE_WITH_CLAUSE = "UPDATE [{0}] SET {1} WHERE {2}";
        public const String QUERY_DELETE = "DELETE FROM [{0}]";
        public const String QUERY_DELETE_WITH_CLAUSE = "DELETE FROM [{0}] WHERE {1}";
        public const String QUERY_SELECT = "SELECT {0} FROM [{1}]";
        public const String QUERY_SELECT_WITH_CLAUSE = "SELECT {0} FROM [{1}] WHERE {2}";

        #endregion

        #region Pagination

        public const String QUERY_SELECT_COUNT = "SELECT COUNT(0) FROM [{0}]";
        public const String QUERY_SELECT_COUNT_WITH_CLAUSE = "SELECT COUNT(0) FROM [{0}] WHERE {1}";
        public const String QUERY_PAGINATION =
                                    @"SELECT TOP {0}  *  FROM 
                                            (SELECT ROW_NUMBER() OVER (ORDER BY {4}) AS RowNumber, 
                                                {3}
                                                FROM {2} ) as {2}
                                                WHERE {2}.RowNumber > {1} ORDER BY {2}.RowNumber ASC";
        public const String QUERY_PAGINATION_WITH_CLAUSE =
                                            @"SELECT TOP {0}  *  FROM 
                                            (SELECT ROW_NUMBER() OVER (ORDER BY {4}) AS RowNumber, 
                                                {3}
                                                FROM {2} WHERE {5}) as {2}
                                                WHERE {2}.RowNumber > {1} ORDER BY {2}.RowNumber ASC";

        #endregion

        #region public

        /// <summary>
        /// 时间区间查询 SQL 条件拼接
        /// </summary>
        /// <param name="dateTuple">时间区间</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static string DateRange(Tuple<DateTime?, DateTime?> dateTuple, string columnName)
        {
            var list = new List<string>();

            if (dateTuple.Item1.HasValue && dateTuple.Item2.HasValue)
            {
                if (dateTuple.Item1 > dateTuple.Item2)
                    list.Add(string.Format("[{0}] BETWEEN N'{1}' AND N'{2}'", columnName, dateTuple.Item2.Value.ToString("yyyy-MM-dd 00:00:00"), dateTuple.Item1.Value.AddDays(1)));
                else
                    list.Add(string.Format("[{0}] BETWEEN N'{1}' AND N'{2}'", columnName, dateTuple.Item1, dateTuple.Item2.Value.AddDays(1)));
            }
            else
            {
                if (dateTuple.Item1.HasValue)
                    list.Add(string.Format("[{0}] >= N'{1}'", columnName, dateTuple.Item1.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                if (dateTuple.Item2.HasValue)
                    list.Add(string.Format("[{0}] <= N'{1}'", columnName, dateTuple.Item2.Value.AddDays(1)));
            }
            return list.Count == 0
                ? string.Empty
                : string.Join(" AND ", list);
        }
        
        #endregion

        #region Private Method

        /// <summary>
        /// SQL 拼接专用
        /// </summary>
        /// <param name="list"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Join(string str, List<string> list)
        {
            var sb = new StringBuilder("1 = 1");
            foreach (var item in list)
            {
                if (item.TrimStart().ToUpperInvariant().IndexOf("OR") == 0 ||
                    item.TrimStart().ToUpperInvariant().IndexOf("AND") == 0)
                {
                    sb.Append(item);
                }
                else
                {
                    sb.AppendFormat("{0}{1}", str, item);
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}
