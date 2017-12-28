using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.ViewModel;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ServiceModel
{
    public interface IActionServiceModel<T> : IActionViewModel<T> where T : EntityBase, IDataModel
    {

    }
    public interface IActionServiceModel2C<T> : IActionViewModel2C<T> where T : EntityBase, IDataModel
    {
    }
    public interface IActionServiceModel2M<T> : IActionViewModel2M<T> where T : EntityBase, IDataModel
    {
    }
    public interface IServiceModel<T> where T : IDataModel
    {
    }
}
