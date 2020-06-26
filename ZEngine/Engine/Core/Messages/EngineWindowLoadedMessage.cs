using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Core.Messages
{
    public class EngineWindowLoadedMessage : AbstractMessage
    {
        public EngineWindowLoadedMessage(object sender) : base(sender)
        {
        }
    }
}