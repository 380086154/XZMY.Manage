using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Query.Project;
using T2M.Common.DataServiceComponents.Data.Impl.Query;

namespace XZMY.Manage.Data.Impl.Query.Project
{
    public class CreateProject: BaseCreateQuery<Model.DataModel.Project.Project>, ICreateProject
    {
        protected override void BuildMapping()
        {
            MapAll();
            Unmap(m => m.Code);
        }
    }
}
