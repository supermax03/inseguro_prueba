using Autofac;
using Autofac.Integration.WebApi;
using Core.Contracts;
using Infrastructure;
using Insecure_Deserialization.Controllers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Insecure_Deserialization
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Add DI
            var builder = new ContainerBuilder();
            builder.RegisterType<ValuesController>().InstancePerRequest();
            builder.RegisterType<LoadFromFile>().As<ILoadFlights>().InstancePerRequest();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            var configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = resolver;

        }
    }
}
