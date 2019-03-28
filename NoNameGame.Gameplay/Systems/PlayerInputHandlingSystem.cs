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
using NoNameGame.ECS.Entities;
using System;

namespace NoNameGame.Gameplay.Systems
{
    public class PlayerInputHandlingSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<Player>>,
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
        private readonly List<Entity> _tileEntities = new List<Entity>();
        private Entity _playerEntity;

        public PlayerInputHandlingSystem(
            InputService inputService, 
            ConfigurationService configurationService, 
            EventManager eventManager) 
        {
            _inputService = inputService;
            _eventManager = eventManager;
            _tileSize = configurationService.GetTileSizeInPixels();
            SystemMessageBroker.AddListener<ComponentAdded<Player>>(this);
            SystemMessageBroker.AddListener<ComponentAdded<TileInfo>>(this);
        }

        public void Handle(ComponentAdded<TileInfo> message)
        {
            _tileEntities.Add(message.Entity);
        }

        public void Handle(ComponentAdded<Player> message)
        {
            _playerEntity = message.Entity;
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<TileInfo>())
            {
                _tileEntities.Remove(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            var intents = _inputService.ConsumeIntents(_directionMap.Keys);

            var requestedDirections = _directionMap.Where(x => intents.Contains(x.Key)).Select(x => x.Value);

            if (requestedDirections.Count() == 1)
            {
                var direction = requestedDirections.First();
                if (direction != Vector2.Zero)
                {
                    var occupiedTile = FindOccupiedTile();
                    MovePlayer(direction, _tileSize.ToVector2());

                    _eventManager.Queue(new PlayerAbandonedTile
                    {
                        TileInfo = occupiedTile.GetComponent<TileInfo>(),
                        State = occupiedTile.GetComponent<State>(),
                        TileEntity = occupiedTile
                    });
                }
            }
        }

        private void MovePlayer(Vector2 direction, Vector2 distance)
        {
            _playerEntity.GetComponent<PositionOnBoard>().Translate(direction.ToPoint());
            _playerEntity.GetComponent<TargetScreenPosition>().Position += direction * distance;
            _playerEntity.GetComponent<State>().CurrentState = PlayerStates.Moving;
        }

        private Entity FindOccupiedTile()
            => _tileEntities.First(
                x => x.GetComponent<TileInfo>().Position == _playerEntity.GetComponent<PositionOnBoard>().Current);
    }
}
