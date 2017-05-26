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
                gameEvent.TileInfo.Value--;
                if (gameEvent.TileInfo.Value <= 0)
                {
                    var state = gameEvent.TileInfo.Entity.GetComponent<State>();
                    if (state != null)
                    {
                        state.CurrentState = TileStates.Destroyed;
                    }
                }
            });
        }
    }
}
