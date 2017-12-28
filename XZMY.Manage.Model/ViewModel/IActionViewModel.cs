using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel
{
    public interface IActionViewModel<T> : IActionViewModel2C<T>, IActionViewModel2M<T> where T : EntityBase, IDataModel
    {
    }

    public interface IActionViewModel2C<T> : IViewModel<T> where T : EntityBase, IDataModel
    {
        
        Guid DataId { get; set; }
        T CreateNewDataModel();
    }
    public interface IActionViewModel2M<T> : IViewModel<T> where T : EntityBase, IDataModel
    {
        Guid DataId { get; set; }

        T MergeDataModel(T model);
    }

    public interface IViewModel<T> where T : IDataModel
    {
    }

    public static class IViewModelExtension
    {
        public static TV CreateViewModel<T, TV>(this T model) where T : IDataModel where TV : IViewModel<T>, new()
        {
            try
            {
                var res = new TV();
                var resType = typeof(TV);
                var properties = GetPropertyInfos(resType);
                var modeltypePs = GetPropertyInfos(typeof(T));


                foreach (var property in properties)
                {

                    if (!property.CanWrite || !property.CanRead ||// 排除忽略字段
                        !property.PropertyType.IsValueType && property.PropertyType != typeof(string)
                        )
                        continue;

                    var mp = modeltypePs.FirstOrDefault(m => m.Name == property.Name);
                    if (mp == null) continue;

                    try
                    {
                        if (mp.PropertyType == property.PropertyType)
                            property.SetValue(res, mp.GetValue(model));
                        else
                            property.SetValue(res, Convert.ChangeType(mp.GetValue(model),
                                property.PropertyType));
                    }
                    catch
                    {
                        continue;
                    }
                }

                return res;
            }
            catch
            {
                return default(TV);
            }
        }
        private static Dictionary<Type, PropertyInfo[]> PropertyInfoCache = new Dictionary<Type, PropertyInfo[]>();
        /// <summary>
        /// 
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
