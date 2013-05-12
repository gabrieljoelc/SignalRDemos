using System;
using SignalRDemos.Common.Libs.NServiceBus;

namespace SignalRDemos.Messages.Server 
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	{
        public void Init()
        {
            Configure.With().UseTransport<RabbitMQ>(CloudMQUtils.UseAppHarborAppSetting);
        }
	}

    public class MyClass : IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            Console.Out.WriteLine("SignalRDemos.Messages.Server endpoint is now started.");
        }

        public void Stop()
        {

        }
    }
}