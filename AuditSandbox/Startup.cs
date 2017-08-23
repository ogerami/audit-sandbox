using System.Web.Http;
using Owin;

namespace AuditSandbox
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();

            Ioc.SimpleInjector.Initialize(httpConfiguration);

            app.UseWebApi(httpConfiguration);
        }
    }
}