using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalcApp.Startup))]
namespace CalcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
        }
    }
}
