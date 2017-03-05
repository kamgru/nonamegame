using Game1.Api;
using Game1.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Game1.Managers
{
    public class EntityManager : IEntityManager
    {

        private List<Entity> _entities = new List<Entity>();

        public void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity);
            var components = entity.GetComponents().ToArray();
            for (var i = 0; i < components.Count(); i++)
            {
                entity.RemoveComponent(components[i]);                
            }
            entity = null;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return _entities.ToList();
        }

        public void RegisterEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}
