using NoNameGame.ECS.Core;

namespace NoNameGame.ECS.Messaging
{
    public class ComponentRemoved<TComponent> : IMessage where TComponent : ComponentBase
    {
        public TComponent Component { get; }
        public Entity Entity { get; }

        public ComponentRemoved(TComponent component, Entity entity)
        {
            Component = component;
            Entity = entity;
        }
    }
}
