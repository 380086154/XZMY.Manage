using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.User;
using T2M.Common.DataServiceComponents.Data.Query.Interface;

namespace XZMY.Manage.Data.Query.User
{

    public interface ISearchUser : IPaginationQuery<UserAccount>
    {
        string Keyword { get; set; }
    }
}
