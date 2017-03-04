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
            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<MovementComponent>() && x.HasComponent<TransformComponent>());
            var tileSize = _configurationService.GetTileSizeInPixels();

            foreach (var entity in entities)
            {
                var movement = entity.GetComponent<MovementComponent>();
                var velocity = movement.Velocity;

                var transform = entity.GetComponent<TransformComponent>();
                transform.Position += new Vector2(velocity.X * tileSize.X, velocity.Y * tileSize.Y);

                movement.Velocity = Vector2.Zero;
            }
        }
    }
}
