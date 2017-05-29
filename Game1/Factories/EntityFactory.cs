using System;
using Game1.Api;
using Game1.Entities;
using Microsoft.Xna.Framework.Content;

namespace Game1.Factories
{
    public abstract class EntityFactory
    {
        protected readonly IEntityManager EntityManager;
        protected readonly ContentManager ContentManager;

        protected EntityFactory(IEntityManager entityManager, ContentManager contentManager)
        {
            EntityManager = entityManager;
            ContentManager = contentManager;
        }

        public virtual Entity CreateEntity()
        {
            var entity = (Entity)Activator.CreateInstance(typeof(Entity));
            EntityManager.RegisterEntity(entity);
            return entity;
        }
    }
}