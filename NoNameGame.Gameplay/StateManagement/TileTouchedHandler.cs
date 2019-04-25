using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Systems.StateHandling;
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

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var tile = entity as Tile;
            if (tile.State.InTransition)
            {
                switch (tile.TileInfo.TileType)
                {
                    case TileType.Double:
                        UpdateDouble(tile);
                        break;

                    case TileType.Triple:
                        UpdateTriple(tile);
                        break;

                    case TileType.Unknown:
                        UpdateUnknown(tile);
                        break;
                }

                tile.State.InTransition = false;
            }
        }

        private void UpdateDouble(Tile tile)
        {
            tile.Sprite.Rectangle = new Rectangle(32, 32, 32, 32);
        }

        private void UpdateTriple(Tile tile)
        {
            tile.Sprite.Rectangle = tile.TileInfo.Value == 3
                        ? new Rectangle(32, 64, 32, 32)
                        : new Rectangle(64, 64, 32, 32);
        }

        private void UpdateUnknown(Tile tile)
        {
            tile.Animator.Play(AnimationDictionary.TileTouch);
        }
    }
}
