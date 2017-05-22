using Game1.Api;
using System.Linq;
using Game1.Components;
using Game1.Data;
using Game1.Services;

namespace Game1.Systems
{
    public class PlayerMovingHandler : StateHandlerBase
    {
        private readonly InputService _inputService;
        private readonly IEntityManager _entityManager;

        public PlayerMovingHandler(InputService inputService, IEntityManager entityManager)
            : base(PlayerStates.Moving)
        {
            _inputService = inputService;
            _entityManager = entityManager;
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                _inputService.SetContextActive((int)Context.Gameplay, false);
                entityState.Entity.GetComponent<Animator>().Play("walk");
                entityState.State.InTransition = false;
            }
            else
            {
                if (entityState.Entity.Transform.Position == entityState.Entity.GetComponent<TargetScreenPosition>().Position)
                {
                    var currentPosition = entityState.Entity.GetComponent<PositionOnBoard>().Current;
                    var currentTile = _entityManager.GetEntities()
                        .Where(x => x.HasComponent<TileInfo>())
                        .Select(x => new { TileInfo = x.GetComponent<TileInfo>() })
                        .FirstOrDefault(x => x.TileInfo.Position == currentPosition);

                    if (currentTile == null || currentTile.TileInfo.Destroyed)
                    {
                        entityState.State.CurrentState = PlayerStates.Dead;
                    }
                    else
                    {
                        entityState.State.CurrentState = PlayerStates.Idle;
                    }
                }
            }
        }
    }
}
