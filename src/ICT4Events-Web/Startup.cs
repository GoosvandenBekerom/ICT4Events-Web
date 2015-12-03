using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICT4Events_Web.Startup))]
namespace ICT4Events_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
