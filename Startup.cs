using Microsoft.Owin;
using Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(ProyectoAlmaInicio.Startup))]
namespace ProyectoAlmaInicio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

   
}
