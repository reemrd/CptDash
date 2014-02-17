using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CptDash.Startup))]
namespace CptDash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
