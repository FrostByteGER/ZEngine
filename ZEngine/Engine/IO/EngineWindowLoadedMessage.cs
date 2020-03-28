using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.IO
{
    public class EngineWindowLoadedMessage : AbstractMessage
    {
        public EngineWindowLoadedMessage(object sender) : base(sender)
        {
        }
    }
}