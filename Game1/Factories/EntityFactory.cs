using System;
using Game1.Api;
using Game1.Entities;
using Microsoft.Xna.Framework.Content;

namespace Game1.Factories
{
    public abstract class EntityFactory
    {
        protected readonly IEntityManager _entityManager;
        protected readonly ContentManager _contentManager;

        protected EntityFactory(IEntityManager entityManager, ContentManager contentManager)
        {
            _entityManager = entityManager;
            _contentManager = contentManager;
        }

        public virtual Entity CreateEntity()
        {
            var entity = (Entity)Activator.CreateInstance(typeof(Entity));
            _entityManager.RegisterEntity(entity);
            return entity;
        }
    }
}