using System;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.Messaging
{
    public interface IMessageBus : IEngineService
    {
        Guid Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;
        void Unsubscribe<TMessage>(Guid token) where TMessage: IMessage;
        void UnsubscribeAll(object target);
        void Publish(IMessage message);
        void Cleanup();
    }
}