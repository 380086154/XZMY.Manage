using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.Helper;

namespace T2M.Common.Utils.ADONET.SQLServer
{
    /// <summary>
    /// Provide a OOP based methods for Transact-SQL statement creation.
    /// </summary>
    /// <typeparam name="T">The type of business model.</typeparam>
    public abstract class MappingQuery<T> where T : class, IDataModel
    {
        #region Properties

        /// <summary>
        /// 表名缓存
        /// </summary>
        private static readonly Dictionary<Type, String> TableNameCache = new Dictionary<Type, String>(100);

        /// <summary>
        /// Collection of mapped properties. To avoid conflict, 
        /// we use BOTH business model instance type and query class instance type to locate property mapping expression and value.
        /// </summary>
        protected readonly static Dictionary<Tuple<Type, Type>, Dictionary<Expression<Func<T, Object>>, Tuple<MappedFieldAssignmentOption, Object>>>
            MappedPropertyCollection = new Dictionary<Tuple<Type, Type>, Dictionary<Expression<Func<T, Object>>, Tuple<MappedFieldAssignmentOption, Object>>>(1000);

        /// <summary>
        /// Collection of mapped properties name.
        /// </summary>
        protected readonly static Dictionary<Tuple<Type, Type>, String[]> MappedPropertyNameCollection = new Dictionary<Tuple<Type, Type>, String[]>(1000);

