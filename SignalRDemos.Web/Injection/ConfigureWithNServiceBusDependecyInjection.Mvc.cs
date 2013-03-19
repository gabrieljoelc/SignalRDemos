using System;
using System.Linq;
using System.Web.Mvc;
using NServiceBus;

namespace SignalRDemos.Web.Injection
{
    public static partial class ConfigureWithNServiceBusDependecyInjection
    {
        // see http://www.make-awesome.com/2011/02/injecting-nservicebus-into-asp-net-mvc-3/
        public static Configure ForMvc(this Configure configure)
        {
            // Register our controller activator with NSB
            configure.Configurer.RegisterSingleton(typeof(IControllerActivator),
                new NServiceBusMvcControllerActivator());

            // Find every controller class so that we can register it
            var controllers = Configure.TypesToScan
                .Where(t => typeof(IController).IsAssignableFrom(t));

            // Register each controller class with the NServiceBus container
            foreach (Type type in controllers)
                configure.Configurer.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);

            // Set the MVC dependency resolver to use our resolver
            DependencyResolver.SetResolver(new NServiceBusMvcDependencyResolverAdapter(configure.Builder, configure.Configurer));

            return configure;
        }
    }
}
