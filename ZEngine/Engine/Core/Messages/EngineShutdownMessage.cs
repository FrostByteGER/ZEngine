using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Core.Messages
{
    public class EngineShutdownMessage : AbstractMessage
    {
        public EngineShutdownMessage(Engine engine) : base(engine)
        {
            
        }
    }
}