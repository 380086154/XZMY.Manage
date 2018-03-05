using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.WindowsService.Utility;

namespace System
{
    public static class ConvertUtility
    {
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

        #region ToInt64

        /// <summary>
        /// 将 String 转换为 Int64
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int64? ToInt64(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Int64 obj;
            if (Int64.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Int64，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this String str, Int64 defaultValue)
        {
            var res = str.ToInt64();
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
        public static string ToStringFormat(this string date, string format = "yyyy-MM-dd")
        {
            return date.ToDateTime(DateTimePlus.GetMinDateTime).ToString(format);
        }

        /// <summary>
        /// 将 DataTime 转为 int
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static int ToInt(this DateTime date)
        {
            var dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(date - dateTime).TotalSeconds;
        }

        /// <summary>
        /// 将 int 转给 DateTime
        /// </summary>
        /// <param name="totalSeconds">时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int seconds)
        {
            var date = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return date.AddSeconds(seconds);
        }

        #endregion

        #region ToDataTable

        /// <summary>
        /// 将 List 集合转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list) where T : new()
        {
            if (list == null) throw new ArgumentNullException("ConvertUtils.ToDataTable error：list can not be null");
            var plist = new List<PropertyInfo>();
            var type = typeof(T);
            var dt = new DataTable();
            //将所有的public属性加入到集合并添加到DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                if (!dt.Columns.Contains(p.Name))
                {
                    plist.Add(p);
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
            });

            foreach (var entity in list)
            {
                //创建一个dataRow实例
                DataRow row = dt.NewRow();

                //给row赋值
                plist.ForEach(p => row[p.Name] = CheckValue(p, p.GetValue(entity, null)));
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list, String[] properties) where T : new()
        {
            if (list == null) throw new ArgumentNullException("ConvertUtils.ToDataTable error：list can not be null");
            var plist = new List<PropertyInfo>();
            var type = typeof(T);
            var dt = new DataTable("Table");
            //将所有的public属性加入到集合并添加到DataTable的列
            Array.ForEach(type.GetProperties(), p =>
            {
                if (!dt.Columns.Contains(p.Name) &&
                    properties.Contains(String.Format("[{0}]", p.Name)))
                {
                    plist.Add(p);
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
            });

            foreach (var entity in list)
            {
                //创建一个dataRow实例
                DataRow row = dt.NewRow();

                //给row赋值
                //plist.ForEach(p => row[p.Name] = CheckValue(p, p.GetValue(entity, null)));

                foreach (var p in plist)
                {
                    if (properties.Contains(String.Format("[{0}]", p.Name)))
                        row[p.Name] = CheckValue(p, p.GetValue(entity, null));
                }

                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 将匿名类 集合转换为DataTable
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static DataTable ToDateTable(this object[] datas)
        {
            //创建表
            var dt = new DataTable();
            //创建列
            if (datas.Length > 0)
            {
                //获取集合中第一个元素的类型
                Type t = datas[0].GetType();
                //通过反射获取该类型的所有属性集合
                PropertyInfo[] infos = t.GetProperties();
                //遍历属性集合来创建数据列
                foreach (PropertyInfo pro in infos)
                {
                    var dc = new DataColumn(pro.Name, GetNotNullType(pro.PropertyType));
                    dt.Columns.Add(dc);
                }
                //根据集合中所有对象来添加数据行
                for (int i = 0; i < datas.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    //遍历对象的所有属性
                    foreach (PropertyInfo pro in infos)
                    {
                        //获取data[i]这个对象的pro属性的值
                        dr[pro.Name] = pro.GetValue(datas[i], null).ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        ///<summary>
        ///将可空类型转换为对应的不可空类型。
        ///</summary>
        ///<param name="t"></param>
        ///<returns></returns>
        private static Type GetNotNullType(Type t)
        {
            //如果是值类型，才需要转换
            if (t.IsValueType)
            {
                if (t == typeof(int?)) return typeof(int);
                if (t == typeof(DateTime?)) return typeof(DateTime);
                if (t == typeof(long?)) return typeof(long);
                if (t == typeof(Int64?)) return typeof(Int64);
                if (t == typeof(decimal?)) return typeof(decimal);
                if (t == typeof(double?)) return typeof(double);

                //short ,long ,decimal,double都要转换处理
            }
            return t;
        }

        /// <summary>
        /// 检查空属性并赋初值
        /// </summary>
        /// <param name="info">属性信息</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        private static object CheckValue(PropertyInfo info, object value)
        {
            var type = info.PropertyType.Name;

            if ((info.PropertyType.BaseType == typeof(Enum) ||
                type == "Int16" || type == "Int32" || type == "Int64" || type == "Decimal") &&
                value == null)
            {
                value = 0;
            }
            else if (type == "String" && value == null)
            {
                value = string.Empty;
            }
            else if (type == "DateTime" && (value.ToString().ToDateTime() < DateTimePlus.GetMinDateTime || value.Equals(null)))
            {
                value = DateTimePlus.GetMinDateTime;
            }
            else if (type == "Guid")
            {
                value = value.ToString().ToGuid(Guid.Empty);
            }

            return value;
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

        #region ToEntity

        public static T ToEntity<T>(this DataRow dr) where T : class,new()
        {
            if (dr == null) return default(T);

            var entity = new T();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var obj = dr[propertyInfo.Name];

                if (obj != null)
                {
                    var value = CheckValue(propertyInfo, obj);
                    entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, value, null);
                }
            }

            return entity;
        }

        #endregion
    }
}
