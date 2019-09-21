using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOIT.Startup))]
namespace SOIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
