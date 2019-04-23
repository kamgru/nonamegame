using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.CommandHandling;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using NoNameGame.Gameplay.Events;

namespace NoNameGame.Gameplay.Commands
{
    public class MovePlayerCommand : ICommand
    {
        private Vector2 _direction;
        private Vector2 _distance;
        private Player _player;

        public MovePlayerCommand(
            Vector2 direction,
            Vector2 distance,
            Player player)
        {
            _direction = direction;
            _distance = distance;
            _player = player;
        }

        public void Execute()
        {
            _player.PositionOnBoard.Translate(_direction.ToPoint());

            var target = new TargetScreenPosition
            {
                Duration = 300f,
                Start = _player.Transform.Position
            };

            target.Target = target.Start + _direction * _distance;

            _player.AddComponent(target);

            _player.State.CurrentState = PlayerStates.Moving;

            GameEventManager.Raise(new PlayerAbandonedTile());
        }
    }
}
