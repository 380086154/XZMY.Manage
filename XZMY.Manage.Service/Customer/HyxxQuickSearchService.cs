using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.ViewModel.Customer;

namespace XZMY.Manage.Service.Customer
{
    /// <summary>
    /// 快速查询
    /// </summary>
    public class HyxxQuickSearchService
    {
        public static IList<HyxxQuickSearchDto> list = new List<HyxxQuickSearchDto>();

        public PagedResult<HyxxQuickSearchDto> GetKeywords(VmQuickSearch model)
        {
            var service = new CustomSearchWithPaginationService<HyxxQuickSearchDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<HyxxQuickSearchDto>>
                {
                    new CustomConditionPlus<HyxxQuickSearchDto>
                    {
                        Value = model.Keywords ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<HyxxQuickSearchDto, object>>[] {
                            x => x.hykh,
                            x => x.hyxm,
                            x => x.xmjm,
                            x => x.yddh,
                        }
                    }
                },
                SortMember = new Expression<Func<HyxxQuickSearchDto, object>>[] { x => x.hyxm },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
            };

            if (model.BranchDataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<HyxxQuickSearchDto>
                {
                    Value = model.BranchDataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<HyxxQuickSearchDto, object>>[] {
                    x => x.BranchDataId
                }
                });
            }

            return service.Invoke();
        }
    }
}
