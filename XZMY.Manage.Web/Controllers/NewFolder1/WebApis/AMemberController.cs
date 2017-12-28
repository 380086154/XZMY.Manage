using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XZMY.Manage.Model.ServiceModel.Members;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.WebApiHandlers.Members;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    public class AMemberController : ApiController
    {

        // POST api/<controller>]
        /// <summary>
        /// 前台用户注册接口
        /// </summary>
        /// <param name="model">AccName = 登录名(手机或邮箱),Password = 密码,Type = 用户类型(1:学生,2:家长)</param>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Guid> Register([FromBody]SmCreateMember model)
        {
            if (model == null)
                return ApiHandlerInvokeResult<Guid>.NULL_VIEWMODEL;
            try
            {
                var handler = new MemberCreateHandler(model);
                var res = handler.Invoke();
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Message = res.Message,
                    Output = (Guid)res.DynamicOutput,
                    Success = res.Success,
                    Code = res.Code,
                };
            } 
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Guid>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }
    }
}