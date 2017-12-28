using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Impl.Query.User;
using XZMY.Manage.Data.Query.User;
using XZMY.Manage.Model.DataModel.User;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.User
{
    public class SearchUserService : IInvokeService<PagedResult<UserAccount>>
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public Expression<Func<UserAccount, object>>[] SortMember { get; set; }
        public SortType SortType { get; set; }

        public PagedResult<UserAccount> Invoke()
        {
            ISearchUser query = new SearchUser()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                SortMember = SortMember,
                SortType = SortType,
                Keyword = Keyword
            };

            return query.Execute();
        }
    }
}
