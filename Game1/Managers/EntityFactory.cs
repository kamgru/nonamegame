using Game1.Api;
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
        private IEntityManager _entityManager;

        public EntityFactory(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public TEntity Create<TEntity>(params object[] args) where TEntity : Entity
        {            
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity), args);
            _entityManager.RegisterEntity(entity);
            return entity;
        }

    }
}
