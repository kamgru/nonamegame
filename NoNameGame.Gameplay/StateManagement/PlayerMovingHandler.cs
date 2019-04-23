using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Input;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Events;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerMovingHandler
        : StateHandlerBase,
        IMessageListener<EntityCreated>,
        IMessageListener<EntityDestroyed>
    {
        private readonly IInputMapProvider _inputMapProvider;
        private readonly ICollection<Entity> _entities;

        public PlayerMovingHandler(IInputMapProvider inputMapProvider)
            : base(PlayerStates.Moving)
        {
            _inputMapProvider = inputMapProvider;
            _entities = new List<Entity>();
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<EntityDestroyed>(this);
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                _inputMapProvider.GetContextById(Contexts.Gameplay)?.Deactivate();
                entityState.Entity.GetComponent<Animator>().Play(AnimationDictionary.PlayerMove);
                entityState.State.InTransition = false;
            }
            else
            {
                if (entityState.Entity.Transform.Position == entityState.Entity.GetComponent<TargetScreenPosition>().Target)
                {
                    var currentPosition = entityState.Entity.GetComponent<PositionOnBoard>().Current;
                    var currentTile = _entities.Where(x => x.HasComponent<TileInfo>())
                        .Select(x => new { TileInfo = x.GetComponent<TileInfo>(), Entity = x })
                        .FirstOrDefault(x => x.TileInfo.Position == currentPosition);

                    if (currentTile == null || currentTile.Entity.GetComponent<State>().CurrentState == TileStates.Destroyed)
                    {
                        entityState.State.CurrentState = PlayerStates.Dead;
                    }
                    else
                    {
                        entityState.State.CurrentState = PlayerStates.Idle;
                        entityState.Entity.RemoveComponent(entityState.Entity.GetComponent<TargetScreenPosition>());

                        GameEventManager.Raise(
                            new PlayerEnteredTile(currentTile.TileInfo, currentTile.Entity, entityState.Entity.GetComponent<PositionOnBoard>()));
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
