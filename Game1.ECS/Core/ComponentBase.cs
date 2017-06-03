namespace NoNameGame.ECS.Core
{
    public abstract class ComponentBase
    {
        public bool Active { get; set; } = true;
        public Entity Entity { get; set; }
    }
}