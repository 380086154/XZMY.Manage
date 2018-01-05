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
    /// 折扣卡类型 服务
    /// </summary>
    public class ZkkService
    {
        /// <summary>
        /// 根据卡id获取 折扣卡 信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ZkkDto GetById(string id)
        {
            try
            {
                var service = new CustomSearchService<ZkkDto>
                {
                    CustomConditions = new List<CustomCondition<ZkkDto>>
                    {
                        new CustomConditionPlus<ZkkDto>
                        {
                            Value = id,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<ZkkDto, object>>[] {
                                x => x.id,
                            }
                        }
                    }
                };

                var result = service.Invoke();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("ZkkService 异常", ex.Message, LogLevel.Debug, ex);
            }
            return null;
        }
    }
}
