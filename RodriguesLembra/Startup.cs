using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RodriguesLembra.Startup))]
namespace RodriguesLembra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
