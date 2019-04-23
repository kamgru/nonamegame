using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

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
            var tile = entityState.Entity as Tile;
            if (tile.State.InTransition)
            {
                if (tile.TileInfo.TileType == TileType.Double)
                {
                    tile.Sprite.Rectangle = new Rectangle(32, 32, 32, 32);
                    return;
                }
                
                if (tile.TileInfo.TileType == TileType.Triple)
                {
                    tile.Sprite.Rectangle = tile.TileInfo.Value == 2
                        ? new Rectangle(32, 64, 32, 32)
                        : new Rectangle(64, 64, 32, 32);
                    return;
                }
            }
        }
    }
}
