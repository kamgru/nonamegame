using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.ECS.Core
{
    public class EntityManager : IEntityManager
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity);
            var components = entity.GetComponents().ToArray();
            for (var i = 0; i < components.Count(); i++)
            {
                entity.RemoveComponent(components[i]);                
            }
            SystemMessageBroker.Send(new EntityDestroyed(entity));
        }

        public void RegisterEntity(Entity entity)
        {
            _entities.Add(entity);
            SystemMessageBroker.Send(new EntityCreated(entity));
        }
    }
}
