using System.Collections.Generic;
using ZEngine.Engine.Events.Messages;
using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Events
{
    internal class EventManager : IEventManager
    {
        private Queue<EngineEvent> EngineEvents { get; set; } = new Queue<EngineEvent>();
        private uint EventIDCounter { get; set; } = 0;

        private IMessageBus _bus;

        public EventManager(IMessageBus bus)
        {
            _bus = bus;
            _bus.Subscribe<RegisterEventMessage>(OnEventAdded);
        }

        private void OnEventAdded(RegisterEventMessage msg)
        {
            if(EngineEvents.Contains(msg.Event))
                return;
            EngineEvents.Enqueue(msg.Event);
        }

        public void ProcessPendingEvents()
        {
            // Execute all pending events in the Queue
            while (EngineEvents.Count > 0)
            {
                var engineEvent = EngineEvents.Dequeue();
                if (!engineEvent.Revoked)
                {
                    engineEvent.ExecuteEvent();
                }
            }
        }

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }
    }
}