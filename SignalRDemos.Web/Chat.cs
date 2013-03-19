using Microsoft.AspNet.SignalR;
using NServiceBus;
using SignalRDemos.Messages;

namespace SignalRDemos.Web
{
    public class Chat : Hub
    {
        private readonly IBus _bus;

        public Chat(IBus bus)
        {
            _bus = bus;
        }

        public void SendAll(string message)
        {
            Clients.All.addMessage(string.Format("Connection ID {0} broadcasted: {1}", Context.ConnectionId, message));
        }
        public void SendCaller(string message)
        {
            Clients.Caller.addMessage(string.Format("Caller said: {0}", message));
        }

        public void SendByConnectionId(string connectionId, string message)
        {
            Clients.Client(connectionId).addMessage(string.Format("Connection ID {0} said: {1}", connectionId, message));
        }

        public void SendSendMessageCommand(string message)
        {
            _bus.Send(new SendMessageCommand { Message = message + string.Format(" (from command message sent by Connection ID {0})", Context.ConnectionId), ConnectionId = Context.ConnectionId });
        }
    }
}