using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XZMY.Manage.Log.Models;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlServerLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public void AddLog(LogEntity log)
        {
            if (HttpContext.Current == null) return;
            var cache = HttpContext.Current.Items;
            lock (cache)
            {
                if (!cache.Contains("LOGITEMS") || cache["LOGITEMS"].As<List<LogEntity>>() == null) cache["LOGITEMS"] = new List<LogEntity>();
                var logcache = cache["LOGITEMS"].As<List<LogEntity>>();
                logcache.Add(log);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            if (HttpContext.Current == null) return;
            var cache = HttpContext.Current.Items;
            lock (cache)
            {
                if (!cache.Contains("LOGITEMS") || cache["LOGITEMS"].As<List<LogEntity>>() == null) cache["LOGITEMS"] = new List<LogEntity>();
                var logcache = cache["LOGITEMS"].As<List<LogEntity>>();
                if (logcache == null || logcache.Count == 0) return;
                using (var wrapper = new SqlTransactionWrapper())
                {
                    foreach (var log in logcache)
                    {
                        var service = new BaseCreateService<LogEntity>(log);
                        service.Invoke(wrapper.Transaction);
                    }
                }
                Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            if (HttpContext.Current == null) return;
            var cache = HttpContext.Current.Items;
            cache["LOGITEMS"] = new List<LogEntity>();
        }
    }
}
