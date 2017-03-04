using Game1.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Managers
{
    public class EntityManager : IEntityManager
    {

        private List<Entity> _entities = new List<Entity>();

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
