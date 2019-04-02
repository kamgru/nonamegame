using Microsoft.Xna.Framework;
using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Events;

namespace NoNameGame.Gameplay.Systems.CommandHandling
{
    public class MovePlayerCommand : ICommand
    {
        private Vector2 _direction;
        private Vector2 _distance;
        private Entity _player;

        public MovePlayerCommand(
            Vector2 direction, 
            Vector2 distance,
            Entity player)
        {
            _direction = direction;
            _distance = distance;
            _player = player;
        }

        public void Execute()
        {
            _player.GetComponent<PositionOnBoard>().Translate(_direction.ToPoint());
            _player.GetComponent<TargetScreenPosition>().Position += _direction * _distance;
            _player.GetComponent<State>().CurrentState = PlayerStates.Moving;

            GameEventManager.Raise(new PlayerAbandonedTile());
        }
    }
}
