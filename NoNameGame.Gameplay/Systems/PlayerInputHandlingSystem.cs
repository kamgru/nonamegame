using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NoNameGame.Data;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Components;
using NoNameGame.Core.Services;
using NoNameGame.Core.Events;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Events;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;

namespace NoNameGame.Gameplay.Systems
{
    public class PlayerInputHandlingSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<Player>>,
        IMessageListener<ComponentAdded<PositionOnBoard>>,
        IMessageListener<ComponentAdded<TileInfo>>
    {
        private readonly Dictionary<Intent, Vector2> _directionMap = new Dictionary<Intent, Vector2>
        {
            {Intent.MoveDown, new Vector2(0, 1) },
            {Intent.MoveUp, new Vector2(0, -1) },
            {Intent.MoveRight, new Vector2(1, 0) },
            {Intent.MoveLeft, new Vector2(-1, 0) }
        };

        private readonly InputService _inputService;
        private readonly EventManager _eventManager;
        private readonly Point _tileSize;

        public PlayerInputHandlingSystem(
            InputService inputService, 
            ConfigurationService configurationService, 
            EventManager eventManager) 
        {
            _inputService = inputService;
            _eventManager = eventManager;
            _tileSize = configurationService.GetTileSizeInPixels();
            SystemMessageBroker.AddListener<ComponentAdded<Player>>(this);
            SystemMessageBroker.AddListener<ComponentAdded<PositionOnBoard>>(this);
            SystemMessageBroker.AddListener<ComponentAdded<TileInfo>>(this);
        }

        public void Handle(ComponentAdded<TileInfo> message)
        {
            Entities.Add(message.Entity);
        }

        public void Handle(ComponentAdded<PositionOnBoard> message)
        {
            if (message.Entity.HasComponent<Player>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Handle(ComponentAdded<Player> message)
        {
            if (message.Entity.HasComponent<PositionOnBoard>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            var intents = _inputService.ConsumeIntents(_directionMap.Keys);

            var requestedDirections = _directionMap.Where(x => intents.Contains(x.Key)).Select(x => x.Value);

            if (requestedDirections.Count() == 1)
            {
                var player = Entities
                    .Single(x => x.HasComponent<PositionOnBoard>() && x.HasComponent<Player>());

                var direction = requestedDirections.First();
                if (direction != Vector2.Zero)
                {
                    var boardPosition = player.GetComponent<PositionOnBoard>();

                    var tile = Entities.Where(x => x.HasComponent<TileInfo>())
                        .First(x => x.GetComponent<TileInfo>().Position == boardPosition.Current);

                    boardPosition.Translate(direction.ToPoint());

                    player.GetComponent<TargetScreenPosition>().Position += direction * _tileSize.ToVector2();
                    player.GetComponent<State>().CurrentState = PlayerStates.Moving;

                    _eventManager.Queue(new PlayerAbandonedTile
                    {
                        TileInfo = tile.GetComponent<TileInfo>(),
                        State = tile.GetComponent<State>(),
                        TileEntity = tile
                    });
                }
            }
        }
    }
}
