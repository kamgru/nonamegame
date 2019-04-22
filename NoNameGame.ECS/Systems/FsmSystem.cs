using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
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
        private readonly HashSet<Entity> _entities = new HashSet<Entity>();

        public FsmSystem()
        {
            _handlersDictionary = new Dictionary<string, StateHandlerBase>();
            SystemMessageBroker.AddListener<ComponentAdded<State>>(this);
        }

        public void Handle(ComponentAdded<State> message)
        {
            if (!_entities.Contains(message.Entity))
            {
                _entities.Add(message.Entity);
            }
        }

        public override void Handle(EntityDestroyed message)
        {
            _entities.Remove(message.Entity);
        }

        public void RegisterHandler(StateHandlerBase handler)
        {
            _handlersDictionary.Add(handler.State, handler);
        }

        public void Update(GameTime gameTime)
        {
            var groups = _entities
                .Select(x => new EntityState { Entity = x, State = x.GetComponent<State>() })
                .GroupBy(x => x.State.CurrentState)
                .ToList();

            foreach (var group in groups)
            {
                if (_handlersDictionary.ContainsKey(group.Key))
                {
                    foreach (var entity in group)
                    {
                        _handlersDictionary[group.Key].Handle(entity);
                    }
                }
            }

        }
    }
}
