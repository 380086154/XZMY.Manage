using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.User;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Sys
{
    public class LocationController : ControllerBase
    {
        #region 功能
        public List<VmLocationEdit> GetList(VmLocationEdit model)
        {
            var service = new CustomSearchService<Location>
            {
                CustomConditions = new List<CustomCondition<Location>>
                {
                    new CustomConditionPlus<Location>
                    {
                        Value = model.Name??String.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Location, object>>[] { x => x.Name}
                    }
                }
            };
            var result = service.Invoke();
            List<VmLocationEdit> list = new List<VmLocationEdit>();
            foreach (var m in result)
            {
                list.Add(m.CreateViewModel<Location, VmLocationEdit>());
            }
            return list;
        }
        #endregion
        //列表
        [AutoCreateAuthAction(Name = "地区管理", Code = "LocationList", ModuleCode = "SYSTEM", Url = "/Location/List", Visible = true, Remark = "")]
        public ActionResult List(Guid? id)
        {
            var service = new CustomSearchService<Location>
            {
                CustomConditions = new List<CustomCondition<Location>>
                {
                    new CustomConditionPlus<Location>
                    {
                        Value = Guid.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Location, object>>[] { x => x.ParentId }
                    }
                }
            };

            var result = service.Invoke().OrderBy(x => x.EName).ToList();

            ViewBag.RootList = result;
            var entity = result.FirstOrDefault();
            if (id.HasValue)
            {
                foreach (var m in result)
                {
                    if (m.DataId == id.Value)
                    {
                        entity = m;
                    }
                }
            }
            ViewBag.Id = entity.DataId;
            return View(entity);
        }

        //创建/编辑
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new Location();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Location>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Location, VmLocation>());
        }

        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmLocation model)
        {
            
            if (model.DataId == Guid.Empty)
            {
                //model.Id = Guid.NewGuid();
                var ParentLocation = GetLocation(model.ParentId);
               
                model.Level = ParentLocation.Level + 1;
                model.PathId = String.Format("{0},{1}", ParentLocation.PathId, model.DataId);
                model.PathName = String.Format("{0},{1}", ParentLocation.PathName, model.Name);
                model.Sort = 1;


                var handler = new BaseCreateHandler<Location>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var ParentLocation = GetLocation(model.ParentId);
                
                model.Level = ParentLocation.Level + 1;
                model.PathId = String.Format("{0},{1}", ParentLocation.PathId, model.DataId);
                model.PathName = String.Format("{0},{1}", ParentLocation.PathName, model.Name);
                model.Sort = 1;

                var handler = new BaseModifyHandler<Location>(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string eName)
        {
            var service = new GetEntityBySingleColumnService<Location> { ColumnMember = x => x.EName, ColumnValue = eName };

            var result = service.Invoke();
            var flag = false;
            var entity = result.FirstOrDefault(x => x.DataId != id);
            if (result.Count > 0 && entity != null && id != entity.DataId)
            {
                flag = true;
            }

            return Json(flag ? "已存在" : "true", JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new CustomSearchService<Location>
            {
                CustomConditions = new List<CustomCondition<Location>>
                {
                    new CustomConditionPlus<Location>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Location, object>>[] { x => x.Name, x => x.EName }
                    }
                }
            };

            var result = service.Invoke().OrderBy(x => x.EName).ToList();

            var root = result.Where(x => x.ParentId == (Request.Params["id"] ?? "").ToGuid(Guid.Empty)).ToList();
            var children = result.Where(x => x.ParentId != Guid.Empty).ToList();

            //var dict = new List<string>();
            //foreach (var item in root)
            //{
            //    var childrenList = children.Where(x => x.ParentId == item.Id).ToList();

            //    dict.Add($@"""{item.Id}"":{GetItem(childrenList, children)}");
            //}

            return Json(new { success = true, rows = GetItem(root, children), errors = GetErrors() }, JsonRequestBehavior.AllowGet);
            //return Json(new { success = true, rows = dict, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //获取 JSON 数据
        public string GetItem(IList<Location> root, IList<Location> children, bool isChildren = false)
        {
            if (root == null || !root.Any()) return "[]";
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var item in root)
            {
                var childrenList = children.Where(x => x.ParentId == item.DataId).ToList();

                var vPathName = item.PathName.Split(",");
                var ParentName = "";
                if (vPathName.Length > 1)
                {
                    ParentName = vPathName[vPathName.Length - 2];
                }
                sb.AppendFormat(@"{{""text"":""{0}""", item.Name.Escape());
                sb.AppendFormat(@",""Id"":""{0}""", item.DataId);
                sb.AppendFormat(@",""Name"":""{0}""", item.Name.Escape());
                sb.AppendFormat(@",""ParentId"":""{0}""", item.ParentId);
                sb.AppendFormat(@",""ParentName"":""{0}""", ParentName);
                sb.AppendFormat(@",""EName"":""{0}""", item.EName);
                sb.AppendFormat(@",""Level"":""{0}""", item.Level);
                sb.AppendFormat(@",""Sort"":""{0}""", item.Sort);
                sb.AppendFormat(@",""PathId"":""{0}""", item.PathId);
                sb.AppendFormat(@",""PathName"":""{0}""", item.PathName);
                //sb.AppendFormat(@",""icon"":""{0}""", (isChildren ? "fa fa-folder icon-state-default" : ""));
                //sb.Append(@",""state"":{opened:true}");

                if (childrenList.Any())
                    sb.AppendFormat(@",""children"":{0}", GetItem(childrenList, children, true));//是否有子级

                sb.Append("},");
            }

            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);//剔除最后一个逗号
            sb.Append("]");

            return sb.ToString();
        }

        public string GetItem(IList<Location> children, bool isChildren = true)
        {
            if (children == null || !children.Any()) return @"[]";
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var item in children)
            {
                var childrenList = children.Where(x => x.ParentId == item.DataId).ToList();

                //sb.AppendFormat(@"{{""Id"":""{0}""", item.Id);
                sb.AppendFormat(@"{{""text"":""{0}""", item.Name.Escape());
                //sb.Append(@",""icon"":""1""");
                //sb.Append(@",""state"":{opened:true}");

                if (childrenList.Any())
                    sb.AppendFormat(@",""children"":{0}", GetItem(childrenList, children, true));//是否有子级

                sb.Append("},");
            }

            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);//剔除最后一个逗号
            sb.Append("]");

            return sb.ToString();
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new Location();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Location>(id.Value);
                entity = service.Invoke() ?? new Location();
            }

            return View(entity);
        }
        /// <summary>
        /// 通过ID 获取地区对象信息
        /// </summary>
        /// <param name="Id">地区ID</param>
        /// <returns></returns>
        public VmLocation GetLocation(Guid Id)
        {
            var entity = new Location();
            var service = new GetEntityByIdService<Location>(Id);
            entity = service.Invoke() ?? new Location();
            return entity.CreateViewModel<Location, VmLocation>();
        }


        public ActionResult AjaxGetVmLocation(Guid Id)
        {
            VmLocation model = GetLocation(Id);
            return Json(new { success = true, rows = model, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除该地区及以下地区
        /// </summary>
        /// <param name="Id">地区ID</param>
        /// <returns></returns>
        public ActionResult AjaxDelete(Guid Id)
        {
            var model = GetLocation(Id);
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CommandType cmdType = CommandType.Text;
            string cmdText = string.Format("DELETE from Location where pathid like '{0}%'", model.PathId);
            int QueryCount = T2M.Common.Utils.ADONET.SQLServer.SqlServerHelper.ExecuteNonQuery(connString, cmdType, cmdText);
            return Json(new { success = true}, JsonRequestBehavior.AllowGet);
        }
    }
}