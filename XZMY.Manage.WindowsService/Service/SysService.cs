using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStringService
    {
        /// <summary>
        /// 连接字符串服务，从服务器获取连接字符串以后有修改就不用更新客户端
        /// </summary>
        /// <param name="havaNetwork">是否有网络，是有，否无</param>
        public ConnectionStringService(bool havaNetwork)
        {
            Init(havaNetwork);
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="path"></param>
        /// <param name="havaNetwork">是否有网络，是无，否有</param>
        /// <returns></returns>
        public DatabaseHelper InitDatabaseHelper(string path, bool havaNetwork = false)
        {
            DatabaseHelper db = null;
            try
            {
                if (string.IsNullOrWhiteSpace(ConnectionString))
                    Init(havaNetwork);

                db = new DatabaseHelper(path + "mphygl.mdb", ConnectionString);
            }
            catch (Exception ex)
            {
                Log.Add(ex.StackTrace);
            }
            return db;
        }

        #region Private method

        /// <summary>
        /// 初始化并从服务获取数据库连接字符串，如果无网络或获取失败，则返回默认值
        /// </summary>
        /// <param name="havaNetwork">是否有网络，是无，否有</param>
        private void Init(bool havaNetwork)
        {
            var url = "http://www.xzmy.site/api/Sys/GetConnectionString";
            var defaultConnectionString = "Data Source=sds209635357.my3w.com;Initial Catalog=sds209635357_db; Persist Security Info=True;User ID=sds209635357; Password=E17680A936674932B358;MultipleActiveResultSets=true";

            if (!havaNetwork)
            {
                ConnectionString = defaultConnectionString;
                return;
            }

            var content = HttpRequestUtil.RequestUrl(url, "GET");
            var str = HttpRequestUtil.GetJsonValue(content, "Value");

            ConnectionString = string.IsNullOrWhiteSpace(str)
                ? defaultConnectionString
                : str;
        }

        #endregion
    }
}
