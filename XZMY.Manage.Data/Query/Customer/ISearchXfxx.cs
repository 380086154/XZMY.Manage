using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query.Interface;

namespace XZMY.Manage.Data.Query.Customer
{
    public interface ISearchXfxx : IBaseCreateQuery<Model.DataModel.XfxxDto>
    {
        string Keyword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Execute();
    }
}
