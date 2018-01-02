using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 日志 服务
    /// </summary>
    public class LogService
    {
        public DatabaseHelper db = null;
        public Guid BranchNameDataId = Guid.Empty;
        public string IP = string.Empty;
        public string UserId = string.Empty;
        public string UserName = string.Empty;

        public LogService(DatabaseHelper databaseHelper, string ip, Guid userId, string userName)
        {
            db = databaseHelper;
            IP = ip;
            UserId = userId.ToString();
            UserName = userName;
        }

        public LogService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public LogService(DatabaseHelper databaseHelper, Guid branchNameDataId)
        {
            db = databaseHelper;
            BranchNameDataId = branchNameDataId;
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="entity"></param>
        public void Add(LogEntity entity)
        {
            var sql = @"INSERT INTO [SYS_LOG]
           ([DataId],[Title],[Message],[Exception],[Level],[IP],[UserId],[UserName],[CreatedTime])
     VALUES
           (NEWID(),@Title,@Message,@Exception,@Level,@IP,@UserId,@UserName,GETDATE())";

            var sp = new SqlParameter[] {
                new SqlParameter("@Title", entity.Title),//姓名
                new SqlParameter("@Message", entity.Message),//卡类型
                new SqlParameter("@Exception", entity.Exception),//卡类型名称
                new SqlParameter("@Level", entity.Level),//折扣（卡名称）
                new SqlParameter("@IP", entity.IP),//积分
                new SqlParameter("@UserId",entity.UserId),//当前状态
                new SqlParameter("@UserName", entity.UserName),//性别
            };

            db.ExecuteNonQuery(sql, EProviderName.SqlClient, sp);
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="level"></param>
        public void Add(string title, string message, string exception, LogLevel level)
        {
            Add(new LogEntity
            {
                Title = title,
                Message =message,
                Exception =exception,
                Level =level,
                IP = IP,
                UserId = UserId,
                UserName = UserName
            });
        }
    }
}
