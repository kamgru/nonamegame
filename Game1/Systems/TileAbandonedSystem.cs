﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Microsoft.Xna.Framework;
using Game1.Managers;

namespace Game1.Systems
{
    public class TileAbandonedSystem : SystemBase, IUpdatingSystem
    {
        private readonly IEntityManager _entityManager;

        public TileAbandonedSystem(IEntityManager entityManager, SystemsManager systemsManager)
            :base(entityManager, systemsManager)
        {
            _entityManager = entityManager;
        }

        public void Update(GameTime gameTime)
        {
            var tiles = _entityManager.GetEntitiesByComponent<TileAbandoned>()
                .Select(x => new
                {
                    TileInfo = x.GetComponent<TileInfo>(),
                    Animator = x.GetComponent<Animator>(),
                    TileAbandoned = x.GetComponent<TileAbandoned>(),
                    Entity = x
                })
                .ToList();

            foreach (var tile in tiles)
            {
                tile.TileInfo.Value--;
                if (tile.TileInfo.Value <= 0)
                {
                    tile.Animator.Play("break");
                    tile.TileInfo.Destroyed = true;
                    tile.Animator.OnAnimationEnded = () => _entityManager.DestroyEntity(tile.Entity);
                }
                tile.Entity.RemoveComponent(tile.TileAbandoned);
            }
        }
    }
}
