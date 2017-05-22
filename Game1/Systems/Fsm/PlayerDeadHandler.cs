using Game1.Api;
using Game1.Data;

namespace Game1.Systems
{
    public class PlayerDeadHandler : StateHandlerBase
    {
        private readonly IEntityManager _entityManager;

        public PlayerDeadHandler(IEntityManager entityManager)
            :base(PlayerStates.Dead)
        {
            _entityManager = entityManager;
        }

        public override void Handle(EntityState entity)
        {
            if (entity.State.InTransition)
            {
                _entityManager.DestroyEntity(entity.Entity);
            }
        }
    }
}
