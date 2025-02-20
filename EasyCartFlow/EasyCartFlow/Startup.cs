using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyCartFlow.Startup))]
namespace EasyCartFlow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
