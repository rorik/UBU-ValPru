using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GestorIncidencias.Models;
using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using Unity;

namespace GestorIncidencias
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IUnityContainer container = this.AddUnity();
            container.RegisterType<IContextoIncidencias, ContextoDB>();
        }
    }
}
