using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

namespace NoNameGame.Gameplay.StateManagement
{
    public class TileDestroyedHandler : StateHandlerBase
    {
        public TileDestroyedHandler()
            : base(TileStates.Destroyed)
        {
        }

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var tile = entity as Tile;
            if (tile.State.InTransition)
            {
                tile.Animator.Play(AnimationDictionary.TileDestroy);
                tile.State.InTransition = false;
            }
            else if (!tile.Animator.IsPlaying)
            {
                Entity.Destroy(tile);
            }
        }
    }
}
