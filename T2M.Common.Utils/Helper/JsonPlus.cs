using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace T2M.Common.Utils.Helper
{
    /// <summary>
    /// 提供对象到Json序列化和反序列化功能的工具类。
    /// </summary>
    public static class JsonPlus
    {
        #region Public Method

        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(this T t)
        {
            var jsonString = string.Empty;
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }

            // 替换Json的Date字符串
            var p = @"\\/Date\((\d+)\+\d+\)\\/";
            var matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            var reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);

            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(this string jsonString)
        {
            // 将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            var p = @"\d{4}-\d{2}-\d{2}(\s\d{2}:\d{2}:\d{2})?";
            var matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            var reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            var ser = new DataContractJsonSerializer(typeof(T));

            T entity = default(T);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                entity = (T)ser.ReadObject(ms);
            }

            return entity;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            var result = string.Empty;
            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            var result = string.Empty;
            var dt = (DateTime)m.Groups[0].Value.ToDateTime();
            dt = dt.ToUniversalTime();
            var ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;
        }

        #endregion
    }
}
