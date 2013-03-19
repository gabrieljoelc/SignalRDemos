using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using NServiceBus;

namespace SignalRDemos.Web.Injection
{
    public static partial class ConfigureWithNServiceBusDependecyInjection
    {
        public static Configure ForWebApi(this Configure configure, HttpConfiguration httpConfig)
        {
            //// Find every controller class so that we can register it
            var controllers = Configure.TypesToScan
                .Where(t => typeof(IHttpController).IsAssignableFrom(t));

            //// Register each controller class with the NServiceBus container
            foreach (Type type in controllers)
            {
                configure.Configurer.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);
            }

            // Set the Web API dependency resolver to use our resolver
            httpConfig.DependencyResolver =
                new NServiceBusWebApiDependencyResolverAdapter(configure.Builder, configure.Configurer);

            return configure;
        }
    }
}
