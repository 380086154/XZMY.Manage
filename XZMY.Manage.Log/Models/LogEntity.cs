using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Log.Models
{
    [Serializable]
    [DBTable("SYS_LOG")]
    public class LogEntity : EntityBase, IDataModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; }
        public String LevelName
        {
            get
            {
                switch ((int)Level)
                {
                    case 1: return "调试记录";
                    case 2: return "普通记录";
                    case 3: return "警告记录";
                    case 4: return "错误记录";
                    case 5: return "致命错误";
                    default: return "未定义等级";
                }
            }
        }
        public string IP { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public enum LogLevel
    {
        Debug = 1,
        Normal = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5,
        Undefined = 6,
    }
}
