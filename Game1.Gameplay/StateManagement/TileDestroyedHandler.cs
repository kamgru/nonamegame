using Game1.Data;
using Game1.ECS;
using Game1.ECS.Api;
using Game1.ECS.Core;
using Game1.ECS.Components;
using Game1.Gameplay.Components;

namespace Game1.Gameplay.StateManagement
{
    public class TileDestroyedHandler : StateHandlerBase
    {
        private readonly IEntityManager _entityManager;

        public TileDestroyedHandler(IEntityManager entityManager) 
            : base(TileStates.Destroyed)
        {
            _entityManager = entityManager;
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
                _entityManager.DestroyEntity(entityState.Entity);
            }
        }
    }
}
