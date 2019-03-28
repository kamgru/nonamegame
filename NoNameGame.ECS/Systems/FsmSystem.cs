using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.StateHandling;

namespace NoNameGame.ECS.Systems
{
    public class FsmSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<State>>
    {
        private readonly IDictionary<string, StateHandlerBase> _handlersDictionary;

        public FsmSystem()
        {
            _handlersDictionary = new Dictionary<string, StateHandlerBase>();
            SystemMessageBroker.AddListener<ComponentAdded<State>>(this);
        }

        public void Handle(ComponentAdded<State> message)
        {
            Entities.Add(message.Entity);
        }

        public override void Handle(EntityCreated message)
        {
            if (message.Entity.HasComponent<State>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void RegisterHandler(StateHandlerBase handler)
        {
            _handlersDictionary.Add(handler.State, handler);
        }

        public void Update(GameTime gameTime)
        {
            var groups = Entities
                .Select(x => new EntityState { Entity = x, State = x.GetComponent<State>()})
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
