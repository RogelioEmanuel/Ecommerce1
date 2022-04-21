using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FertilizantesV1.Startup))]
namespace FertilizantesV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
