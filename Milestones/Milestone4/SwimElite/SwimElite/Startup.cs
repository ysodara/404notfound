using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwimElite.Startup))]

namespace SwimElite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}