using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Members;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.Member;
using XZMY.Manage.Service.Handlers.User;
using XZMY.Manage.Web.Controllers.WebApis;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Customers
{
    /// <summary>
    /// 会员
    /// </summary>
    public class MemberController : ControllerBase
    {
        /// <summary>
        /// 创建前台会员账号
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateMember()
        {
            return View();
        }
        //列表
        [AutoCreateAuthAction(Name = "客户列表", Code = "MemberList", ModuleCode = "CUSTOMERS", Url = "/Member/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //创建/编辑
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new Member();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Member>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Member, VmMember>());
        }

        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmMember model)
        {
            //if (ModelState.IsValid)
            //{
            int Gender = 0;


            if (Request.Params["Gender"] != null)
            {
                int.TryParse(Request.Params["Gender"].ToString(), out Gender);
            }
            model.Gender = (EGender)Gender;
            if (model.DataId == Guid.Empty)
            {

                ///创建一个新的账号
                ///
                string strPassword = Request.Params["txtPassword"];
                if (String.IsNullOrEmpty(strPassword))
                {
                    strPassword = "123456";
                }
                string strLoginName = Request.Params["LoginName"];
                int sType = Request.Params["Type"].ToInt32(2);
                string strRealName = Request.Params["RealName"];
                string strMobile = Request.Params["Mobile"];
                string strEmail = Request.Params["Email"];
                int sGender = Request.Params["Gender"].ToInt32(1);


                AMemberController bllAMember = new AMemberController();
                SmCreateMember modelCreate = new SmCreateMember();
                modelCreate.AccName = strLoginName;
                modelCreate.Password = strPassword;
                modelCreate.Type = sType;
                var ReturnMemberId = bllAMember.Register(modelCreate);

                #region  修改Member表数据
                var NewModel = GetMember(ReturnMemberId.Output);
                NewModel.RealName = strRealName;
                NewModel.Mobile = strMobile;
                NewModel.Email = strEmail;
                NewModel.Gender = (EGender)sGender;
                CreateEdit(NewModel);
                #endregion

                #region 修改家长表数据
                ParentController bllParent = new ParentController();
                int outParentCount = 0;
                var listParents = bllParent.GetList(new VmParents() { MemberId = NewModel.DataId }, out outParentCount);
                foreach (var m in listParents)
                {
                    m.Name = strRealName;
                    m.Mobile = strMobile;
                    m.Email = strEmail;
                    m.Gender = (EGender)sGender;
                    bllParent.CreateEdit(m);
                }
                #endregion
                #region 修改学生表数据
                StudentController bllStudent = new StudentController();
                int outStudentCount = 0;
                var listStudent = bllStudent.GetList(new VmStudent() { MemberId = NewModel.DataId }, out outStudentCount);
                foreach (var m in listStudent)
                {
                    m.Name = strRealName;
                    m.Mobile = strMobile;
                    m.Email = strEmail;
                    m.Gender = (EGender)sGender;
                    bllStudent.StudentAddEdit(m);
                }
                #endregion



                return Json(new { success = true, Id = NewModel.DataId, errors = GetErrors() });
            }
            else
            {

                var modelMember = GetMember(model.DataId);

                #region strPassword 密码
                //if (Request.Params["txtPassword"] != null)
                //{
                //    var strPassword = Request.Params["txtPassword"].ToString();
                //    if (!String.IsNullOrEmpty(strPassword))
                //    {
                //        modelMember.Password = strPassword.ToMd5();
                //    }
                //}
                #endregion




                modelMember.RealName = model.RealName;
                //modelMember.Mobile = model.Mobile;
                modelMember.Email = model.Email;
                modelMember.Gender = model.Gender;
                modelMember.State = model.State;





                #region 修改学生或家长表的信息
                int TotalCount = 0;

                StudentController bllStudent = new StudentController();
                var tempListStudent = bllStudent.GetList(new VmStudent() { MemberId = modelMember.DataId }, out TotalCount);
                if (tempListStudent.Count > 0)
                {
                    var modelStudent = tempListStudent[0];
                    modelStudent.Mobile = modelMember.Mobile;
                    modelStudent.Email = modelMember.Email;
                    modelStudent.Gender = modelMember.Gender;
                    modelStudent.Name = modelMember.RealName;
                    bllStudent.StudentAddEdit(modelStudent);
                }

                ParentController bllParent = new ParentController();
                var tempList = bllParent.GetList(new VmParents() { MemberId = modelMember.DataId }, out TotalCount);
                if (tempList.Count > 0)
                {
                    var modelParent = tempList[0];
                    modelParent.Mobile = modelMember.Mobile;
                    modelParent.Email = modelMember.Email;
                    modelParent.Gender = modelMember.Gender;
                    modelParent.Name = modelMember.RealName;
                    bllParent.CreateEdit(modelParent);
                }


                #endregion
                var handler = new MemberModifyHandler(modelMember);
                var res = handler.Invoke();

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //return Json(new { status = false, errors = GetErrors() });
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string loginName)
        {
            var service = new GetEntityBySingleColumnService<Member> { ColumnMember = x => x.LoginName, ColumnValue = loginName };

            var result = service.Invoke();
            //var flag = result.Count > 0 && (id != Guid.Empty || result.FirstOrDefault(x => x.Id != id) != null);

            var flag = false;
            var entity = result.FirstOrDefault(x => x.DataId != id);
            if (result.Count > 0 && entity != null && id != entity.DataId)
            {
                flag = true;
            }

            return Json(flag ? "已存在" : "true", JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmMember model)
        {
            var service = new CustomSearchWithPaginationService<Member>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Member>>()
                {
                    new CustomConditionPlus<Member>()
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Member, object>>[] { x => x.LoginName,x=>x.RealName }
                    }
                },
                SortMember = new Expression<Func<Member, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Member>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Member, object>>[] { x => x.State }
                });
            }
            var result = service.Invoke();
            List<VmMember> listVmMember = new List<VmMember>();
            foreach (var m in result.Results)
            {
                listVmMember.Add(m.CreateViewModel<Member, VmMember>());
            }
            return Json(new { success = true, total = result.TotalCount, rows = listVmMember, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new VmMember();

            if (id.HasValue)
            {
                entity = GetMember(id.Value);
            }

            return View(entity);
        }
        /// <summary>
        /// 获取会员基础信息
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public VmMember GetMember(Guid MemberId)
        {
            var entity = new Member();
            var service = new GetEntityByIdService<Member>(MemberId);
            entity = service.Invoke() ?? new Member();
            return entity.CreateViewModel<Member, VmMember>();
        }



        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmMember model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Member>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Member>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
    }
}