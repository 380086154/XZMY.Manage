using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
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
        /// 控制手机号先后顺序，避免所有用户集中拨打同一个号码
        /// </summary>
        public bool PhoneControl = false;

        /// <summary>
        /// 根据会员电话查询 会员信息
        /// </summary>
        /// <param name="yddh"></param>
        /// <returns></returns>
        public PagedResult<HyxxDto> GetByYddh(string yddh)
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

            return service.Invoke();
        }

        /// <summary>
        /// 根据会员电话返回 余额 信息
        /// </summary>
        /// <param name="yddh"></param>
        /// <returns></returns>
        public string GetDetailsByYddh(string yddh)
        {
            var result = GetByYddh(yddh);
            if (result.Results.Count == 0)
            {
                var r = new Random();
                var phone = string.Format(PhoneControl ? "{0} / {1}" : "{1} / {0}", "13609423790", "18523038870");
                return "没有查询到会员卡信息。\r\n如果已办理会员卡，请致电 " + phone + "更新信息。";
            }

            var sb = new StringBuilder();
            var zkkService = new ZkkService();

            sb.AppendFormat("查询到 {0} 张会员卡：", result.Results.Count);

            foreach (var item in result.Results)
            {
                var isCzk = item.klxmc.Contains("储值卡");//是否充值卡

                sb.AppendFormat("\r\n{0}", item.kmc);
                sb.AppendFormat(" {0} 剩余", item.klxmc);
                sb.AppendFormat(" {0} ", item.knje.ToString("F" + (isCzk ? 2 : 0)));
                sb.Append(isCzk ? "元" : "次");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<HyxxDto> GetByHykhList(IList<string> list, Guid branchDataId)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = 1,
                PageSize = 20,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = branchDataId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.BranchDataId,
                        },
                    },
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = string.Join(",", list.Select(m => "'" + m + "'")),
                        Operation = SqlOperation.In,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.hykh,
                        }
                    }
                },
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.jrrq },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
                
            };

            return service.Invoke().Results;
        }
    }
}
