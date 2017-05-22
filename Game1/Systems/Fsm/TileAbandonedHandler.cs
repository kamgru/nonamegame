using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Components;
using Microsoft.Xna.Framework;
using Game1.Managers;
using Game1.Data;

namespace Game1.Systems
{
    public class TileAbandonedHandler : StateHandlerBase
    {
        public TileAbandonedHandler() 
            : base(TileStates.Abandoned)
        {
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                entityState.State.InTransition = false;

                var tileInfo = entityState.Entity.GetComponent<TileInfo>();
                tileInfo.Value--;

                if (tileInfo.Value <= 0)
                {
                    entityState.State.CurrentState = TileStates.Destroyed;
                }
            }
        }
    }
}
