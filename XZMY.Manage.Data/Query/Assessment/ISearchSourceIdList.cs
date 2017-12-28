using System;
using System.Collections.Generic;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.DataModel.User;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Data.Query.Assessment
{
    public interface ISearchSourceIdList 
    {
        List<Guid> IdList { get; set; }
    }
}
