using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Game1.Managers;

namespace Game1.Systems
{
    public class MoveToNewTileSystem : SystemBase, IUpdatingSystem
    {
        public MoveToNewTileSystem(IEntityManager entityManager, SystemsManager systemsManager)
            :base(entityManager, systemsManager)
        {
        }

        public void Update(GameTime gameTime)
        {
            var entity = EntityManager.GetEntitiesByComponent<MovedToNewTile>().SingleOrDefault();
            if (entity == null)
            {
                return;
            }

            var movedToNewTile = entity.GetComponent<MovedToNewTile>();

            var boardPosition = entity.GetComponent<PositionOnBoard>();

            var tiles = EntityManager.GetEntitiesByComponent<TileInfo>()
                .Select(x => x.GetComponent<TileInfo>())
                .ToList();

            var currentTile = tiles.SingleOrDefault(x => x.Position == boardPosition.Current);

            if (currentTile == null || currentTile.Destroyed)
            {
                EntityManager.DestroyEntity(entity);
            }

            tiles.SingleOrDefault(x => x.Position == boardPosition.Previous && !x.Destroyed)
                ?.Entity.AddComponent(new TileAbandoned());

            entity.RemoveComponent(movedToNewTile);
        }
    }
}
