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
        /// 是否店员，店员比普通用户显示更详细
        /// </summary>
        public List<string> IsShopAssistant = new List<string> 
        { 
            "oYVeUwHdKdQ9HfP6LkWu5PV2Aj80",//曾燕
            "oYVeUwBMvqS2MPDkvKx7YcSo156I",//钟林
            "oYVeUwIhRJoOAYybm8EYgtQOIMSM",//阳绪围
            "oYVeUwHCe_uHP9HmKNqVHfCPT4o8",//阳绪洋
            "oYVeUwOvEkDuA6kMOuG2Csp_KvCE",//胡月
            "oYVeUwOSNj7wCFrvZMPbW8SBA-Y8",//刘小平
        };

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
                PageSize = 100,
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
        /// 关键字查询
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        public PagedResult<HyxxDto> GetByKeywords(string keywords)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = 1,
                PageSize = 100,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = keywords ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.yddh,
                            x => x.hyxm,
                            x => x.xmjm,
                            x => x.hykh,
                    }
                }
                },
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.jrrq },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
            };

            return service.Invoke();
        }

        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="fromUserName"></param>
        /// <returns></returns>
        public string GetDetailsByKeywords(string keywords, string fromUserName)
        {
            var result = GetByKeywords(keywords);
            return GetReply(result.Results, fromUserName);
        }

        /// <summary>
        /// 根据会员电话返回 余额 信息
        /// </summary>
        /// <param name="yddh"></param>
        /// <param name="fromUserName">OpenID</param>
        /// <returns></returns>
        public string GetDetailsByYddh(string yddh, string fromUserName)
        {
            var result = GetByYddh(yddh);
            return GetReply(result.Results, fromUserName);
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

        #region Private method

        private string GetBranchName(Guid branchId)
        {
            switch (branchId.ToString())
            {
                case "3389ca9f-57ec-44f1-a818-61370d61f553": return "华创";
                case "949d7d00-7c85-4080-9ee3-9e65ccae575d": return "渝西";
                case "e7d12da5-50d8-4a01-ae3f-cab673845db7": return "测试";
                default:
                    break;
            }
            return "Test";
        }

        public string GetReply(IList<HyxxDto> collection, string fromUserName)
        {
            if (collection.Count == 0)
            {
                var r = new Random();
                var phone = string.Format(PhoneControl ? "{0} / {1}" : "{1} / {0}", "13609423790", "17130955511");
                return "没有查询到会员卡信息。\r\n如果已办理会员卡，请致电 " + phone + "更新信息。";
            }

            var sb = new StringBuilder();
            var zkkService = new ZkkService();
            var list = new List<HyxxDto>();

            sb.AppendFormat("查询到 {0} 张会员卡：\r\n", collection.Count);

            if (!IsShopAssistant.Contains(fromUserName))
            {
                foreach (var item in collection)
                {//合并多张卡，对普通客户只显示一张
                    var entity = list.FirstOrDefault(x =>
                        x.yddh == item.yddh &&
                        x.hykh == item.hykh &&
                        x.xmjm == item.xmjm &&
                        x.hyxm == item.hyxm &&
                        x.klxmc == item.klxmc);

                    if (entity != null)
                    {
                        entity.knje += item.knje;
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                list.AddRange(collection);
            }

            //sb.Append"<p style='font-size:10px'>");
            var carCount = 0;
            //foreach (var item in list)
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var isCzk = item.klxmc.Contains("储值卡");//是否充值卡
                var isShopAssistant = IsShopAssistant.Contains(fromUserName);

                if (isShopAssistant)
                {
                    sb.AppendFormat("{0}", GetBranchName(item.BranchDataId));//分店名称
                    sb.AppendFormat(" {0}", item.hykh.Trim());//会员卡号
                    sb.AppendFormat(" {0}", item.hyxm.Trim());//会员姓名
                }
                else
                {
                    if (item.knje == 0) continue;
                }
                carCount++;
                var kmc = item.kmc;
                float zk = 0;
                if (float.TryParse(item.kmc, out zk))
                {
                    if (zk <= 1)
                    {
                        kmc = (zk * 10) + "折";
                    }
                }

                sb.AppendFormat(" {0}", kmc);
                sb.AppendFormat(" {0} ", item.klxmc);
                sb.AppendFormat("剩余 {0} ", item.knje.ToString("F" + (isCzk ? 2 : 0)));
                sb.Append(isCzk ? "元" : "次");

                if (i != list.Count - 1) sb.Append("\r\n");
            }
            //sb.Append("</p>");

            return sb.ToString().Replace("到 " + collection + " 张", "到 " + carCount + " 张");
        }

        #endregion
    }
}
