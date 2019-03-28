using NoNameGame.Data;
using NoNameGame.ECS;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;

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
