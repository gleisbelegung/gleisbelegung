using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class StatusMessageProcessor : EventListener<IncomingMessageEvent<StatusMessage>>
    {
        protected override void ProcessEvent(IncomingMessageEvent<StatusMessage> eventData)
        {
            GD.Print("needs procssing");
        }
    }
}