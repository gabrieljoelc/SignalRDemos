using System;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRDemos.Messages.Server
{
    // http://www.ryantomlinson.com/post/Building-a-real-time-exception-monitor-with-NServiceBus-and-SignalR.aspx
    // https://github.com/SignalR/SignalR/wiki/SignalR-Client-Hubs
    internal static class SignalRProxyConnection
    {
        private static IHubProxy _proxy;
        private static HubConnection _hubConnection;
        private const string ConnectionUrl = "http://localhost:64596/";
        private const string HubName = "Chat";
        private const string HubMethodName = "SendByConnectionId";

        public static async void SendMessage(SendMessageCommand message)
        {
            Console.WriteLine("Trying to send message to hub: {0}", message);
            if (!IsConnected)
            {
                _hubConnection = new HubConnection(ConnectionUrl);
                _proxy = _hubConnection.CreateHubProxy(HubName);
                try
                {
                    await _hubConnection.Start();
                    Console.WriteLine("Success! Connected with client connection id {0}", _hubConnection.ConnectionId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "An error occured when trying to connection to {0} for Hub Proxy {1} for message {2} with this exception: {3}",
                        ConnectionUrl, HubName, message, ex.GetBaseException());
                    throw;
                }
            }
            try
            {
                await _proxy.Invoke(HubMethodName, message.ConnectionId, message.Message);
                Console.WriteLine("Success! Invoked SendByConnectionId() for message {0}", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred trying to invoke the Hub method {0} for message {1} with this exception: {2}",
                                  HubMethodName, message, ex.GetBaseException());
                throw;
            }
        }

        private static bool IsConnected
        {
            get { return _hubConnection != null && _hubConnection.State != ConnectionState.Connected; }
        }
    }
}