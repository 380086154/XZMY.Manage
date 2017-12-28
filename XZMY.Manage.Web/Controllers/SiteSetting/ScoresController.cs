using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.SiteSetting;
using XZMY.Manage.Service.Handlers.User;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class ScoresController : ControllerBase
    {
        #region 功能
        public List<VmScoresEdit> GetListSourceID(Guid SourceId)
        {
            var list = new List<Scores>();
            if (SourceId!=Guid.Empty)
            {
                var service = new GetEntityBySingleColumnService<Scores>
                {
                    ColumnMember = m => m.SourceId,
                    ColumnValue = SourceId
                };
                list.AddRange(service.Invoke());
            }
            List<VmScoresEdit> listScores = new List<VmScoresEdit>();
            foreach (var m in list)
            {
                listScores.Add(m.CreateViewModel<Scores, VmScoresEdit>());
            }
            return listScores;
        }
        #endregion
        //列表 Ajax 获取数据
        public ActionResult AjaxList(Guid? sourceId)
        {
            var list = new List<Scores>();
            if (sourceId.HasValue)
            {
                var service = new GetEntityBySingleColumnService<Scores>
                {
                    ColumnMember = m => m.SourceId,
                    ColumnValue = sourceId
                };
                list.AddRange(service.Invoke());
            }
            return Json(new { success = true, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AjaxEdit(VmScoresEdit model)
        {
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Scores>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new BaseModifyHandler<Scores>(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }
        public void Create(VmScoresEdit model)
        {
            var handler = new BaseCreateHandler<Scores>(model);
            handler.Invoke();
        }
        public void Modify(VmScoresEdit model)
        {
            var handler = new BaseModifyHandler<Scores>(model);
            handler.Invoke();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmScoresEdit model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Scores>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Scores>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 获取分值列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="SourceType"></param>
        /// <returns></returns>
        public IList<Scores> GetListScores(VmSearchBase model, String SourceType)
        {
            var service = new CustomSearchWithPaginationService<Scores>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Scores>>
                {
                    new CustomConditionPlus<Scores>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Scores, object>>[] { x => x.ScoreItemsName }
                    }
                }
            };
            if (!String.IsNullOrEmpty(SourceType))
            {
                service.CustomConditions.Add(
                    new CustomConditionPlus<Scores>
                    {
                        Value = SourceType,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<Scores, object>>[] { x => x.SourceType }
                    });
            }
            var result = service.Invoke();
            return result.Results;
        }
    }
}