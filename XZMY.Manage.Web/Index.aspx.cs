using System;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Web.Controllers.Planners;

namespace XZMY.Manage.Web
{
    /// <summary>
    /// 测试页面
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(DateTime.Now);
        }
    }
}