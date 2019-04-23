using NoNameGame.ECS.Messaging;

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

        public abstract void Reset();
    }
}
