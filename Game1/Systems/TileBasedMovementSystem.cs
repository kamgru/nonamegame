using Game1.Components;
using Game1.Managers;
using Game1.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Systems
{
    public class MovementSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IConfigurationService _configurationService;

        public MovementSystem(IEntityManager entityManager, IConfigurationService configurationService)
        {
            _entityManager = entityManager;
            _configurationService = configurationService;
        }

        public void Update()
        {
            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<MoveToComponent>() && x.HasComponent<TransformComponent>());
            var tileSize = _configurationService.GetTileSizeInPixels();

            foreach (var entity in entities)
            {
                var moveTo = entity.GetComponent<MoveToComponent>();

                if (moveTo.Active)
                {
                    var transform = entity.GetComponent<TransformComponent>();

                    if ((transform.Position - moveTo.Target).LengthSquared() > 0.01f)
                    {
                        var direction = moveTo.Target - transform.Position;
                        direction.Normalize();

                        var distancePlanned = (direction * moveTo.Speed).LengthSquared();
                        var distanceLeft = (transform.Position - moveTo.Target).LengthSquared();

                        if (distancePlanned >= distanceLeft)
                        {
                            transform.Position = moveTo.Target;
                            moveTo.Active = false;
                        }
                        else
                        {
                            transform.Position += direction * moveTo.Speed;
                        }
                    }
                }                
            }
        }
    }
}
