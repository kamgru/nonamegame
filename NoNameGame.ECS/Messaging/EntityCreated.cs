using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Messaging
{
    public class EntityCreated : IMessage
    {
        public Entity Entity { get; }

        public EntityCreated(Entity entity)
        {
            Entity = entity;
        }
    }
}
