using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Game1.ECS.Core;
using Game1.ECS.Api;
using Game1.ECS.Components;

namespace Game1.ECS.Systems
{
    public class FsmSystem : SystemBase, IUpdatingSystem
    {
        private readonly IDictionary<string, StateHandlerBase> _handlersDictionary;

        public FsmSystem(IEntityManager entityManager) : base(entityManager)
        {
            _handlersDictionary = new Dictionary<string, StateHandlerBase>();
        }

        public void RegisterHandler(StateHandlerBase handler)
        {
            _handlersDictionary.Add(handler.State, handler);
        }

        public void Update(GameTime gameTime)
        {
            var groups = EntityManager.GetEntities().Where(x => x.HasComponent<State>())
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
