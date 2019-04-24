using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Input;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
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
        private readonly List<Tile> _tiles = new List<Tile>();

        public PlayerMovingHandler(IInputMapProvider inputMapProvider)
            : base(PlayerStates.Moving)
        {
            _inputMapProvider = inputMapProvider;
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<EntityDestroyed>(this);
        }

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var player = entity as Player;
            if (player.State.InTransition)
            {
                _inputMapProvider.GetContextById(Contexts.Gameplay)?.Deactivate();

                player.Animator.Play(AnimationDictionary.PlayerMove);
                player.State.InTransition = false;
            }
            else
            {
                if (player.Transform.Position == player.GetComponent<TargetScreenPosition>().Target)
                {
                    var currentPosition = player.PositionOnBoard.Current;
                    var currentTile = _tiles.FirstOrDefault(x => x.TileInfo.Position == currentPosition);

                    if (currentTile == null || currentTile.State.CurrentState == TileStates.Destroyed)
                    {
                        player.State.CurrentState = PlayerStates.Dead;
                    }
                    else
                    {
                        player.State.CurrentState = PlayerStates.Idle;
                        player.RemoveComponent(player.GetComponent<TargetScreenPosition>());

                        GameEventManager.Raise(new PlayerEnteredTile(currentTile));
                    }
                }
            }
        }

        public void Handle(EntityCreated message)
        {
            if (message.Entity is Tile tile)
            {
                _tiles.Add(tile);
            }
        }

        public void Handle(EntityDestroyed message)
        {
            if (message.Entity is Tile tile)
            {
                _tiles.Remove(tile);
            }
        }
    }
}
