using NoNameGame.ECS.Components;
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

        public override void Handle(EntityState entityState)
        {
            var tile = entityState.Entity as Tile;
            if (entityState.State.InTransition)
            {
                tile.Animator.Play(AnimationDictionary.TileDestroy);
                tile.State.InTransition = false;
            }
            else if (!tile.Animator.IsPlaying)
            {
                Entity.Destroy(entityState.Entity);
            }
        }
    }
}
