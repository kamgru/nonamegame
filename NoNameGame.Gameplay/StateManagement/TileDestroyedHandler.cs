using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.StateHandling;
using NoNameGame.Gameplay.Components;

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
            if (entityState.State.InTransition)
            {
                entityState.Entity.GetComponent<Animator>().Play(AnimationDictionary.TileDestroy);
                entityState.Entity.GetComponent<TileInfo>().Destroyed = true;
                entityState.State.InTransition = false;
            }
            else if (!entityState.Entity.GetComponent<Animator>().IsPlaying)
            {
                Entity.Destroy(entityState.Entity);
            }
        }
    }
}
