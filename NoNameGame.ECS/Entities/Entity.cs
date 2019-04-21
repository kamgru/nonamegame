using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using System.Collections.Generic;
using System.Linq;
namespace NoNameGame.ECS.Entities
{
    public abstract class Entity
    {
        protected readonly List<ComponentBase> _components = new List<ComponentBase>();
        protected ScreenPosition _transform;

        public string Name { get; set; }
        public bool Destroyed { get; protected set; }

        public ScreenPosition Transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = new ScreenPosition();
                    _components.Add(_transform);
                }
                return _transform;
            }
        }

        public bool HasComponent<TComponent>() where TComponent : ComponentBase
        {
            return _components.Any(x => x is TComponent);
        }

        public TComponent GetComponent<TComponent>() where TComponent : ComponentBase
        {
            return (TComponent)_components.FirstOrDefault(x => x is TComponent);
        }

        public TComponent AddComponent<TComponent>(TComponent component) where TComponent : ComponentBase
        {
            _components.Add(component);
            component.Entity = this;
            SystemMessageBroker.Send(new ComponentAdded<TComponent>(component, this));
            return component;
        }

        public void RemoveComponent<TComponent>(TComponent component) where TComponent : ComponentBase
        {
            _components.Remove(component);
            component.Entity = null;
            SystemMessageBroker.Send(new ComponentRemoved<TComponent>(component, this));
        }

        public IEnumerable<ComponentBase> GetComponents()
        {
            return _components.ToList();
        }

        public override string ToString()
        {
            return $"Entity: {Name} - components: {string.Join(", ", _components.Select(x => x.GetType().ToString()))}";
        }

        public static void Destroy(Entity entity)
        {
            entity.Destroyed = true;
            SystemMessageBroker.Send(new EntityDestroyed(entity));
        }
    }
}
