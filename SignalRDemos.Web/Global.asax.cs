using System.Web.Routing;
using NServiceBus;
using SignalRDemos.Web.Injection;

namespace SignalRDemos.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // NServiceBus configuration
            var bus = NServiceBus.Configure.With()
                               .DefaultBuilder()
/* as of 3-18-2013 - always configure SignalR before MVC but not sure about Web API so I'm putting SignalR before everything (see bottom of https://github.com/SignalR/SignalR/wiki/Extensibility) */.ForSignalR().ForMvc().ForWebApi(System.Web.Http.GlobalConfiguration.Configuration)
                               .XmlSerializer()
                               .Log4Net()
                               .MsmqTransport()
                               .IsTransactional(false)
                               .PurgeOnStartup(true)
                               .UnicastBus()
                               .ImpersonateSender(false)
                               .CreateBus()
                               .Start(
                                   () =>
                                   NServiceBus.Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>()
                                            .Install());
            
            System.Web.Mvc.AreaRegistration.RegisterAllAreas();

            WebApiConfig.RegisterRoutes(System.Web.Http.GlobalConfiguration.Configuration);
            MvcFilterConfig.RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}