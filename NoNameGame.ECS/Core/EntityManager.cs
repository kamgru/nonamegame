using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using NoNameGame.ECS.Api;

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
        }

        public IEnumerable<Entity> GetEntities()
        {
            return _entities.ToList();
        }

        public IEnumerable<Entity> GetEntitiesByComponent<TComponent>() where TComponent : ComponentBase
        {
            var result = _entities.Where(x => x.HasComponent<TComponent>()).Distinct().ToList();
            return result;
        }

        public void RegisterEntity(Entity entity)
        {
            Debug.WriteLine($"RegisterEntity(): {entity.ToString()}");
            _entities.Add(entity);
        }
    }
}
