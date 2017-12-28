using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using XZMY.Manage.Model.DataModel.Program;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Program;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Program
{
    public class ProgramOrderController : ControllerBase
    {
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmProgramOrder GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<ProgramOrder>(Id);
            var entity = service.Invoke() ?? new ProgramOrder();
            return entity.CreateViewModel<ProgramOrder, VmProgramOrder>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmProgramOrder model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<ProgramOrder>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<ProgramOrder>(model);
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
            var service = new BaseDeleteService<ProgramOrder>(Id);
            service.Invoke();
        }

        public List<VmProgramOrder> GetList(VmProgramOrder model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<ProgramOrder>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<ProgramOrder>>
                    {
                        new CustomConditionPlus<ProgramOrder>
                        {
                            Value = model.ProgramName??String.Empty,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<ProgramOrder, object>>[]
                            {
                                m => m.ProgramName
                            },
                        }
                    },
                SortMember = new Expression<Func<ProgramOrder, object>>[] { m => m.CreatedTime },
            };
            if (model.ProgramId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramOrder>
                {
                    Value = model.ProgramId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramOrder, object>>[] { x => x.ProgramId }
                });
            }
            if (model.TemplateId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramOrder>
                {
                    Value = model.TemplateId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramOrder, object>>[] { x => x.TemplateId }
                });
            }
            if (model.StudentId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramOrder>
                {
                    Value = model.StudentId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramOrder, object>>[] { x => x.StudentId }
                });
            }
            if (model.MemberId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramOrder>
                {
                    Value = model.MemberId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramOrder, object>>[] { x => x.MemberId }
                });
            }
            if (model.Type>0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<ProgramOrder>
                {
                    Value = model.Type,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<ProgramOrder, object>>[] { x => x.Type }
                });
            }
            
            var result = service.Invoke();
            List<VmProgramOrder> list = new List<VmProgramOrder>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<ProgramOrder, VmProgramOrder>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}