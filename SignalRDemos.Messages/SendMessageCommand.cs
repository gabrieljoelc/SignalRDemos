using NServiceBus;

namespace SignalRDemos.Messages
{
    public class SendMessageCommand : ICommand
    {
        public string Message { get; set; }
        public string ConnectionId { get; set; }
        public override string ToString()
        {
            return string.Format("Message: {0}, ConnectionId: {1}", Message, ConnectionId);
        }
    }
}
