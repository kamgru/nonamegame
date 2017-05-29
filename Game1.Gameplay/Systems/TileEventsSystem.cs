using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Data;
using Game1.ECS.Api;
using Game1.ECS.Core;
using Game1.ECS.Components;
using Game1.Gameplay.Events;
using Game1.Core.Events;
using Game1.Gameplay.Components;

namespace Game1.Gameplay.Systems
{
    public class TileEventsSystem : SystemBase
    {
        public TileEventsSystem(IEntityManager entityManager, EventManager eventManager)
            : base(entityManager)
        {
            eventManager.On<PlayerAbandonedTile>(gameEvent =>
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

            eventManager.On<PlayerEnteredTile>(gameEvent =>
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
