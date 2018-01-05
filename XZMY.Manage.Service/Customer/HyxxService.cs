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
    /// 会员信息 服务
    /// </summary>
    public class HyxxService
    {
        /// <summary>
        /// 根据会员电话返回 余额 信息
        /// </summary>
        /// <param name="yddh"></param>
        /// <returns></returns>
        public string GetDetailsByYddh(string yddh)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = 1,
                PageSize = 10,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = yddh ?? string.Empty,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                        x => x.yddh,
                    }
                }
                },
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.jrrq },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
            };

            var result = service.Invoke();

            if (result.Results.Count == 0)
            {
                return "没有查询到会员卡信息。如果已办理会员卡，请致电 18523038870 更新电话。";
            }

            var sb = new StringBuilder();
            var zkkService = new ZkkService();

            sb.AppendFormat("查询到 {0} 张会员卡：", result.Results.Count);

            foreach (var item in result.Results)
            {
                sb.AppendFormat("\r\n{0} {1} 剩余 {2} 元", item.kmc, item.klxmc, item.knje.ToString("F2"));
            }

            return sb.ToString();
        }
    }
}
