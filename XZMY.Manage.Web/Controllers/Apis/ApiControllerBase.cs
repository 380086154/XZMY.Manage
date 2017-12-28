using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XZMY.Manage.Model;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiControllerBase : ApiController
    {
        /// <summary>
        /// 日志
        /// </summary>
        //public ILog Log => LogFactory.GetLogger(GetType());

        //private IdentityInfo _currentIdentityInfo;

        /// <summary>
        /// 
        /// </summary>
        protected ApiControllerBase()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ApiResult Fail(string message = "")
        {
            var result = new ApiResult();
            result.Message = message;
            result.Fail();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ApiResult Success(string message, object data)
        {
            var result = new ApiResult<object>();
            result.Value = data;
            result.Message = message;
            result.Succeed();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ApiResult Success(object data = null)
        {
            var result = new ApiResult<object>();
            result.Value = data;
            result.Message = string.Empty;
            result.Succeed();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ApiResult Error(string message)
        {
            var result = new ApiResult();
            result.Message = message;
            result.Succeed();
            result.Code = -1;
            return result;
        }
    }
}