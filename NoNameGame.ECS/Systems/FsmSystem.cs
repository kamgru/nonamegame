using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.StateHandling;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.ECS.Systems
{
    public class FsmSystem
        : SystemBase,
        IUpdatingSystem,
        IMessageListener<ComponentAdded<State>>
    {
        private readonly IDictionary<string, StateHandlerBase> _handlersDictionary;
        private readonly HashSet<State> _states = new HashSet<State>();

        public FsmSystem()
        {
            _handlersDictionary = new Dictionary<string, StateHandlerBase>();
            SystemMessageBroker.AddListener<ComponentAdded<State>>(this);
        }

        public void Handle(ComponentAdded<State> message)
        {
            if (!_states.Contains(message.Component))
            {
                _states.Add(message.Component);
            }
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<State>())
            {
                _states.Remove(message.Entity.GetComponent<State>());
            }
        }

        public void RegisterHandler(StateHandlerBase handler)
        {
            _handlersDictionary.Add(handler.State, handler);
        }

        public override void Reset()
        {
            _states.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var state in _states.ToList())
            {
                if (_handlersDictionary.TryGetValue(state.CurrentState, out StateHandlerBase handler))
                {
                    handler.UpdateState(state.Entity, gameTime);
                }
            }
        }
    }
}
