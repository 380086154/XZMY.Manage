using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    public class ConnectionStringService
    {
        /// <summary>
        /// 连接字符串服务，从服务器获取连接字符串以后有修改就不用更新客户端
        /// </summary>
        public ConnectionStringService()
        {
            Init();
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DatabaseHelper InitDatabaseHelper(string path)
        {
            DatabaseHelper db = null;
            try
            {
                if (string.IsNullOrWhiteSpace(ConnectionString))
                    Init();

                db = new DatabaseHelper(path + "mphygl.mdb", ConnectionString);
            }
            catch (Exception ex)
            {
                Log.Add(ex.StackTrace);
            }
            return db;
        }

        #region Private method

        private void Init()
        {
            var url = "http://www.xzmy.site/api/Sys/GetConnectionString";
            var content = HttpRequestUtil.RequestUrl(url, "GET");
            var str = HttpRequestUtil.GetJsonValue(content, "Value");

            var defaultConnectionString = "Data Source=sds209635357.my3w.com;Initial Catalog=sds209635357_db; Persist Security Info=True;User ID=sds209635357; Password=E17680A936674932B358;MultipleActiveResultSets=true";

            ConnectionString = string.IsNullOrWhiteSpace(str)
                ? defaultConnectionString
                : str;
        }

        #endregion
    }
}
