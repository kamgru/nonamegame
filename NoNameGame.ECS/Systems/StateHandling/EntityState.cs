using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Systems.StateHandling
{
    public class EntityState
    {
        public State State { get; set; }
        public Entity Entity { get; set; }
    }
}
