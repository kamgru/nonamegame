using Microsoft.Xna.Framework;
using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Events;
using System.Linq;

namespace NoNameGame.Gameplay.Systems.CommandHandling
{
    public class MovePlayerCommandHandler
    {
        public void Handle(MovePlayerCommand command)
        {
            if (command.Direction != Vector2.Zero)
            {
                MovePlayer(command.Direction, command.Distance, command.Player);
                GameEventManager.Raise(new PlayerAbandonedTile());
            }
        }

        private void MovePlayer(Vector2 direction, Vector2 distance, Entity player)
        {
            player.GetComponent<PositionOnBoard>().Translate(direction.ToPoint());
            player.GetComponent<TargetScreenPosition>().Position += direction * distance;
            player.GetComponent<State>().CurrentState = PlayerStates.Moving;
        }
    }
}
