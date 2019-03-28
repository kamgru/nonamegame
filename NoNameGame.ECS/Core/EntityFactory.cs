using System;
using Microsoft.Xna.Framework.Content;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.ECS.Factories
{
    public abstract class EntityFactory
    {
        protected readonly ContentManager ContentManager;

        protected EntityFactory(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public virtual Entity CreateEntity()
        {
            var entity = (Entity)Activator.CreateInstance(typeof(Entity));
            SystemMessageBroker.Send(new EntityCreated(entity));
            return entity;
        }
    }
}