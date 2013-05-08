using System;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRDemos.Messages.Server
{
    // http://www.ryantomlinson.com/post/Building-a-real-time-exception-monitor-with-NServiceBus-and-SignalR.aspx
    // https://github.com/SignalR/SignalR/wiki/SignalR-Client-Hubs
    public static class SignalRProxyConnection
    {
        private static IHubProxy _proxy;
        private static HubConnection _hubConnection;
        private const string ConnectionUrl = "http://localhost:64596/";
        private const string HubName = "Chat";
        private const string HubMethodName = "SendByConnectionId";

        public static async void SendMessage(SendMessageCommand message)
        {
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
                        "An error occured when trying to connection to {0} for Hub Proxy {1} with this exception: {2}",
                        ConnectionUrl, HubName, ex.GetBaseException());
                }
            }
            try
            {
                await _proxy.Invoke(HubMethodName, message.ConnectionId, message.Message);
                Console.WriteLine("Success! Invoked SendByConnectionId()");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred trying to invoke the Hub method {0} with this exception: {1}",
                                  HubMethodName, ex.GetBaseException());
            }
        }

        private static bool IsConnected
        {
            get { return _hubConnection != null && _hubConnection.State != ConnectionState.Connected; }
        }
    }
}