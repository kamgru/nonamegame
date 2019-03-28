using NoNameGame.Data;
using System.Linq;
using NoNameGame.ECS.Components;
using NoNameGame.Core.Services;
using NoNameGame.Core.Events;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Events;
using NoNameGame.ECS.Messaging;
using System.Collections.Generic;
using NoNameGame.ECS.StateHandling;
using NoNameGame.ECS.Entities;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerMovingHandler 
        : StateHandlerBase,
        IMessageListener<EntityCreated>,
        IMessageListener<EntityDestroyed>
    {
        private readonly InputService _inputService;
        private readonly EventManager _eventManager;
        private readonly ICollection<Entity> _entities;

        public PlayerMovingHandler(InputService inputService, EventManager eventManager)
            : base(PlayerStates.Moving)
        {
            _inputService = inputService;
            _eventManager = eventManager;
            _entities = new List<Entity>();
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<EntityDestroyed>(this);
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
                    var currentTile = _entities.Where(x => x.HasComponent<TileInfo>())
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

        public void Handle(EntityCreated message)
        {
            _entities.Add(message.Entity);
        }

        public void Handle(EntityDestroyed message)
        {
            _entities.Remove(message.Entity);
        }
    }
}
