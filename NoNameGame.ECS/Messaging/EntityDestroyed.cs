using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Messaging
{
    public class EntityDestroyed : IMessage
    {
        public Entity Entity { get; }

        public EntityDestroyed(Entity entity)
        {
            Entity = entity;
        }
    }
}
