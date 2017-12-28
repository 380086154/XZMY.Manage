using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using T2M.Common.Utils.Helper;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this Object obj) where T : class
        {
            return obj as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Boolean IsIn<T>(this T obj, IEnumerable<T> collection)
        {
            return collection.Contains(obj);
        }

        /// <summary>
        /// 判断该类型是否实现了指定接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tinterface"></param>
        /// <returns></returns>
        public static Boolean IsImplemented(this Type type, Type tinterface)
        {
            if (tinterface.IsInterface)
            {
                return type.GetInterfaces().Contains(tinterface);
            }
            return false;
        }

        /// <summary>
        /// 创建对象的深度克隆，对象类型必须标记为可序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            if (obj == null) return obj;

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        #region ConvertToEnum

        /// <summary>
        /// 尝试把字符串转换成枚举形式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"> </param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(this String str) where T : struct, IConvertible
        {
            T t = default(T);
            return Enum.TryParse(str, true, out t) ? t : default(T);
        }

        #endregion

        #region ToGuid

        /// <summary>
        /// 将 String 转换为 Guid
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param> 
        /// <returns></returns>
        public static Guid? ToGuid(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;

            Guid obj;
            if (Guid.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Guid，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Guid ToGuid(this String str, Guid defaultValue)
        {
            var res = str.ToGuid();
            return res == null ? defaultValue : res.Value;
        }

        #endregion

        #region ToInt32

        /// <summary>
        /// 将 String 转换为 Int32
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32? ToInt32(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Int32 obj;
            if (Int32.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Int32，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this String str, Int32 defaultValue)
        {
            var res = str.ToInt32();
            return res == null ? defaultValue : res.Value;
        }

        #endregion

        #region ToDecimal

        /// <summary>
        /// 将 String 转换为 Decimal
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Decimal obj;
            if (Decimal.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Decimal，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(this String str, Decimal defaultValue)
        {
            var res = str.ToDecimal();
            return res == null ? defaultValue : res.Value;
        }

        #endregion

        #region ToFloat

        /// <summary>
        /// 将 String 转换为 Float
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Single? ToFloat(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Single obj;
            if (Single.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Float，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Single ToFloat(this String str, Single defaultValue)
        {
            var res = str.ToFloat();
            return res == null ? defaultValue : res.Value;
        }

        #endregion

        #region ToDouble

        /// <summary>
        /// 将 String 转换为 Double
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Double? ToDouble(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Double obj;
            if (Double.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Double，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Double ToDouble(this String str, Double defaultValue)
        {
            var res = str.ToDouble();
            return res == null ? defaultValue : res.Value;
        }

        #endregion

        #region ToDateTime

        /// <summary>
        /// 将 String 转换为 DateTime
        /// <para>注：如转换失败则返回 null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            DateTime obj;
            return !DateTime.TryParse(str.Trim(), out obj) ? (DateTime?)null : obj;
        }

        /// <summary>
        /// 将 String 转换为 DateTime，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this String str, DateTime defaultValue)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            DateTime obj;
            return !DateTime.TryParse(str.Trim(), out obj) ? defaultValue : obj;
        }

        /// <summary>
        /// 将 DateTime 转换为指定格式的字符串
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="format">格式，默认为：yyyy-MM-dd</param>
        /// <returns></returns>
        public static string ToStringFormat(this DateTime date, string format = "yyyy-MM-dd")
        {
            return date.ToString(format);
        }

        /// <summary>
        /// 将 String 转换为指定格式的字符串
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="format">格式，默认为：yyyy-MM-dd</param>
        /// <returns></returns>
        public static string ToStringFormat(this String date, string format = "yyyy-MM-dd")
        {
            return date.ToDateTime(DateTimePlus.GetMinDateTime()).ToString(format);
        }

        #endregion

        #region ToBoolean

        /// <summary>
        /// 将 String 转换为 Boolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean? ToBoolean(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Boolean obj;
            if (!Boolean.TryParse(str.Trim(), out obj)) return null;
            return obj;
        }

        #endregion

        #region JsonSerializer

        /// <summary>
        /// 序列化到JSON
        /// </summary>
        public static String ToJsonSerializer<T>(this T item)
        {
            var serializer = new DataContractJsonSerializer(item.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        /// <summary>
        /// 反序列化到实体 注：根据传入的参数如是实体，则返回实体。
        /// </summary>
        public static T ToObject<T>(this String str) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                return serializer.ReadObject(ms) as T;
            }
        }

        #endregion

        #region ConvertTo

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TOut ConvertTo<TOut>(this Object model) where TOut : new()
        {
            if (model == null) return default(TOut);
            var targetType = typeof(TOut);
            var sourceType = model.GetType();

            return (TOut)model.ConvertTo(targetType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targetType"></param>
        /// <param name="deeprank"></param>
        /// <returns></returns>
        private static Object ConvertTo(this Object model, Type targetType, Int32 deeprank = 0)
        {
            if (deeprank > 10 || model == null) return null;

            var sourceType = model.GetType();
            if (sourceType == targetType) return model;
            //if (sourceType.Name.Contains("Proxy")) return model;

            if (sourceType.IsImplemented(typeof(IList)))
            {
                var elementtype = targetType.GenericTypeArguments.FirstOrDefault();
                Object list;
                if (elementtype != null)
                {
                    Type generic = typeof(List<>).MakeGenericType(elementtype);
                    list = Activator.CreateInstance(generic) as IList;
                }
                else list = targetType.Assembly.CreateInstance(targetType.FullName);

                try
                {
                    foreach (var item in (IList)model)
                    {
                        list.As<IList>().Add(item.ConvertTo(elementtype, deeprank + 1));
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return list;
            }

            Object res = targetType.Assembly.CreateInstance(targetType.FullName);

            foreach (var propertity in targetType.GetProperties())
            {
                if (!propertity.CanWrite) continue;

                try
                {
                    var proname = propertity.Name;
                    var sourcePropertity = sourceType.GetProperty(proname);
                    if (sourcePropertity == null) continue;
                    var sourcevalue = sourcePropertity.GetValue(model);
                    if (sourcevalue == null) continue;
                    if (sourcePropertity.PropertyType != propertity.PropertyType)
                    {
                        if (sourcePropertity.PropertyType.IsValueType)
                        {
                            propertity.SetValue(res, sourcevalue);
                            continue;
                        }
                        sourcevalue = sourcevalue.ConvertTo(propertity.PropertyType, deeprank + 1);
                        if (sourcevalue == null) continue;
                    }
                    propertity.SetValue(res, sourcevalue);
                }
                catch (TargetInvocationException ex)
                {
                    return null;
                }
                catch (ArgumentException ex)
                {
                    return null;
                }
            }
            return res;
        }

        /// <summary>
        /// 两个相似实体赋值
        /// </summary>
        /// <typeparam name="S">源实体</typeparam>
        /// <typeparam name="T">赋值实体</typeparam>
        /// <param name="source">源实体值</param>
        /// <returns></returns>
        public static T ConvertTo<S, T>(S source) where T : new()
        {
            if (source == null) return default(T);

            var propertiesT = typeof(S).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var propertiesL = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var setT = new T();
            foreach (PropertyInfo itemL in propertiesL)
            {
                foreach (PropertyInfo itemT in propertiesT)
                {
                    if (itemL.Name != itemT.Name) continue;

                    var value = itemT.GetValue(source, null);
                    itemL.SetValue(setT, value, null);
                    break;
                }
            }
            return setT;
        }

        #endregion

        /// <summary>
        /// 获取类型的默认值，等同于default(type)的返回值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object GetDefaultValue(this Type type)
        {
            return Assembly.GetAssembly(type).CreateInstance(type.FullName);
        }
    }
}
