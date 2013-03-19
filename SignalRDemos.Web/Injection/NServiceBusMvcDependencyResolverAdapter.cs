using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NServiceBus.ObjectBuilder;

namespace SignalRDemos.Web.Injection
{
    // see http://www.make-awesome.com/2011/02/injecting-nservicebus-into-asp-net-mvc-3/
    class NServiceBusMvcDependencyResolverAdapter : IDependencyResolver
    {
        readonly IBuilder _builder;
        private readonly IConfigureComponents _configurer;

        public NServiceBusMvcDependencyResolverAdapter(IBuilder builder, IConfigureComponents configurer)
        {
            _builder = builder;
            _configurer = configurer;
        }

        public object GetService(Type serviceType)
        {
            if (_configurer.HasComponent(serviceType))
            {
                return _builder.Build(serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _builder.BuildAll(serviceType);
        }
    }
}