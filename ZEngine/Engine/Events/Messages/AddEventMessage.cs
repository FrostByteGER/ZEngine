using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Events.Messages
{
    public class RegisterEventMessage : AbstractMessage
    {
        public EngineEvent Event { get; }
        public RegisterEventMessage(object sender, EngineEvent evt) : base(sender)
        {
            Event = evt;
        }
    }
}