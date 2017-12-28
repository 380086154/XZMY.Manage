using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace T2M.Common.Utils.ADONET.SQLServer
{
    /// <summary>
    /// 实体转换
    /// </summary>
    public static class ModelBuilder
    {
        //实体属性缓存
        private static Dictionary<Type, PropertyInfo[]> PropertyInfoCache = new Dictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Build a business model instance from <see cref="DataRow"/>.
        /// </summary>
        /// <typeparam name="T">The type of business model</typeparam>
        /// <param name="row">Represents a row of data that read from database</param>
        /// <returns>Instance of business model</returns>
        public static T ToModel<T>(this DataRow row) where T : class,new()
        {
            try
            {
                var model = new T();
                var modelType = typeof(T);
                var properties = GetPropertyInfos(modelType);

                foreach (var property in properties)
                {
                    if (!property.CanWrite) continue;
                    if (!property.PropertyType.IsValueType && property.PropertyType != typeof(String))
                        continue;
                    try
                    {
                        if (property.PropertyType.IsEnum)
                            property.SetValue(model, Int32.Parse(row[property.Name].ToString()));
                        else
                            property.SetValue(model, Convert.ChangeType(row[property.Name],
                                property.PropertyType));
                    }
                    catch
                    {
                        continue;
                    }
                }
                return model;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Build a model instance from <see cref="SqlDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The type of business model</typeparam>
        /// <param name="reader">Represents rows of data that read from database</param>
        /// <returns>Instance of business model</returns>
        public static T ToModel<T>(this SqlDataReader reader) where T :class, IDataModel, new()
        {
            try
            {
                var model = new T();
                var modelType = typeof(T);
                var properties = GetPropertyInfos(modelType);

                foreach (var property in properties)
                {
                  
                    if (!property.CanWrite || !property.CanRead ||// 排除忽略字段
                        !property.PropertyType.IsValueType && property.PropertyType != typeof(String)
                        )
                        continue;

                    try
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            property.SetValue(model, Int32.Parse(reader[property.Name].ToString()));
                            continue;
                        }
                        if (property.PropertyType == typeof(Guid))
                        {
                            property.SetValue(model, Guid.Parse(reader[property.Name].ToString()));
                            continue;
                        }
                        property.SetValue(model, Convert.ChangeType(reader[property.Name],
                            property.PropertyType));
                    }
                    catch
                    {
                        continue;
                    }
                }

                return model;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 实体属性缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetPropertyInfos(Type type)
        {
            if (type == null) return new PropertyInfo[0];
            if (!PropertyInfoCache.ContainsKey(type))
            {
                lock (PropertyInfoCache)
                {
                    if (!PropertyInfoCache.ContainsKey(type))
                    {
                        PropertyInfoCache[type] = type.GetProperties();
                    }
                }
            }
            return PropertyInfoCache[type];
        }
    }
}
