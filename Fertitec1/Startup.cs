using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fertitec.Startup))]
namespace Fertitec
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
