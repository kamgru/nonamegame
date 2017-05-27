using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Events;
using Game1.Components;
using Game1.Data;

namespace Game1.Systems
{
    public class TileEventsSystem : SystemBase
    {
        public TileEventsSystem(IEntityManager entityManager, EventManager eventManager)
            : base(entityManager)
        {
            eventManager.RegisterListener<PlayerAbandonedTile>(gameEvent =>
            {
                if (gameEvent.TileInfo.TileType == TileType.Normal)
                {
                    gameEvent.TileInfo.Value--;
                    if (gameEvent.TileInfo.Value <= 0)
                    {
                        var state = gameEvent.TileInfo.Entity.GetComponent<State>();
                        if (state != null)
                        {
                            state.CurrentState = TileStates.Destroyed;
                        }
                    }
                }
            });

            eventManager.RegisterListener<PlayerEnteredTile>(gameEvent =>
            {
                if (gameEvent.TileInfo.TileType == TileType.End)
                {
                    var tiles = EntityManager.GetEntities()
                    .Where(x => x.HasComponent<TileInfo>())
                    .Select(x => x.GetComponent<TileInfo>());

                    if (tiles.Where(x => x.TileType == TileType.Normal).All(x => x.Destroyed))
                    {
                        eventManager.Queue(new StageClear());
                    }
                }
            });
        }
    }
}
