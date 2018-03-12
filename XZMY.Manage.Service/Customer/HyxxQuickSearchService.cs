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

        public PagedResult<HyxxQuickSearchDto> GetKeywords(string keywords)
        {
            var service = new CustomSearchWithPaginationService<HyxxQuickSearchDto>
            {
                PageIndex = 1,
                PageSize = 50,
                CustomConditions = new List<CustomCondition<HyxxQuickSearchDto>>
                {
                    new CustomConditionPlus<HyxxQuickSearchDto>
                    {
                        Value = keywords ?? string.Empty,
                        Operation = SqlOperation.Equals,
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

            return service.Invoke();
        }
    }
}
