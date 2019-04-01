using Microsoft.Xna.Framework;
using NoNameGame.Core.Events;
using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Events;
using System.Linq;

namespace NoNameGame.Gameplay.Systems.CommandHandling
{
    public class MovePlayerCommandHandler
    {
        private readonly EventManager _eventManager;
        private readonly EntityRepository _entityRepository;

        public MovePlayerCommandHandler(EventManager eventManager, EntityRepository entityRepository)
        {
            _eventManager = eventManager;
            _entityRepository = entityRepository;
        }

        public void Handle(MovePlayerCommand command)
        {
            if (command.Direction != Vector2.Zero)
            {
                var occupiedTile = _entityRepository.FindByComponent<TileInfo>().First(
                    x => x.GetComponent<TileInfo>().Position == command.Player.GetComponent<PositionOnBoard>().Current);

                MovePlayer(command.Direction, command.Distance, command.Player);

                _eventManager.Queue(new PlayerAbandonedTile
                {
                    TileInfo = occupiedTile.GetComponent<TileInfo>(),
                    State = occupiedTile.GetComponent<State>(),
                    TileEntity = occupiedTile
                });
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
