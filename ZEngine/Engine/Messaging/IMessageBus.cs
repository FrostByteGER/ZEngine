using System;

namespace ZEngine.Engine.Messaging
{
    public interface IMessageBus
    {
        Guid Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;
        void Unsubscribe<TMessage>(Guid token) where TMessage: IMessage;
        void UnsubscribeAll(object target);
        void Publish(IMessage message);
        void Cleanup();
    }
}