﻿using Game1.Components;
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
    public class TileBasedMovementSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IConfigurationService _configurationService;

        public TileBasedMovementSystem(IEntityManager entityManager, IConfigurationService configurationService)
        {
            _entityManager = entityManager;
            _configurationService = configurationService;
        }

        public void Update()
        {
            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<MoveToTarget>() 
                && x.HasComponent<MoveSpeed>());

            var tileSize = _configurationService.GetTileSizeInPixels();

            foreach (var entity in entities)
            {
                var moveTo = entity.GetComponent<MoveToTarget>();

                var transform = entity.Transform;

                if ((transform.Position - moveTo.Target).LengthSquared() > 0.01f)
                {
                    var speed = entity.GetComponent<MoveSpeed>().Speed;

                    var direction = moveTo.Target - transform.Position;
                    direction.Normalize();

                    var distancePlanned = (direction * speed).LengthSquared();
                    var distanceLeft = (transform.Position - moveTo.Target).LengthSquared();

                    if (distancePlanned >= distanceLeft)
                    {
                        transform.Position = moveTo.Target;
                        entity.RemoveComponent(moveTo);
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
