using System;
using NServiceBus;

namespace SignalRDemos.Messages
{
    public class SendMessageCommand : ICommand
    {
        public string Message { get; set; }
        public string ConnectionId { get; set; }
    }
}
