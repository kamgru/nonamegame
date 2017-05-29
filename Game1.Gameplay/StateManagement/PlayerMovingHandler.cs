using Game1.Data;
using Game1.ECS;
using Game1.ECS.Api;
using Game1.ECS.Core;
using System.Linq;
using Game1.ECS.Components;
using Game1.Core.Services;
using Game1.Core.Events;
using Game1.Gameplay.Components;
using Game1.Gameplay.Events;

namespace Game1.Gameplay.StateManagement
{
    public class PlayerMovingHandler : StateHandlerBase
    {
        private readonly InputService _inputService;
        private readonly IEntityManager _entityManager;
        private readonly EventManager _eventManager;

        public PlayerMovingHandler(InputService inputService, IEntityManager entityManager, EventManager eventManager)
            : base(PlayerStates.Moving)
        {
            _inputService = inputService;
            _entityManager = entityManager;
            _eventManager = eventManager;
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
                        .Select(x => new { TileInfo = x.GetComponent<TileInfo>(), Entity = x })
                        .FirstOrDefault(x => x.TileInfo.Position == currentPosition);

                    if (currentTile == null || currentTile.TileInfo.Destroyed)
                    {
                        entityState.State.CurrentState = PlayerStates.Dead;
                    }
                    else
                    {
                        entityState.State.CurrentState = PlayerStates.Idle;

                        _eventManager.Queue(new PlayerEnteredTile
                        {
                            TileEntity = currentTile.Entity,
                            TileInfo = currentTile.TileInfo
                        });
                    }
                }
            }
        }
    }
}
