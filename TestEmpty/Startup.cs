using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestEmpty.Startup))]
namespace TestEmpty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
