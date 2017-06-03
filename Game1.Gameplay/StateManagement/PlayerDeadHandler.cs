using NoNameGame.Data;
using NoNameGame.ECS;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;

namespace NoNameGame.Gameplay.StateManagement
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
