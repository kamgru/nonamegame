using Game1.Data;
using Game1.ECS;
using Game1.ECS.Api;
using Game1.ECS.Core;

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
