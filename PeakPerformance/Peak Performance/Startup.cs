using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Peak_Performance.Startup))]

namespace Peak_Performance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}