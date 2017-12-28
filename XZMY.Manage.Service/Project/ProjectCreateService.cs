using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Impl.Query.Project;
using XZMY.Manage.Model.DataModel.Project;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Project
{
    public class ProjectCreateService : BaseCreateService<Model.DataModel.Project.Project>
    {
        public override Model.DataModel.Project.Project Invoke(IDbTransaction transaction)
        {
            var query = new CreateProject();
            query.Model = Model;
            return query.Execute(transaction);
        }
    }
}
