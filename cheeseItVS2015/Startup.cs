using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cheeseItVS2015.Startup))]
namespace cheeseItVS2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
