using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2M.Common.Utils.Extension
{
    public static class Json
    {
        public static object ToJson(this string json)
        {
            return ToJson<object>(json);
        }

        public static string ToJson(this object obj)
        {
            return ToJson(obj, "yyyy-MM-dd HH:mm:ss");
        }

        public static string ToJson(this object obj, string dateTimeFormat)
        {
            var timeConverter = new IsoDateTimeConverter() { DateTimeFormat = dateTimeFormat };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        public static T ToJson<T>(this string json)
        {
            return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public static List<T> ToList<T>(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static DataTable ToTable(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }
        public static JObject ToJObject(this string json)
        {
            return json == null ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }

        public static T ToObject<T>(this string json)
        {
            return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
