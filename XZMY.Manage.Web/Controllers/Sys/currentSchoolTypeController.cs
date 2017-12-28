using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.School;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class currentSchoolTypeController : ControllerBase
    {
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmcurrentSchoolType GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<currentSchoolType>(Id);
            var entity = service.Invoke() ?? new currentSchoolType();
            return entity.CreateViewModel<currentSchoolType, VmcurrentSchoolType>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmcurrentSchoolType model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<currentSchoolType>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<currentSchoolType>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 物理删除对象
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            var service = new BaseDeleteService<currentSchoolType>(Id);
            service.Invoke();
        }

        public List<VmcurrentSchoolType> GetList(VmcurrentSchoolType model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<currentSchoolType>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<currentSchoolType>>
                    {
                        new CustomConditionPlus<currentSchoolType>
                        {
                            Value = model.currentSchoolTypeName??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<currentSchoolType, object>>[]
                            {
                                m => m.currentSchoolTypeName
                            },
                        }
                    },
                SortMember = new Expression<Func<currentSchoolType, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<currentSchoolType>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<currentSchoolType, object>>[] { x => x.DataId }
                });
            }
            if (model.currentSchoolTypeId>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<currentSchoolType>
                {
                    Value = model.currentSchoolTypeId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<currentSchoolType, object>>[] { x => x.currentSchoolTypeId }
                });
            }
            var result = service.Invoke();
            List<VmcurrentSchoolType> list = new List<VmcurrentSchoolType>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<currentSchoolType, VmcurrentSchoolType>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}