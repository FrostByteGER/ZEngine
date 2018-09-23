using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Services;

namespace SFML_Engine.Engine.Messaging
{
    public class MessageBus : IService, IMessageBus
    {

        private readonly Dictionary<Type, List<Action<IMessage>>> _messageTable;

        public MessageBus()
        {
            _messageTable = new Dictionary<Type, List<Action<IMessage>>>();
        }

        public void Subscribe<T>(Action<IMessage> callback) where T : IMessage
        {
            if (_messageTable.TryGetValue(typeof(T), out var list))
                list.Add(callback);
            else
                _messageTable.Add(typeof(T), new List<Action<IMessage>> { callback });
        }

        public void Unsubscribe<T>(Action<IMessage> callback) where T : IMessage
        {
            if (_messageTable.TryGetValue(typeof(T), out var list))
                list.Remove(callback);
        }

        public void Publish(IMessage message)
        {
            if (_messageTable.TryGetValue(message.GetType(), out var list))
                foreach (var action in list)
                    action.Invoke(message);
        }

        // TODO: Evaluate performance
        public void Cleanup()
        {
            var actionsToRemove = new List<Action<IMessage>>();
            var typesToRemove = new List<Type>();
            foreach (var kvp in _messageTable)
            {
                foreach (var action in kvp.Value)
                    if (action.Target == null)
                        actionsToRemove.Add(action);

                if (actionsToRemove.Count != kvp.Value.Count)
                {
                    foreach (var action in actionsToRemove)
                        kvp.Value.Remove(action);

                    if (kvp.Value.Count == 0)
                        typesToRemove.Add(kvp.Key);
                }
                else
                {
                    typesToRemove.Add(kvp.Key);
                }
            }

            foreach (var type in typesToRemove)
                _messageTable.Remove(type);
        }
    }
}