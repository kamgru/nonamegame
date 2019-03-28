using NoNameGame.ECS.Api;
using NoNameGame.Gameplay.Components;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;

namespace NoNameGame.Gameplay.Systems
{
    public class MoveToScreenPositionSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<TargetScreenPosition>>,
        IMessageListener<ComponentAdded<MoveSpeed>>
    {
        public MoveToScreenPositionSystem()
        {
            SystemMessageBroker.AddListener<ComponentAdded<TargetScreenPosition>>(this);
            SystemMessageBroker.AddListener<ComponentAdded<MoveSpeed>>(this);
        }

        public void Handle(ComponentAdded<MoveSpeed> message)
        {
            if (message.Entity.HasComponent<TargetScreenPosition>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Handle(ComponentAdded<TargetScreenPosition> message)
        {
            if (message.Entity.HasComponent<MoveSpeed>())
            {
                Entities.Add(message.Entity);
            }
        }

        public override void Handle(EntityCreated message)
        {
            if (message.Entity.HasComponent<MoveSpeed>()
                && message.Entity.HasComponent<TargetScreenPosition>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                var moveTo = entity.GetComponent<TargetScreenPosition>();

                var transform = entity.Transform;

                if ((transform.Position - moveTo.Position).LengthSquared() > 0.01f)
                {
                    var speed = entity.GetComponent<MoveSpeed>().Speed;

                    var direction = moveTo.Position - transform.Position;
                    direction.Normalize();

                    var distancePlanned = (direction * speed).LengthSquared();
                    var distanceLeft = (transform.Position - moveTo.Position).LengthSquared();

                    if (distancePlanned >= distanceLeft)
                    {
                        transform.Position = moveTo.Position;
                    }
                    else
                    {
                        transform.Position += direction * speed;
                    }
                }                
            }
        }
    }
}
