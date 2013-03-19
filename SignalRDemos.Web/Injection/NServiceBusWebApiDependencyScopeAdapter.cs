using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using NServiceBus.ObjectBuilder;

namespace SignalRDemos.Web.Injection
{
    // see http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
    class NServiceBusWebApiDependencyScopeAdapter : IDependencyScope
    {
        public NServiceBusWebApiDependencyScopeAdapter(IBuilder builder, IConfigureComponents configurer)
        {
            Configurer = configurer;
            Builder = builder;
        }

        protected IConfigureComponents Configurer { get; set; }
        protected IBuilder Builder { get; set; }

        public object GetService(Type serviceType)
        {
            if (Configurer.HasComponent(serviceType))
            {
                return Builder.Build(serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Builder.BuildAll(serviceType); 
        }

        public void Dispose()
        {
            Builder.Dispose();
        }
    }
}