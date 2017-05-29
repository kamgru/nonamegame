using System;
using Microsoft.Xna.Framework.Content;
using Game1.ECS.Api;
using Game1.ECS.Core;

namespace Game1.ECS.Factories
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