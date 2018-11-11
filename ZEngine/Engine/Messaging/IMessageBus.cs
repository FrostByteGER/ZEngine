using System;

namespace ZEngine.Engine.Messaging
{
    public interface IMessageBus
    {
        void Subscribe<T>(Action<IMessage> callback) where T : IMessage;
        void Unsubscribe<T>(Action<IMessage> callback) where T: IMessage;
        void Publish(IMessage message);
        void Cleanup();
    }
}