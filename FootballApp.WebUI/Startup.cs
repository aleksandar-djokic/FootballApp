using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballApp.WebUI.Startup))]
namespace FootballApp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);

        }
    }
}
