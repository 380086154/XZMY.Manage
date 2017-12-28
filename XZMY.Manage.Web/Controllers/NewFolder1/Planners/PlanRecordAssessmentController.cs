using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Plan;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    public class PlanRecordAssessmentController : ControllerBase
    {
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmPlanRecord_Assessment GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<PlanRecord_Assessment>(Id);
            var entity = service.Invoke() ?? new PlanRecord_Assessment();
            return entity.CreateViewModel<PlanRecord_Assessment, VmPlanRecord_Assessment>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmPlanRecord_Assessment model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<PlanRecord_Assessment>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<PlanRecord_Assessment>(model);
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
            var service = new BaseDeleteService<PlanRecord_Assessment>(Id);
            service.Invoke();
        }

        public List<VmPlanRecord_Assessment> GetList(VmPlanRecord_Assessment model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<PlanRecord_Assessment>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<PlanRecord_Assessment>>
                    {
                        new CustomConditionPlus<PlanRecord_Assessment>
                        {
                            Value = model.StudentId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<PlanRecord_Assessment, object>>[]
                            {
                                m => m.StudentId
                            },
                        }
                    },
                SortMember = new Expression<Func<PlanRecord_Assessment, object>>[] { m => m.CreatedTime },
            };
            if (model.PlanRecordId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord_Assessment>
                {
                    Value = model.PlanRecordId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord_Assessment, object>>[] { x => x.PlanRecordId }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord_Assessment>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord_Assessment, object>>[] { x => x.DataId }
                });
            }
           
            var result = service.Invoke();
            List<VmPlanRecord_Assessment> list = new List<VmPlanRecord_Assessment>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<PlanRecord_Assessment, VmPlanRecord_Assessment>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}