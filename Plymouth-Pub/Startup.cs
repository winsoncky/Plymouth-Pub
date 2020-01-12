using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Plymouth_Pub.Startup))]
namespace Plymouth_Pub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
