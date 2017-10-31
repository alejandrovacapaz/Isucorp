using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IsucorpTest.Web.Startup))]
namespace IsucorpTest.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
