using NoNameGame.ECS.Api;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using System.Collections.Generic;

namespace NoNameGame.ECS.Systems
{
    public abstract class SystemBase : ISystem, IMessageListener<EntityDestroyed>
    {
        protected bool Active;

        protected SystemBase()
        {
            SystemMessageBroker.AddListener(this);
        }

        public virtual void SetActive(bool value)
        {
            Active = value;
        }

        public virtual bool IsActive()
        {
            return Active;
        }

        public abstract void Handle(EntityDestroyed message);
    }
}
