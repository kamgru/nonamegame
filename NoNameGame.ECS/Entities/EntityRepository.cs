using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.ECS.Entities
{
    public class EntityRepository
        : IMessageListener<EntityCreated>,
        IMessageListener<EntityDestroyed>
    {
        private List<Entity> _entities = new List<Entity>();

        public EntityRepository()
        {
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<EntityDestroyed>(this);
        }

        public void Handle(EntityDestroyed message)
        {
            _entities.Remove(message.Entity);
        }

        public void Handle(EntityCreated message)
        {
            _entities.Add(message.Entity);
        }

        public IEnumerable<Entity> FindByComponent<TComponent>() where TComponent : ComponentBase
        {
            return _entities.Where(x => x.HasComponent<TComponent>());
        }

        public IReadOnlyCollection<Entity> GetAll() => _entities.AsReadOnly();
    }
}
