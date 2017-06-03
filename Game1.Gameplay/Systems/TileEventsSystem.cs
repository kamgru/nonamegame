using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.Data;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Events;
using NoNameGame.Core.Events;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Factories;

namespace NoNameGame.Gameplay.Systems
{
    public class TileEventsSystem : SystemBase
    {
        private readonly Entity _poof;

        public TileEventsSystem(IEntityManager entityManager, EventManager eventManager, PoofFactory poofFactory)
            : base(entityManager)
        {

            _poof = poofFactory.CreatePoof();

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
                
                _poof.Transform.Position = gameEvent.TileEntity.Transform.Position;
                _poof.GetComponent<Animator>().Play("poof");

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
