using Game1.Core.Services;
using Game1.Data;
using Game1.ECS;
using Game1.ECS.Api;
using Game1.ECS.Core;

namespace Game1.Gameplay.StateManagement
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
