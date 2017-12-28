using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Customers
{
    public class ParentController : ControllerBase
    {
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmParents GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<Parent>(Id);
            var entity = service.Invoke() ?? new Parent();
            return entity.CreateViewModel<Parent, VmParents>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmParents model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Parent>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Parent>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        public List<VmParents> GetList(VmParents model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<Parent>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<Parent>>
                    {
                        new CustomConditionPlus<Parent>
                        {
                            Value = model.Keyword??String.Empty,
                            Operation = T2M.Common.DataServiceComponents.Data.Query.SqlOperation.Like,
                            Member = new Expression<Func<Parent, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<Parent, object>>[] { m => m.CreatedTime },
            };
            if (model.MemberId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Parent>
                {
                    Value = model.MemberId,
                    Operation = T2M.Common.DataServiceComponents.Data.Query.SqlOperation.Equals,
                    Member = new Expression<Func<Parent, object>>[] { x => x.MemberId }
                });
            }
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Parent>
                {
                    Value = model.DataId,
                    Operation = T2M.Common.DataServiceComponents.Data.Query.SqlOperation.Equals,
                    Member = new Expression<Func<Parent, object>>[] { x => x.DataId }
                });
            }
           
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<Parent>
                {
                    Value = model.Name,
                    Operation = T2M.Common.DataServiceComponents.Data.Query.SqlOperation.Like,
                    Member = new Expression<Func<Parent, object>>[] { x => x.Name }
                });
            }
            var result = service.Invoke();
            List<VmParents> list = new List<VmParents>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<Parent, VmParents>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
        public ActionResult Details(Guid? Id)
        {
            var entity = new Parent();
            if (Id.HasValue)
            {
                var service = new GetEntityByIdService<Parent>(Id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Parent, VmParents>());
        }
    }
}