using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Game1.Systems
{
    public class MoveToNewTileSystem : ISystem
    {
        private readonly IEntityManager _entityManager;

        public MoveToNewTileSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public void Update(GameTime gameTime)
        {
            var entity = _entityManager.GetEntitiesByComponent<MovedToNewTile>().SingleOrDefault();
            if (entity == null)
            {
                return;
            }

            var movedToNewTile = entity.GetComponent<MovedToNewTile>();

            var boardPosition = entity.GetComponent<BoardPosition>();

            //var boardInfo = _entityManager.GetEntitiesByComponent<BoardInfo>().Single().GetComponent<BoardInfo>();
            var tiles = _entityManager.GetEntitiesByComponent<TileInfo>().Select(x => x.GetComponent<TileInfo>());
            //var currentTile = boardInfo?.GetTileAt(boardPosition.Current);
            var currentTile = tiles.SingleOrDefault(x => x.Position == boardPosition.Current);

            if (currentTile == null || currentTile.Destroyed)
            {
                _entityManager.DestroyEntity(entity);
            }

            var previousTile = tiles.SingleOrDefault(x => x.Position == boardPosition.Previous && !x.Destroyed);
            if (previousTile != null)
            {
                previousTile.Value--;
                if (previousTile.Value <= 0)
                {
                    //boardInfo.RemoveTileAt(previousTile.Position);
                    //_entityManager.DestroyEntity(previousTile.Entity);
                    
                    previousTile.Entity.GetComponent<Animator>().Play("break");
                    previousTile.Destroyed = true;
                }
            }

            entity.RemoveComponent(movedToNewTile);
        }
    }
}
