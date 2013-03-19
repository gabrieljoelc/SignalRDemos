using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalRDemos.Web.Injection
{
    // see http://www.make-awesome.com/2011/02/injecting-nservicebus-into-asp-net-mvc-3/
    public class NServiceBusMvcControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}