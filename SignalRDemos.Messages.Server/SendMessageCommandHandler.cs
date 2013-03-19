using NServiceBus;

namespace SignalRDemos.Messages.Server
{
    class SendMessageCommandHandler : IHandleMessages<SendMessageCommand>
    {
        public void Handle(SendMessageCommand message)
        {
            SignalRProxyConnection.SendMessage(message);
        }
    }
}
