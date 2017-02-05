using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSCGroupProject.Startup))]
namespace CSCGroupProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
