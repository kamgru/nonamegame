using Game1.Data;
using Game1.Services;

namespace Game1.Systems
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
