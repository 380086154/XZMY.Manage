using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Log;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Utils.Extendsions;

namespace XZMY.Manage.Service.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public static void Log(string title, string message, LogLevel level)
        {
            var log = new LogEntity()
            {
                DataId = Guid.NewGuid(),
                Title = title,
                Message = message,
                Level = level,
                IP = LoggedUserManager.GetCurrentUserAccount().IP,
                UserId = LoggedUserManager.GetCurrentUserAccount().AccountId.ToString(),
                UserName = LoggedUserManager.GetCurrentUserAccount().Name,
            };
            log.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

            var logger = new SqlServerLogger();
            logger.AddLog(log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="ex"></param>
        public static void LogException(string title, string message, LogLevel level, Exception ex)
        {
            var log = new LogEntity()
            {
                DataId = Guid.NewGuid(),
                Title = title,
                Message = message,
                Level = level,
                IP = LoggedUserManager.GetCurrentUserAccount().IP,
                UserId = LoggedUserManager.GetCurrentUserAccount().AccountId.ToString(),
                UserName = LoggedUserManager.GetCurrentUserAccount().Name,
                Exception = ex.FormatMessage()
            };
            log.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

            var logger = new SqlServerLogger();
            logger.AddLog(log);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Commit()
        {
            var logger = new SqlServerLogger();
            logger.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Clear()
        {
            var logger = new SqlServerLogger();
            logger.Clear();
        }
    }
}
