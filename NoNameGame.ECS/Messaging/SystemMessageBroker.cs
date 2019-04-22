using System;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.ECS.Messaging
{
    public static class SystemMessageBroker
    {
        private static IDictionary<Type, ICollection<object>> messageListeners = new Dictionary<Type, ICollection<object>>();

        public static void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            if (messageListeners.TryGetValue(typeof(TMessage), out ICollection<object> listeners))
            {
                foreach (var listener in listeners.Cast<IMessageListener<TMessage>>())
                {
                    listener.Handle(message);
                }
            }
        }

        public static void AddListener<TMessage>(IMessageListener<TMessage> listener) where TMessage : IMessage
        {
            if (!messageListeners.ContainsKey(typeof(TMessage)))
            {
                messageListeners[typeof(TMessage)] = new List<object>();
            }

            messageListeners[typeof(TMessage)].Add(listener);
        }
    }
}
