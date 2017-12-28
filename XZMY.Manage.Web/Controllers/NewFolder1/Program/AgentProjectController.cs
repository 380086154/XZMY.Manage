using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Program;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Program;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Program
{
    public class AgentProjectController : ControllerBase
    {
        #region Ajax
        /// <summary>
        /// Ajax获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmAgentProject model)
        {
            int TotalCount = 0;
            List<VmAgentProject> list = new List<VmAgentProject>();
            list = GetList(model, out TotalCount);
            return Json(new { success = true, total = TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
       
        #endregion
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmAgentProject GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<AgentProject>(Id);
            var entity = service.Invoke() ?? new AgentProject();
            return entity.CreateViewModel<AgentProject, VmAgentProject>();
        }
       
        public List<VmAgentProject> GetList(VmAgentProject model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<AgentProject>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<AgentProject>>
                    {
                        new CustomConditionPlus<AgentProject>
                        {
                            Value = model.Name,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<AgentProject, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<AgentProject, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<AgentProject>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<AgentProject, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.ScoreItemNames))
            {
                service.CustomConditions.Add(new CustomConditionPlus<AgentProject>
                {
                    Value = model.ScoreItemNames,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<AgentProject, object>>[] { x => x.ScoreItemNames }
                });
            }
            var result = service.Invoke();
            List<VmAgentProject> list = new List<VmAgentProject>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<AgentProject, VmAgentProject>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}