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
    public class PlanRecordAssessmentAnswersController : ControllerBase
    {
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmPlanRecord_AssessmentAnswers GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<PlanRecord_AssessmentAnswers>(Id);
            var entity = service.Invoke() ?? new PlanRecord_AssessmentAnswers();
            return entity.CreateViewModel<PlanRecord_AssessmentAnswers, VmPlanRecord_AssessmentAnswers>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmPlanRecord_AssessmentAnswers model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<PlanRecord_AssessmentAnswers>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<PlanRecord_AssessmentAnswers>(model);
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
            var service = new BaseDeleteService<PlanRecord_AssessmentAnswers>(Id);
            service.Invoke();
        }

        public List<VmPlanRecord_AssessmentAnswers> GetList(VmPlanRecord_AssessmentAnswers model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<PlanRecord_AssessmentAnswers>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<PlanRecord_AssessmentAnswers>>
                    {
                        new CustomConditionPlus<PlanRecord_AssessmentAnswers>
                        {
                            Value = model.AnswersId,
                            Operation = SqlOperation.Equals,
                            Member = new Expression<Func<PlanRecord_AssessmentAnswers, object>>[]
                            {
                                m => m.AnswersId
                            },
                        }
                    },
                SortMember = new Expression<Func<PlanRecord_AssessmentAnswers, object>>[] { m => m.CreatedTime },
            };
            if (model.AnswersId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord_AssessmentAnswers>
                {
                    Value = model.AnswersId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord_AssessmentAnswers, object>>[] { x => x.AnswersId }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<PlanRecord_AssessmentAnswers>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<PlanRecord_AssessmentAnswers, object>>[] { x => x.DataId }
                });
            }

            var result = service.Invoke();
            List<VmPlanRecord_AssessmentAnswers> list = new List<VmPlanRecord_AssessmentAnswers>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<PlanRecord_AssessmentAnswers, VmPlanRecord_AssessmentAnswers>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}