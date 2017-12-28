using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using XZMY.Manage.Model.DataModel.Albums;
using XZMY.Manage.Model.ServiceModel.Albums;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Web.Controllers.Albums;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.WebApis
{
    public class AAlbumsController : ApiController
    {
        public ApiHandlerInvokeResult<SmAlbum[]> GetAll()
        {
            try
            {
                var service = new GetEntityTableService<Album>();
                var datas = service.Invoke();
                var res = datas.Select(m =>
                {
                    return new SmAlbum()
                    {
                        Id = m.DataId,
                        //ModifiedTime = m.ModifiedTime,
                        Url = (Request.RequestUri.Host + ":" + Request.RequestUri.Port + "/" + m.Url).Replace("\\","/"),
                        Title = m.Title,
                        Detail = m.Detail,
                        Thumbnail = (Request.RequestUri.Host + ":" + Request.RequestUri.Port +  "/" + m.Thumbnail).Replace("\\", "/"),

                    };
                }).ToArray();
                return new ApiHandlerInvokeResult<SmAlbum[]>
                {
                    Output = res,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiHandlerInvokeResult<SmAlbum[]>
                {
                    Exception = ex,
                    Message = ex.Message,
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                };
            }
        }
    }
}
