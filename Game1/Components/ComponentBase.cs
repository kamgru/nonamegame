using Game1.Entities;

namespace Game1.Components
{
    public abstract class ComponentBase
    {
        public bool Active { get; set; } = true;
        public Entity Entity { get; set; }
    }
}