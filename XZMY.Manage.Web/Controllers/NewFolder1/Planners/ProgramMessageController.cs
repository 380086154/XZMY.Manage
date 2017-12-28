using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    public class ProgramMessageController : ControllerBase
    {
        #region AJAX
        public ActionResult AjaxProgramMessageEdit(VmProgramMessage model)
        {
            if (model.PlannerId != Guid.Empty)
            {
                if (String.IsNullOrEmpty(model.PlannerName))
                {
                    PlannerController bllPlanner = new PlannerController();
                    model.PlannerName = bllPlanner.GetPlanner(model.PlannerId).Name;
                }
            }
            model.MessageTime = DateTime.Now;

             Guid ProgramMessageId = ProgramMessageAddEdit(model);
            if (ProgramMessageId != Guid.Empty)
            {
                return Json(new { success = true, Id= ProgramMessageId, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 功能
        public Guid ProgramMessageAddEdit(VmProgramMessage model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<ProgramMessage>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<ProgramMessage>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        public VmProgramMessage ProgramMessageGetModel(Guid ProgramMessageId)
        {
            var service = new GetEntityByIdService<ProgramMessage>(ProgramMessageId);
            var entity = service.Invoke();
            return entity.CreateViewModel<ProgramMessage, VmProgramMessage>();
        }
        #endregion
    }
}