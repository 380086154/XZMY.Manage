using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    public class StudentTotalScoreController : ControllerBase
    {
        public ActionResult Edit()
        {
            VmStudentTotalScore model = new VmStudentTotalScore();
            var list = GetList();
            if (list.Count > 0)
            {
                model = list[0];
            }
            else
            {
                //没有数据创建默认数据
                model.EnglishScore = 100;
                model.LearnScore = 100;
                model.QualityScore = 100;
                CreateEdit(model);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AjaxEdit(VmStudentTotalScore model)
        {
            Guid Id = CreateEdit(model);
            if (Id == Guid.Empty)
            {
                return Json(new { success = false, Id = Id, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = Id, errors = GetErrors() });
            }
        }
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmStudentTotalScore GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<StudentTotalScore>(Id);
            var entity = service.Invoke() ?? new StudentTotalScore();
            return entity.CreateViewModel<StudentTotalScore, VmStudentTotalScore>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmStudentTotalScore model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<StudentTotalScore>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<StudentTotalScore>(model);
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
            var service = new BaseDeleteService<StudentTotalScore>(Id);
            service.Invoke();
        }

        public List<VmStudentTotalScore> GetList()
        {

            var service = new GetEntityListService<StudentTotalScore>()
            {
                PageIndex = 1,
                PageSize = 1,
                SortMember = new Expression<Func<StudentTotalScore, object>>[] { x => x.CreatedTime },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Asc
            };

            var result = service.Invoke();
            List<VmStudentTotalScore> list = new List<VmStudentTotalScore>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<StudentTotalScore, VmStudentTotalScore>());
            }
            return list;

        }
        #endregion
    }
}