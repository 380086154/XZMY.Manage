using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.ViewModel.Assessment;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using XZMY.Manage.Service.Sys;
using XZMY.Manage.Service.Customer;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class HyxxController : ControllerBase
    {
        //客户列表
        [AutoCreateAuthAction(Name = "会员信息", Code = "HyxxList", ModuleCode = "CUSTOMER", Url = "/Hyxx/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            GetBranch();
            return View();
        }

        [AutoCreateAuthAction(Name = "消费信息", Code = "HyxxPayment", ModuleCode = "CUSTOMER", Url = "/Hyxx/Payment", Visible = true, Remark = "")]
        public ActionResult Payment(string id)
        {
            ViewBag.Id = id;
            GetBranch();
            return View();
        }

        [AutoCreateAuthAction(Name = "跨店查询", Code = "HyxxSearch", ModuleCode = "CUSTOMER", Url = "/Hyxx/Search", Visible = true, Remark = "")]
        public ActionResult Search(string id)
        {
            return View();
        }

        /// <summary>
        /// 获取分店信息
        /// </summary>
        private void GetBranch()
        {
            ViewBag.IsAdmin = IsAdmin;
            if (!IsAdmin) return;

            var service = new CustomSearchWithPaginationService<BranchDto>
            {
                PageIndex = 1,
                PageSize = 20,
                CustomConditions = new List<CustomCondition<BranchDto>>
                {
                    new CustomConditionPlus<BranchDto>
                    {
                        Value = EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<BranchDto, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<BranchDto, object>>[] { x => x.Name }
            };
            ViewBag.BranchList = service.Invoke().Results;
        }

        //客户列表 Ajax 获取数据
        public ActionResult AjaxCustomerList(VmCustomer model)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.yddh,
                            x => x.hyxm,
                            x => x.xmjm,
                            x => x.hykh,
                        }
                    }
                },
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.jrrq },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
            };

            if (!IsAdmin)
            {
                service.CustomConditions.Add(new CustomConditionPlus<HyxxDto>
                {
                    Value = CurrentBranchDataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<HyxxDto, object>>[] {
                        x => x.BranchDataId
                    }
                });
            }

            if (model.BranchDataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<HyxxDto>
                {
                    Value = model.BranchDataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<HyxxDto, object>>[] {
                        x =>x.BranchDataId,
                    }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //消费列表 Ajax 获取数据
        public ActionResult AjaxPaymentList(VmCustomer model)
        {
            var service = new CustomSearchWithPaginationService<XfxxDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<XfxxDto>>
                {
                    new CustomConditionPlus<XfxxDto>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<XfxxDto, object>>[] {
                            x => x.czy,
                            x => x.hykh,
                            x => x.bz,
                        }
                    }
                },
                SortMember = new Expression<Func<XfxxDto, object>>[] { x => x.xfrq }
            };

            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                service.CustomConditions.Add(new CustomConditionPlus<XfxxDto>
                {
                    Value = model.Id ?? ViewBag.Id,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<XfxxDto, object>>[] {
                        x =>x.hykh,
                    }
                });
            }

            if (model.BranchDataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<XfxxDto>
                {
                    Value = model.BranchDataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<XfxxDto, object>>[] {
                        x =>x.BranchDataId,
                    }
                });
            }

            if (!IsAdmin)
            {
                if (string.IsNullOrWhiteSpace(model.Id) && CurrentBranchDataId != Guid.Empty)
                    service.CustomConditions.Add(new CustomConditionPlus<XfxxDto>
                    {
                        Value = CurrentBranchDataId,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<XfxxDto, object>>[] {
                        x => x.BranchDataId
                    }
                    });
            }

            var result = service.Invoke();
            
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxSearchList(VmSearchBase model)
        {
            if (string.IsNullOrWhiteSpace(model.Keyword)) return null;

            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<HyxxDto>>
                {
                    new CustomConditionPlus<HyxxDto>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.yddh,
                            x => x.hyxm,
                            x => x.xmjm,
                            x => x.hykh,
                        }
                    }
                },
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.jrrq },
                SortType = T2M.Common.DataServiceComponents.Data.Query.Interface.SortType.Desc
            };

            var result = service.Invoke();

            var branchService = new BranchService();
            var branchList = branchService.GetByIdList(result.Results.Select(x => x.BranchDataId).Distinct().ToList());

            var list = new List<VmSearch>();
            foreach (var item in result.Results)
            {
                var vmSearch = item.ConvertTo<VmSearch>();

                var entity = branchList.Where(x => x.DataId == item.BranchDataId).FirstOrDefault();
                vmSearch.BranchName = entity != null ? entity.Name : "未知";
                list.Add(vmSearch);
            }

            return Json(new { success = true, total = result.TotalCount, rows = list, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new HyxxDto();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<HyxxDto>(id.Value);
                entity = service.Invoke() ?? new HyxxDto();
            }

            return View(entity);
        }
    }
}