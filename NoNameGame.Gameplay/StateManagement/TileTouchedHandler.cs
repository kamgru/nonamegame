using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;

namespace NoNameGame.Gameplay.StateManagement
{
    public class TileTouchedHandler : StateHandlerBase
    {
        public TileTouchedHandler() 
            : base(TileStates.Touched)
        {
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                var tileInfo = entityState.Entity.GetComponent<TileInfo>();
                var sprite = entityState.Entity.GetComponent<Sprite>();
                
                if (tileInfo.TileType == TileType.Double)
                {
                    sprite.Rectangle = new Rectangle(32, 32, 32, 32);
                    return;
                }
                
                if (tileInfo.TileType == TileType.Triple)
                {
                    sprite.Rectangle = tileInfo.Value == 2
                        ? new Rectangle(32, 64, 32, 32)
                        : new Rectangle(64, 64, 32, 32);
                    return;
                }
            }
        }
    }
}
