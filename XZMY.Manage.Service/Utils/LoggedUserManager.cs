using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.DataModel.User;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;

namespace XZMY.Manage.Service.Utils
{

    /// <summary>
    ///     登录用户管理
    /// </summary>
    public class LoggedUserManager
    {
        private const string CURRENT_INFO = "CURRENT_INFO";

        /// <summary>
        ///     测试帐号Id
        /// </summary>
        private static readonly Guid TestGuid = Guid.Parse("48f441b5-f772-4f5e-a88a-1dc37390b882");

        /// <summary>
        /// 管理员Id
        /// </summary>
        private static readonly Guid AdminId = Guid.Parse("48f441b5-f772-4f5e-a88a-1dc37390b882");

        #region Property

        /// <summary>
        /// 是否管理员
        /// </summary>
        public static bool IsAdmin { get; set; }

        #endregion

        public static CurrentUserAccountModel GetSystemAccount()
        {
            return new CurrentUserAccountModel
            {
                AccountId = Guid.Empty,
                Name = "System",
                AccountName = "System"
            };

        }

        /// <summary>
        ///     当前登录用户
        /// </summary>
        /// <returns></returns>
        public static CurrentUserAccountModel GetCurrentUserAccount()
        {
            try
            {
                var currentAccount =
                    HttpContext.Current.Session != null ?
                    HttpContext.Current.Session[CURRENT_INFO] as CurrentUserAccountModel :
                    HttpContext.Current.Items[CURRENT_INFO] as CurrentUserAccountModel;

                return currentAccount ?? GetSystemAccount();
            }
            catch
            {
                return GetSystemAccount();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="ip"></param>
        public static void SetCurrentUserAccount(UserAccount account, string ip)
        {
            //return;
            //var cache = new UserProfileStaticDataCache();
            //var acache = new UserAccountStaticDataCache();
            //var profile = cache.Find(accountid);
            //var account = acache.Find(accountid);
            //if (profile == null) return;
            //var service = new GetEntityByIdService<UserAccount>(accountid);
            //var profile = service.Invoke();
            //if (profile == null) return;

            #region Planner

            //Planner modelPlanner = new Planner();
            //var servicePlanner = new CustomSearchWithPaginationService<Planner>
            //{
            //    PageIndex = 1,
            //    PageSize = 1,
            //    CustomConditions = new List<CustomCondition<Planner>>
            //    {
            //        new CustomConditionPlus<Planner>
            //        {
            //            Value = profile.Id,
            //            Operation = SqlOperation.Equals,
            //            Member = new Expression<Func<Planner, object>>[] { x => x.UserId}
            //        }
            //    },
            //    SortMember = new Expression<Func<Planner, object>>[] { x => x.CreatedTime }
            //};
            //var resultPlanner = servicePlanner.Invoke();
            //var listPlanner = resultPlanner.Results;

            //if (resultPlanner.TotalCount > 0)
            //{
            //    modelPlanner = listPlanner[0];
            //}

            #endregion

            //var servicePlanner = new GetEntityByIdService<Planner>(Xml;
            //var profilePlanner = servicePlanner.Invoke();
            var currentAccount = new CurrentUserAccountModel
            {
                AccountId = account.DataId,
                BranchDataId = account.BranchDataId,
                IsAdmin = (account.DataId == AdminId),
                Name = account.LoginName,
                Email = account.Email,
                AccountName = account.LoginName ?? account.Mobile,
                IP = ip,
                //PlannerId= modelPlanner.Id,
                //PlannerName=modelPlanner.Name
                //DepartmentId = profile.DepartmentId,
                //DepartmentName = profile.DepartmentName,
                //DepartmentId = profile.DepartmentId,
                //DepartmentName = profile.DepartmentName,
                //Position = profile.Position
            };

            SetBranchDataId(account.BranchDataId);//保存分店 id，用于数据筛选

            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[CURRENT_INFO] = currentAccount;
            else
                HttpContext.Current.Items[CURRENT_INFO] = currentAccount;
        }

        /// <summary>
        /// 获取 Cookie 中的 AccountId
        /// </summary>
        /// <returns></returns>
        public static Guid GetCookieBranchDataId()
        {
            var current = HttpContext.Current;
            if (current != null)
            {
                var cookie = current.Request.Cookies["BranchDataId"];
                if (cookie != null)
                {
                    return cookie.Value.ToGuid().Value;
                }
            }
            return Guid.Empty;
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            var currentAccount =
                HttpContext.Current.Session != null ?
                HttpContext.Current.Session[CURRENT_INFO] as CurrentUserAccountModel :
                HttpContext.Current.Items[CURRENT_INFO] as CurrentUserAccountModel;

            return currentAccount != null;
        }

        /// <summary>
        /// 退出
        /// </summary>
        public static void Loginout()
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[CURRENT_INFO] = null;
            else
                HttpContext.Current.Items[CURRENT_INFO] = null;
        }

        #region Private method

        private static void SetBranchDataId(Guid id)
        {
            var cookie = new HttpCookie("BranchDataId");
            cookie.Expires = DateTime.Now.AddHours(20);
            cookie.Value = id.ToString();
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        ///// <summary>
        ///// 保存用户登录信息
        ///// </summary>
        ///// <param name="entity"></param>
        //public static void SetCurrentAccount(Models.UserAccount entity)
        //{
        //    var currentAccount = new CurrentUserAccountModel
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Password = entity.Password,
        //        Nickname = entity.Nickname,
        //        Email = entity.Email,
        //        DepartmentId = entity.DepartmentId,
        //        DepartmentName = entity.DepartmentName,
        //        DepartmentId = entity.DepartmentId,
        //        DepartmentName = entity.DepartmentName,
        //        Position = entity.Position,
        //        State = entity.State,
        //        SysBonus = entity.SysBonus
        //    };

        //    HttpContext.Current.Session[CURRENT_INFO] = currentAccount;
        //}
    }
}
