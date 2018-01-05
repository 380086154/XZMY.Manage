using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Service.Customer
{
    /// <summary>
    /// 充值卡类型 服务
    /// </summary>
    public class CzkService
    {
        /// <summary>
        /// 根据卡类型获取 充值卡 信息
        /// </summary>
        /// <param name="klxmc"></param>
        /// <returns></returns>
        public CzkDto GetDetailsByKlxmc(string klxmc)
        {
            try
            {
                var service = new CustomSearchService<CzkDto>
                {
                    CustomConditions = new List<CustomCondition<CzkDto>>
                {
                    new CustomConditionPlus<CzkDto>
                    {
                        Value = klxmc ?? string.Empty,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<CzkDto, object>>[] {
                            x => x.yhzl,
                        }
                    }
                }
                };

                var result = service.Invoke();

                LogHelper.Log("查询余额 日志：", result.Count.ToString(), LogLevel.Debug);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("WeixinController 异常", ex.Message, LogLevel.Debug, ex);
            }
            return null;
        }
    }
}
