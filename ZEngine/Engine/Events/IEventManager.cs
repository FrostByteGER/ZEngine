using ZEngine.Engine.Services;

namespace ZEngine.Engine.Events
{
    public interface IEventManager : IEngineService
    {
        void ProcessPendingEvents();
    }
}