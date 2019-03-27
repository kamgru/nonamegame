using NoNameGame.ECS.Core;

namespace NoNameGame.ECS.Messaging
{
    public class ComponentAdded<TComponent> : IMessage where TComponent : ComponentBase
    {
        public TComponent Component { get; }
        public Entity Entity { get; }

        public ComponentAdded(TComponent component, Entity entity)
        {
            Component = component;
            Entity = entity;
        }
    }
}
