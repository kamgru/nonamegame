using Game1.ECS.Api;
using Game1.ECS.Core;
using Game1.Gameplay.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Gameplay.Systems
{
    public class MoveToScreenPositionSystem : SystemBase, IUpdatingSystem
    {
        public MoveToScreenPositionSystem(IEntityManager entityManager)
            :base(entityManager)
        {
        }

        public void Update(GameTime gameTime)
        {
            var entities = EntityManager.GetEntities().Where(x => x.HasComponent<TargetScreenPosition>() 
                && x.HasComponent<MoveSpeed>());


            foreach (var entity in entities)
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
