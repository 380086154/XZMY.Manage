using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Project;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Project
{
    public class ProjectCreateHandler : BaseCreateHandler<Model.DataModel.Project.Project>
    {
        public ProjectCreateHandler(IActionViewModel<Model.DataModel.Project.Project> vm) : base(vm)
        {
        }
        protected override IInvokeTransactionService<Model.DataModel.Project.Project> GetService(Model.DataModel.Project.Project model)
        {
            return new ProjectCreateService() { Model = model };
        }
    }
}
