using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Utils.DataDictionary;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    public class ADataDictionaryController : ApiController
    {

        /// <summary>
        /// 获取全部数据字典
        /// </summary>
        /// <returns></returns>
        public ApiHandlerInvokeResult<Dictionary<string,List<DataDictionaryItem>>> GetAll()
        {
            try
            {
                var res = new Dictionary<string, List<DataDictionaryItem>>();
                var cs = DataDictionaryManager.GetCatagories();
                cs.ForEach(m => res.Add(m, DataDictionaryManager.GetCatagory(m).Values.ToList()));
                
                return new ApiHandlerInvokeResult<Dictionary<string, List<DataDictionaryItem>>>()
                {
                    Output = res,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<Dictionary<string, List<DataDictionaryItem>>>()
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }
    }
}

