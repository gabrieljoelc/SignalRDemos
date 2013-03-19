using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using NServiceBus.ObjectBuilder;

namespace SignalRDemos.Web.Injection
{
    class NServiceBusSignalRDependencyResolverAdapter : DefaultDependencyResolver
    {
        readonly IBuilder _builder;
        private readonly IConfigureComponents _configurer;

        public NServiceBusSignalRDependencyResolverAdapter(IBuilder builder, IConfigureComponents configurer)
        {
            _builder = builder;
            _configurer = configurer;
        }

        public override object GetService(Type serviceType)
        {
            if (_configurer.HasComponent(serviceType))
            {
                return _builder.Build(serviceType);
            }

            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (_configurer.HasComponent(serviceType))
            {
                return _builder.BuildAll(serviceType);
            }

            return base.GetServices(serviceType);
        }
    }
}