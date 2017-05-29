using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Events
{

    public class EventManager
    {
        private readonly Queue<IGameEvent> _gameEvents = new Queue<IGameEvent>();

        private readonly IDictionary<Type, List<Delegate>> _delegates = new Dictionary<Type, List<Delegate>>();

        public void Queue<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
        {
            _gameEvents.Enqueue(gameEvent);
        }

        public void RegisterListener<TEvent>(Action<TEvent> eventHandler) where TEvent : IGameEvent
        {
            if (!_delegates.ContainsKey(typeof(TEvent)))
            {
                _delegates.Add(typeof(TEvent), new List<Delegate>());
            }

            _delegates[typeof(TEvent)].Add(eventHandler);
        }

        public void Dispatch()
        {
            foreach (var gameEvent in _gameEvents.ToList())
            {
                var type = gameEvent.GetType();
                if (_delegates.ContainsKey(type))
                {
                    _delegates[type].ForEach(action => action.DynamicInvoke(gameEvent));
                    _gameEvents.Dequeue();
                }
            }
        }
    }
}