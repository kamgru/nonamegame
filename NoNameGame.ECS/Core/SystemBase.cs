using NoNameGame.ECS.Api;
using NoNameGame.ECS.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.ECS.Core
{
    public abstract class SystemBase 
        : ISystem,
        IMessageListener<EntityCreated>,
        IMessageListener<EntityDestroyed>
    {
        protected bool Active;
        protected ICollection<Entity> Entities;

        protected SystemBase()
        {
            Entities = new List<Entity>();
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<EntityDestroyed>(this);
        }

        public virtual void SetActive(bool value)
        {
            Active = value;
        }

        public virtual bool IsActive()
        {
            return Active;
        }

        public virtual void Handle(EntityCreated message)
        {
        }

        public virtual void Handle(EntityDestroyed message)
        {
            Entities.Remove(message.Entity);
        }
    }
}
