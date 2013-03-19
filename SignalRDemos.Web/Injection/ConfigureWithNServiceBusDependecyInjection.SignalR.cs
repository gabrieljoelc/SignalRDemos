using System;
using System.Linq;
using NServiceBus;
using Microsoft.AspNet.SignalR;

namespace SignalRDemos.Web.Injection
{
    public static partial class ConfigureWithNServiceBusDependecyInjection
    {
        public static Configure ForSignalR(this Configure configure)
        {
            // Find every hub class so that we can register it
            var hubs = Configure.TypesToScan
                .Where(t => typeof(Hub).IsAssignableFrom(t));

            //// Register each hub class with the NServiceBus container
            foreach (Type type in hubs)
                configure.Configurer.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);

            // Set the SignalR dependency resolver to use our resolver
            GlobalHost.DependencyResolver = new NServiceBusSignalRDependencyResolverAdapter(configure.Builder, configure.Configurer);

            return configure;
        }
    }
}
