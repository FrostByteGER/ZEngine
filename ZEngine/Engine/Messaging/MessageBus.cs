using System;
using System.Collections.Generic;

namespace ZEngine.Engine.Messaging
{
    public class MessageBus : IMessageBus
    {
        private interface IMessageSubscription
        {
            public Guid Token { get; }
            object Target { get; }

            void Publish(IMessage msg);
        }


        private class MessageSubscription<TMessage> : IMessageSubscription where TMessage : IMessage
        {
            private readonly Action<TMessage> _action;

            public Guid Token { get; private set; }
            public object Target => _action.Target;
            public void Publish(IMessage msg)
            {
                _action.Invoke((TMessage)msg);
            }

            public MessageSubscription(Action<TMessage> action)
            {
                Token = Guid.NewGuid();
                _action = action;
            }
        }
        private readonly Dictionary<Type, List<IMessageSubscription>> _messageTable;

        public MessageBus()
        {
            _messageTable = new Dictionary<Type, List<IMessageSubscription>>();
        }

        public Guid Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage
        {
            var sub = new MessageSubscription<TMessage>(callback);
            if (_messageTable.TryGetValue(typeof(TMessage), out var list))
                list.Add(sub);
            else
                _messageTable.Add(typeof(TMessage), new List<IMessageSubscription> { sub });
            return sub.Token;
        }

        public void Unsubscribe<TMessage>(Guid token) where TMessage : IMessage
        {
            if (_messageTable.TryGetValue(typeof(TMessage), out var list))
            {
                list.RemoveAll(e => e.Token.Equals(token));
            }
        }

        public void UnsubscribeAll(object target)
        {
            //TODO:
            throw new NotImplementedException();
            //foreach (KeyValuePair<Type, List<IMessageSubscription>> subscription in _subscriptions)
            //    subscription.Value.RemoveAll(s => s.Target == target);
        }

        public void Publish(IMessage message)
        {
            if (_messageTable.TryGetValue(message.GetType(), out var list))
                foreach (var sub in list)
                {
                    sub.Publish(message);
                }
                    
        }

        // TODO: Evaluate performance
        public void Cleanup()
        {
            var actionsToRemove = new List<IMessageSubscription>();
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
            {
                _messageTable.Remove(type);

            }
        }
    }
}