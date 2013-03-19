using System.Web.Http.Dependencies;
using NServiceBus.ObjectBuilder;

namespace SignalRDemos.Web.Injection
{
    // see http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
    class NServiceBusWebApiDependencyResolverAdapter : NServiceBusWebApiDependencyScopeAdapter,
                                                       IDependencyResolver
    {
        public NServiceBusWebApiDependencyResolverAdapter(IBuilder container, IConfigureComponents configurer)
            : base(container, configurer)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = Builder.CreateChildBuilder();
            return new NServiceBusWebApiDependencyScopeAdapter(child, Configurer);
        }
    }
}