using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Agent;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.Order;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Members;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Order;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Order;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Web.Controllers.Program;
using XZMY.Manage.Web.Controllers.WebApis;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Project
{
    //活动订单报名
    public class OrderProjectController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "活动订单", Code = "OrderProjectList", ModuleCode = "ORDER", Url = "/OrderProject/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        //创建/编辑1
        [HttpGet]
        [AutoCreateAuthAction(Name = "活动代报名", Code = "OrderProjectEdit", ModuleCode = "ORDER", Url = "/OrderProject/Edit", Visible = true)]
        public ActionResult Edit(Guid? id)
        {
            var entity = new OrderProject();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<OrderProject>(id.Value);
                entity = service.Invoke();
            }

            return View(entity.CreateViewModel<OrderProject, VmOrderProject>());
        }

        //保存 创建/编辑 
        [HttpPost]
        public ActionResult AjaxEdit(VmOrderProject model)
        {
            string StudentName = "";
            if (Request.Params["RealName"] != null)
            {
                StudentName = Request.Params["RealName"].ToString();
            }
            string StudentMobile = "";
            if (Request.Params["Mobile"] != null)
            {
                StudentMobile = Request.Params["Mobile"].ToString();
            }

            string StudentEmail = "";
            if (Request.Params["Email"] != null)
            {
                StudentEmail = Request.Params["Email"].ToString();
            }
            string OrderLocationPathName = "";
            if (Request.Params["LocationPathName"] != null)
            {
                OrderLocationPathName = Request.Params["LocationPathName"].ToString();
            }
            model.Name = StudentName;
            model.RealName = StudentName;
            model.ContactName= StudentName;
            model.Mobile = StudentMobile;
            model.ContactMobile = StudentMobile;
            model.Email = StudentEmail;
            model.LocationName = OrderLocationPathName;
            model.LocationPathName = OrderLocationPathName;
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                //根据活动ID获取信息
                var Project = new GetEntityByIdService<Model.DataModel.Project.Project>(model.ProjectId).Invoke();

                if (Project == null)
                    return Json(new { success = false, errors = "活动信息IS NULL，活动ID：" + model.ProjectId });

                model.TotalPrice = Project.ActualPrice;
                model.DepositPrice = Project.DepositPrice;
                model.PayPrice = 0m;
                #region 查询学生用户  有,则赋值MemberId   无,则注册
                var MemberService = new CustomSearchWithPaginationService<Member>
                {
                    PageIndex = 1,
                    PageSize = 1,
                    CustomConditions = new List<CustomCondition<Member>>
                   {
                       new CustomConditionPlus<Member>
                       {
                           Value = model.Mobile,
                           Operation = SqlOperation.Equals,
                        Member   = new Expression<Func<Member, object>>[] { x => x.LoginName }
                       }
                   }
                };
                var memberResult = MemberService.Invoke();
                if (memberResult.TotalCount == 0 || memberResult.Results[0].DataId == Guid.Empty)
                {   //注册
                    AMemberController amember = new AMemberController();
                    ApiHandlerInvokeResult<Guid> aResult = amember.Register(new SmCreateMember() { AccName= model.Mobile, Password="123456", Type = 1 });
                    if (aResult.Success && aResult.Output != Guid.Empty)
                    {
                        model.MemberId = aResult.Output;
                    }
                    else
                    {
                        
                        return Json(new { success = false, errors = "报名失败！" });
                    }
                }
                else
                {
                    model.MemberId = memberResult.Results[0].DataId;
                }
                #endregion
                //是否已支付
                //if (model.IsPayCompletion == EOrderIsPayCompletion.是)
                //    model.PayPrice = model.DepositPrice == 0 ? model.TotalPrice : model.DepositPrice;

                var handler = new OrderProjectCreateHandler(model);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            return Json(new { status = false, errors = GetErrors() });
        }
        /// <summary>
        /// 活动评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEditComment(Guid Id, String CommentContent)
        {
            Guid PlannerId = Guid.Empty;
            String PlannerName = "";
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
            }


            //获取数据
            var entityOrderProject = new OrderProject();
            var service = new GetEntityByIdService<OrderProject>(Id);
            entityOrderProject = service.Invoke();
            VmOrderProject modelOrderProject = entityOrderProject.CreateViewModel<OrderProject, VmOrderProject>();

            //修改数据
            modelOrderProject.CommentContent = CommentContent;
            modelOrderProject.CommentPlannerId = PlannerId;
            modelOrderProject.CommentPlannerName = PlannerName;
            modelOrderProject.CommentTime = DateTime.Now;

            //保存数据
            var handler = new BaseModifyHandler<OrderProject>(modelOrderProject);
            var res = handler.Invoke();
            if (res.Code != 0)
            {
                return Json(new { success = false, errors = GetErrors() });
            }
            return Json(new { success = res.Success, Id = modelOrderProject.DataId, errors = GetErrors() });

        }

        //删除

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmOrderProject model)
        {
            var service = new CustomSearchWithPaginationService<OrderProject>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,

                CustomConditions = new List<CustomCondition<OrderProject>>
                {
                    new CustomConditionPlus<OrderProject>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<OrderProject, object>>[] { x => x.ProjectName, x=>x.Name }
                    }
                },

                SortMember = new Expression<Func<OrderProject, object>>[] { x => x.CreatedTime }
            };

            if (model.ProcessState > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<OrderProject>
                {
                    Value = model.ProcessState,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<OrderProject, object>>[] { x => x.ProcessState }
                });
            }
            if (model.StudentId != null)
            {
                if (model.StudentId != Guid.Empty)
                {
                    service.CustomConditions.Add(new CustomConditionPlus<OrderProject>
                    {
                        Value = model.StudentId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<OrderProject, object>>[] { x => x.StudentId }
                    });
                }
            }
            var result = service.Invoke();
            List<VmOrderProject> list = new List<VmOrderProject>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<OrderProject, VmOrderProject>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //代理商列表
        public ActionResult AjaxAgentList(string name)
        {
            var service = new CustomSearchWithPaginationService<V_AgentUserAgentContact>
            {
                PageIndex = 1,
                PageSize = 9999999,
                CustomConditions = new List<CustomCondition<V_AgentUserAgentContact>>
                {
                    new CustomConditionPlus<V_AgentUserAgentContact>
                    {
                        Value = "1",
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<V_AgentUserAgentContact, object>>[] { x => x.UserState }
                    }
                },
                SortMember = new Expression<Func<V_AgentUserAgentContact, object>>[] { x => x.CreatedTime }
            };
            if (!String.IsNullOrEmpty(name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<V_AgentUserAgentContact>
                {
                    Value = name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<V_AgentUserAgentContact, object>>[] { x => x.AgentCompanyName }
                });
            }
            var result = service.Invoke();
            var data = result.Results.Select(x => new
            {
                Id = x.DataId,
                Name = x.AgentCompanyName,
                Address = x.AgentAddress,
                ContactName = x.AgentContactName,
                Mobile = x.AgentContactMobile,
            });

            return Json(new { success = true, total = result.TotalCount, rows = data, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //活动列表
        public ActionResult AjaxProjectList(string name)
        {
            AgentProjectController bllAgentProject = new AgentProjectController();
            int TotalCount = 0;
            var listResults = bllAgentProject.GetList(new Model.ViewModel.Program.VmAgentProject() { }, out TotalCount);
            var data = listResults.Select(x => new
            {
                Id = Guid.NewGuid(),
                RowId = x.DataId,
                ProjectName = x.Name,
                Name = $"[{x.BeginDate.ToString("yyyy-MM-dd")}-{x.EndDate.ToString("yyyy-MM-dd")}]——" + x.Name,
                Code = x.Code,
                BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
            });

            return Json(new { success = true, total = TotalCount, rows = data, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id,string go)
        {
            var entity = new OrderProject();
            //如果为空则表示从订单列表跳转过来的，否 则表示从活动课程管理列表跳转过来的
            ViewBag.GoUrl = string.IsNullOrEmpty(go) ? "" : go;
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<OrderProject>(id.Value);
                entity = service.Invoke() ?? new OrderProject();
            }

            var vm = entity.CreateViewModel<OrderProject, VmOrderProjectDetails>();
            {
                var service = new GetEntityBySingleColumnService<Member>
                {
                    ColumnMember = x => x.Mobile,
                    ColumnValue = entity.Mobile
                };
                var result = service.Invoke();
                var member = result.FirstOrDefault();

                if (result.Count > 0 && member != null)
                {
                    vm.RealName = member.RealName;
                    vm.Email = member.Email;
                }
            }

            {
                var service = new GetEntityByIdService<Model.DataModel.Project.Project>(entity.ProjectId);
                var project = service.Invoke();

                if (project != null)
                {
                    vm.ProjectCode = project.Code;
                }
            }

            {
                var service = new GetEntityByIdService<AgentContact>(entity.AgentId);
                var agentContact = service.Invoke();

                if (agentContact != null)
                {
                    vm.AgentContactName = agentContact.Name;
                    vm.AgentContactMobile = agentContact.Mobile;
                }
            }

            return View(vm);
        }
        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmOrderProject GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<OrderProject>(Id);
            var entity = service.Invoke() ?? new OrderProject();
            return entity.CreateViewModel<OrderProject, VmOrderProject>();
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmOrderProject model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<OrderProject>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<OrderProject>(model);
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
            var service = new BaseDeleteService<OrderProject>(Id);
            service.Invoke();
        }

      
        #endregion

    }
}