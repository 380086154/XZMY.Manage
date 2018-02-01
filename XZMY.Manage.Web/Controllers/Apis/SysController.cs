using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Service.Customer;
using XZMY.Manage.Web.Models.Api;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 系统接口
    /// </summary>
    public class SysController : ApiControllerBase
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return Success("connectionString", connectionString);
        }

        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetTime()
        {
            return Success("DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}