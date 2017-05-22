using Game1.Api;
using Game1.Components;
using Game1.Data;

namespace Game1.Systems
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
                entityState.Entity.GetComponent<Animator>().Play("break");
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
