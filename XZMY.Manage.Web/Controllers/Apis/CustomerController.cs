using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Web.Models.Api;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 客户接口
    /// </summary>
    public class CustomerController : ApiControllerBase
    {
        /// <summary>
        /// 根据电话获取客户信息
        /// </summary>
        /// <param name="yddh"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetHyxxByYddh(string yddh)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = 1,
                PageSize = 10,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = yddh,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<HyxxDto, object>>[] { x => x.yddh }
                    }
                },
                SortType = SortType.Desc
            };
            var result = service.Invoke();

            var entity = result.Results[0];

            return Success(new HyxxApiModel
            {
                hykh = entity.hykh,
                hyxm = entity.hyxm.Trim(),
                kmc = entity.kmc,
                knje = entity.knje,
                hyje = entity.hyje,
                jrrq = entity.jrrq,
            });
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

         [HttpGet]
        public ApiResult GetIntTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return Success("DateTime", (int)(DateTime.Now - startTime).TotalSeconds);
        }
    }
}