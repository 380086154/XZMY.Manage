using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XZMY.Manage.Web.Startup))]
namespace XZMY.Manage.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
