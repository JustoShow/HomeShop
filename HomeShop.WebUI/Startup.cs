using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeShop.WebUI.Startup))]
namespace HomeShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
