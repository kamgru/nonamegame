using NoNameGame.Data;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.StateHandling;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerDeadHandler : StateHandlerBase
    {
        public PlayerDeadHandler()
            :base(PlayerStates.Dead)
        {
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                Entity.Destroy(entityState.Entity);
            }
        }
    }
}