        private String _tableName = string.Empty;
        /// <summary>
        /// 当实体名称与数据库表名一致时作为表名使用，若不一致时外部单独处理。
        /// </summary>
        public virtual String TableName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_tableName)) return _tableName;

                if (EntityType == null) throw new ArgumentNullException();


                if (TableNameCache.ContainsKey(EntityType)) return TableNameCache[EntityType];

                var tableattr = EntityType.GetCustomAttribute<DBTableAttribute>();
                if (tableattr != null)
                    TableNameCache[EntityType] = tableattr.TableName;
                else
                    TableNameCache[EntityType] = EntityType.Name;

                return TableNameCache[EntityType];
            }
            set { _tableName = value; }
        }


        /// <summary>
        /// Instance type of business model whose properties are to be mapped.
        /// </summary>
        protected Type EntityType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// Instance type of query build class.
        /// </summary>
        protected Type QueryType
        {
            get { return GetType(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the 
        /// <see>
        ///     <cref>MappingQuery</cref>
        /// </see>
        /// class.
        /// </summary>
        protected MappingQuery()
        {
            if (!IsMappedQuery())
            {
                BuildMapping();
            }
        }

        #endregion

        /// <summary>
        /// Build mappings for query that represent a specific business scenario.
        /// </summary>
        protected abstract void BuildMapping();

        #region Entity properties mapping methods

        /// <summary>
        /// Map a property as database primary key.
        /// </summary>
        /// <param name="mappingExpression">Mapping expression of property.</param>
        protected void Id(Expression<Func<T, Object>> mappingExpression)
        {
            Map(mappingExpression);
        }

        /// <summary>
        /// Map a property as database field.
        /// </summary>
        /// <param name="mappingExpression">The mapping expression of property.</param>
        /// <param name="option">The option indicates how the value of mapped property is to be assigned.</param>
        /// <param name="val">The value of the mapped property.</param>
        protected void Map(Expression<Func<T, object>> mappingExpression, MappedFieldAssignmentOption option = MappedFieldAssignmentOption.Runtime, Object val = null)
        {
            var key = new Tuple<Type, Type>(EntityType, QueryType);

            if (!MappedPropertyCollection.ContainsKey(key))
            {
                lock (MappedPropertyCollection)
                {
                    if (!MappedPropertyCollection.ContainsKey(key))
                    {
                        MappedPropertyCollection.Add(key, new Dictionary<Expression<Func<T, Object>>, Tuple<MappedFieldAssignmentOption, Object>>(new ExpressionComparer()));
                    }
                }
            }

            lock (MappedPropertyCollection[key])
            {
                MappedPropertyCollection[key][mappingExpression] = new Tuple<MappedFieldAssignmentOption, Object>(option, val);
            }
        }

        protected void Unmap(Expression<Func<T, object>> mappingExpression)
        {
            var key = new Tuple<Type, Type>(EntityType, QueryType);

            if (!MappedPropertyCollection.ContainsKey(key))
            {
                return;
            }

            lock (MappedPropertyCollection[key])
            {
                MappedPropertyCollection[key].Remove(mappingExpression);
            }
        }
        /// <summary>
        /// Map all properties.
        /// </summary>
        protected void MapAll()
        {
            //var key = new Tuple<Type, Type>(EntityType, QueryType);

            //if (MappedPropertyCollection.ContainsKey(key)) //缓存
            //{

            //    Map(MappedPropertyCollection[key].ToList());
            //}

            //else
            //{
            var properties = EntityType.GetProperties();

            foreach (var property in properties)
            {

                if (!property.CanWrite || !property.CanRead)
                    continue;

                var propType = property.PropertyType;

                if (propType.IsPrimitive || propType.IsValueType || propType == typeof(String))
                {
                    var ex = CreateMappingExpression(property);
                    Map(ex);
                }
            }
            //}
        }

        #endregion

        /// <summary>
        /// Get name collection of mapped collection.
        /// </summary>
        /// <returns>Property name collection.</returns>
        protected String[] GetMappedProperties()
        {
            lock (MappedPropertyNameCollection)
            {
                //var mark = !String.IsNullOrWhiteSpace(tableName)
                //    ? String.Format("[{0}].", tableName)
                //    : "";

                var key = new Tuple<Type, Type>(EntityType, QueryType);

                if (MappedPropertyNameCollection.ContainsKey(key))
                {
                    var property = MappedPropertyNameCollection[key].ToArray();
                    return property;
                }

                var members = MappedPropertyCollection[key];
                var names = members.Select(m => m.Key.GetExpressionMemberName());
                var res = MappedPropertyNameCollection[key] = names.Distinct().ToArray();

                return res;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected String[] GetParamterPlaceholders()
        {
            lock (MappedPropertyCollection)
            {
                var key = new Tuple<Type, Type>(EntityType, QueryType);

                var names = MappedPropertyCollection[key].Select(m => "@" + m.Key.GetExpressionMemberName()).ToArray();

                return names;
            }
        }

        /// <summary>
        /// Get value collection of mapped collection.
        /// </summary>
        /// <param name="model">A instance of business model whose properties are to be mapped.</param>
        /// <returns>A read only value collection.</returns>
        protected Dictionary<Expression<Func<T, Object>>, Object> GetMappedPropertyValues(T model)
        {
            lock (MappedPropertyCollection)
            {
                var key = new Tuple<Type, Type>(EntityType, QueryType);

                var members = MappedPropertyCollection[key].ToList();
                var mappedPropertyValueCollection = new Dictionary<Expression<Func<T, Object>>, Object>(new ExpressionComparer());
                members.ForEach(m =>
                    {
                        Object val;

                        switch (m.Value.Item1)
                        {
                            case MappedFieldAssignmentOption.DefaultValue:
                                val = m.Value.Item2;
                                break;
                            default:
                                val = m.Key.Compile().Invoke(model);
                                break;
                        }

                        if (mappedPropertyValueCollection.ContainsKey(m.Key))
                        {
                            throw new ArgumentException("Duplicated Mapped Property: " + m.Key);
                        }

                        var value = GetDatabaseValue(val);
                        if (value == null && m.Key.GetExpressionMemberType() == typeof(string)) value = string.Empty;
                        if (m.Key.GetExpressionMemberType() == typeof(DateTime) && value.Equals(default(DateTime))) value = DateTimePlus.GetMinDateTime;
                        mappedPropertyValueCollection.Add(m.Key, value);
                    });

                return new Dictionary<Expression<Func<T, Object>>, Object>(mappedPropertyValueCollection);
            }
        }

        /// <summary>
        /// 获取模型中对应参数的数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected SqlParameter[] GetModelParameters(T model)
        {
            var key = new Tuple<Type, Type>(EntityType, QueryType);

            var keys = MappedPropertyCollection[key].ToList();
            var values = GetMappedPropertyValues(model).Values.ToArray();

            var sqlParameter = new SqlParameter[keys.Count];
            for (var i = 0; i < values.Length; i++)
            {
                var m = keys[i];
                var v = values[i];
                var k = m.Key.GetExpressionMemberName();

                var dbType = GetSqlDbType(v);
                if (dbType == SqlDbType.DateTime && (DateTime)v <= DateTimePlus.GetMinDateTime)
                    v = DateTimePlus.GetMinDateTime;

                sqlParameter[i] = SqlServerHelper.BuildInParameter("@" + k, dbType, v);
            }

            return sqlParameter;
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected SqlDbType GetSqlDbType(Object value)
        {
            if (value is DateTime) return SqlDbType.DateTime;

            if (value is Guid) return SqlDbType.UniqueIdentifier;

            if (value is float || value is double || value is decimal)
                return SqlDbType.Decimal;
            if (value is Int16)
                return SqlDbType.SmallInt;
            if (value is Int64 || value is UInt64 || value is UInt32)
                return SqlDbType.BigInt;

            if (value is Int32 || value is UInt16 || (value != null && value.GetType().IsEnum))
                return SqlDbType.Int;

            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 获取更新 SQL 列
        /// </summary>
        /// <returns></returns>
        protected string GetUpdateStringTemplate()
        {

            var properties = GetMappedProperties();
            var paramsValue = GetParamterPlaceholders();

            return string.Join(",", properties.Select((k, i) =>
            {
                if (k.Equals("DataId", StringComparison.CurrentCultureIgnoreCase)) return null;
                var v = paramsValue[i];
                return string.Format("{0}={1}", k, v);
            }).Where(m => m != null));
        }

        #region Private methods

        /// <summary>
        /// Check whether the business model is mapped.
        /// </summary>
        /// <returns>A boolean value indicates whether the business model is mapped.</returns>
        private Boolean IsMappedQuery()
        {
            lock (MappedPropertyCollection)
            {
                return MappedPropertyCollection.ContainsKey(new Tuple<Type, Type>(EntityType, QueryType));
            }
        }

        /// <summary>
        /// Create property mapping expression automatically.
        /// </summary>
        /// <param name="property">A <see cref="PropertyInfo"/> object for mapping.</param>
        /// <returns>A lambda expression which represents property mapping.</returns>
        private Expression<Func<T, Object>> CreateMappingExpression(PropertyInfo property)
        {
            var paramExpression = Expression.Parameter(EntityType, typeof(Object).Name);
            var memberExpression = Expression.Property(paramExpression, property);
            var objectExpression = Expression.Convert(memberExpression, typeof(Object));
            var lambdaExpression = Expression.Lambda<Func<T, Object>>(objectExpression, paramExpression);

            return lambdaExpression;
        }

        /// <summary>
        /// Convert property value for database, e.g. enum(Except System.ValueType), reference type.
        /// </summary>
        /// <param name="val">The value of property.</param>
        /// <returns>SQL Server-specific value.</returns>
        protected object GetDatabaseValue(Object val)
        {
            if (val == null)
                return null;

            if (val.GetType().IsEnum)
                return (Int32)val;

            /*
            if (value.GetType().IsClass)
            {
            }
            */

            return val;
        }

        #endregion

        /// <summary>
        /// Defines methods to support the comparison of objects for equality.
        /// </summary>
        public class ExpressionComparer : EqualityComparer<Expression<Func<T, object>>>
        {
            public override Boolean Equals(Expression<Func<T, object>> x, Expression<Func<T, object>> y)
            {
                if (x == null || y == null)
                    return x == null && y == null;

                var s1 = GetExpressionMemberName(x);
                var s2 = GetExpressionMemberName(y);

                if (s1 == null || s2 == null) return false;

                return s1 == s2;
            }

            public override Int32 GetHashCode(Expression<Func<T, object>> obj)
            {
                if (obj == null) return -1;
                var name = GetExpressionMemberName(obj);
                if (name != null) return name.GetHashCode();
                return -1;
            }
            private String GetExpressionMemberName(Expression<Func<T, object>> m)
            {
                if (m.Body is MemberExpression)
                    return ((MemberExpression)m.Body).Member.Name;

                if (m.Body is UnaryExpression)
                    return ((MemberExpression)((UnaryExpression)m.Body).Operand).Member.Name;
                return null;
            }

        }
    }

    /// <summary>
    /// Represent the strategy of the way that get value from mapped property. 
    /// </summary>
    public enum MappedFieldAssignmentOption
    {
        /// <summary>
        /// A value of the mapped property, which is assigned at runtime.
        /// </summary>
        Runtime,

        /// <summary>
        /// Default value of the mapped property type.
        /// </summary>
        DefaultValue
    }
}