using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;

namespace XZMY.Manage.Service.Sys
{
    /// <summary>
    /// 分店服务
    /// </summary>
    public class BranchService
    {
        /// <summary>
        /// 获取所有分店
        /// </summary>
        /// <returns></returns>
        public IList<BranchDto> GetAll()
        {
            var service = new CustomSearchService<BranchDto>
            {
                CustomConditions = new List<CustomCondition<BranchDto>>
                    {
                        new CustomConditionPlus<BranchDto>
                        {
                            Value = string.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<BranchDto, object>>[] {
                                x => x.Name,
                            }
                        }
                    }
            };

            return service.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<BranchDto> GetByIdList(IList<Guid> list)
        {
            var service = new GetEntityByIdListService<BranchDto>(list);
            return service.Invoke();
        }
    }
}
