using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRDemos.Messages.Server
{
    // http://www.ryantomlinson.com/post/Building-a-real-time-exception-monitor-with-NServiceBus-and-SignalR.aspx
    // https://github.com/SignalR/SignalR/wiki/SignalR-Client-Hubs
    public static class SignalRProxyConnection
    {
        private static bool _connected;
        private static IHubProxy _proxy;
        private static HubConnection _hubConnection;
        private const string ConnectionUrl = "http://localhost:64596/";

        public static void SendMessage(SendMessageCommand message)
        {
            if (!_connected)
            {
                Connect();
            }

            if (_hubConnection.State == ConnectionState.Connected)
                _proxy.Invoke("SendByConnectionId", message.ConnectionId, message.Message);
        }

        private static void Connect()
        {
            _hubConnection = new HubConnection(ConnectionUrl);
            _proxy = _hubConnection.CreateHubProxy("chat");
            _hubConnection.Start().Wait();
            _connected = true;
        }
    }
}