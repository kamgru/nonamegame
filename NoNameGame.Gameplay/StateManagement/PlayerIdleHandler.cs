using NoNameGame.Core.Services;
using NoNameGame.Data;
using NoNameGame.ECS.Systems.StateHandling;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerIdleHandler : StateHandlerBase
    {
        private readonly InputService _inputService;

        public PlayerIdleHandler(InputService inputService) 
            : base(PlayerStates.Idle)
        {
            _inputService = inputService;
        }

        public override void Handle(EntityState entity)
        {
            if (entity.State.InTransition)
            {
                _inputService.SetContextActive((int)Context.Gameplay, true);
                entity.State.InTransition = false;
            }
        }
    }
}
