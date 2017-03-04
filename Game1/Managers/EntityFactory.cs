using Game1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Managers
{
    public class EntityFactory : IEntityFactory
    {
        private List<Entity> _entities = new List<Entity>();

        public TEntity Create<TEntity>(params object[] args) where TEntity : Entity
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity), args);
            _entities.Add(entity);
            return entity;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return _entities.ToList();
        }
    }
}
