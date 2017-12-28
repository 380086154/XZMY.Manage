using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.School;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Sys
{
    /// <summary>
    /// 学校中心
    /// </summary>
    public class SchoolController : ControllerBase
    {
        [AutoCreateAuthAction(Name = "学校列表", Code = "SchoolList", ModuleCode = "SCHOOL", Url = "/School/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 创建修改
        /// </summary>
        /// <returns></returns>
        [AutoCreateAuthAction(Name = "创建学校", Code = "SchoolEdit", ModuleCode = "SCHOOL", Url = "/School/Edit", Visible = true)]
        public ActionResult Edit(Guid? id)
        {
            BingCountryDropDownList();
            VmSchool modelSchool = new VmSchool();
            if (id.HasValue)
            {
                modelSchool = GetModel(id.Value);
            }
            return View(modelSchool);

        }
        public ActionResult Details(Guid? id)
        {
            var entity = new VmSchool();
            if (id.HasValue)
            {
                entity = GetModel(id.Value);
                return View(entity);
            }
            return RedirectToAction("List", "School");
        }

        /// <summary>
        /// 绑定国家下框
        /// </summary>
        /// <returns></returns>
        public ActionResult BingCountryDropDownList()
        {
            List<SelectListItem> dllCountry = new List<SelectListItem>();
            var service = new GetEntityBySingleColumnService<Location>()
            {
                ColumnMember = m => m.Level,
                ColumnValue = 1
            };
            dllCountry = service.Invoke().Select(m => new SelectListItem() { Text = m.Name, Value = m.DataId.ToString() }).ToList();
            ViewBag.dllCountry = dllCountry;
            return View();
        }


        //创建/编辑 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxEdit(VmSchool model)
        {
            model.SchoolTypeId = Request.Params["dllSchoolType"].ToGuid(Guid.Empty);
            if (model.LocationId == Guid.Empty)
                model.LocationId = "975CA0C5-E59F-477B-8C06-9287A0E9E7AF".ToGuid(Guid.Empty);

            model.TeacherCount = Request.Params["TeacherCount"].ToInt32(0);
            model.StudentCount = Request.Params["StudentCount"].ToInt32(0);
            
            //if (ModelState.IsValid)
            //{

            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.School.School>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = res.Message });
                }
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.School.School>(model);
                var res = handler.Invoke();

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }
        /// <summary>
        /// 加载列表页面内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new CustomSearchWithPaginationService<Model.DataModel.School.School>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Model.DataModel.School.School>>
                    {
                        new CustomConditionPlus<Model.DataModel.School.School>
                        {
                            Value = model.Keyword,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<Model.DataModel.School.School, object>>[]
                            {
                                m => m.Name, m => m.Moblie
                            },
                        }
                    },
                SortMember = new Expression<Func<Model.DataModel.School.School, object>>[] { m => m.CreatedTime },
            };
            var result = service.Invoke();
            SchoolTypeController bllSchoolType = new SchoolTypeController();
            int SchoolTypeCount = 0;
            List<VmSchoolType> listSchoolType = bllSchoolType.GetList(new VmSchoolType() { State = Model.Enum.EState.启用 }, out SchoolTypeCount);
            List<VmSchool> listSchool = new List<VmSchool>();
            foreach (var m in result.Results)
            {
                var mSchool = m.CreateViewModel<School, VmSchool>();
                mSchool.listSchoolType = listSchoolType;
                listSchool.Add(mSchool);
            }
            return Json(new { success = true, total = result.TotalCount, rows = listSchool, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxSchoolDelele(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                DeleleSchool(Id);
                return Json(new { success = true, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = false, errors = GetErrors() });
            }
        }

        #region 功能
        /// <summary>
        /// 获取单独对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmSchool GetModel(Guid Id)
        {
            int TotalCount = 0;
            SchoolTypeController bllSchoolType = new SchoolTypeController();
            var service = new GetEntityByIdService<Model.DataModel.School.School>(Id);
            var entity = service.Invoke() ?? new Model.DataModel.School.School();
            var VmModel = entity.CreateViewModel<Model.DataModel.School.School, VmSchool>();
            VmModel.listSchoolType = bllSchoolType.GetList(new VmSchoolType() { State = Model.Enum.EState.启用 }, out TotalCount);
            return VmModel;
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmSchool model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Model.DataModel.School.School>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.School.School>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        /// <summary>
        /// 物理删除学校数据
        /// </summary>
        /// <param name="SchoolId"></param>
        public void DeleleSchool(Guid SchoolId)
        {
            var service = new BaseDeleteService<Model.DataModel.School.School>(SchoolId);
            service.Invoke();
        }


        public List<VmSchool> GetList(VmSchool model, out int TotalCount)
        {
            var service = new CustomSearchWithPaginationService<School>
            {
                PageIndex = model.PageIndex == 0 ? 1 : model.PageIndex,
                PageSize = model.PageSize == 0 ? 99999 : model.PageSize,
                CustomConditions = new List<CustomCondition<School>>
                    {
                        new CustomConditionPlus<School>
                        {
                            Value = model.Name,
                            Operation = SqlOperation.Like,
                            Member = new Expression<Func<School, object>>[]
                            {
                                m => m.Name
                            },
                        }
                    },
                SortMember = new Expression<Func<School, object>>[] { m => m.CreatedTime },
            };
            if (model.DataId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<School>
                {
                    Value = model.DataId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<School, object>>[] { x => x.DataId }
                });
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                service.CustomConditions.Add(new CustomConditionPlus<School>
                {
                    Value = model.Name,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<School, object>>[] { x => x.Name }
                });
            }
            if (model.SchoolTypeId != Guid.Empty)
            {
                service.CustomConditions.Add(new CustomConditionPlus<School>
                {
                    Value = model.SchoolTypeId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<School, object>>[] { x => x.SchoolTypeId }
                });
            }
            var result = service.Invoke();
            List<VmSchool> list = new List<VmSchool>();
            foreach (var m in result.Results)
            {
                list.Add(m.CreateViewModel<School, VmSchool>());
            }
            TotalCount = result.TotalCount;
            return list;
        }
        #endregion
    }
}