using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogEntity
    {
        public LogEntity()
        {
        }

        public LogEntity(string title, string message, string exception, LogLevel level, string ip, string userName)
        {
            DataId = Guid.NewGuid();
            Title = title;
            Message = message;
            Exception = exception;
            Level = level;
            IP = ip;
            UserId = string.Empty;
            UserName = userName;
            CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public Guid DataId { get; set; }
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
        public string CreatedTime { get; set; }
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
