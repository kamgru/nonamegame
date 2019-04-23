using Microsoft.Xna.Framework;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Input;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Commands;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.Systems
{
    public class PlayerInputHandlingSystem
        : SystemBase,
        IUpdatingSystem,
        IMessageListener<ComponentAdded<TileInfo>>,
        IMessageListener<EntityCreated>,
        IMessageListener<EntityDestroyed>
    {
        private readonly IntentProvider _intentProvider;
        private readonly Point _tileSize;
        private readonly List<Entity> _tileEntities = new List<Entity>();
        private Entity _playerEntity;

        public PlayerInputHandlingSystem(
            IntentProvider intentProvider,
            ConfigurationService configurationService)
        {
            _intentProvider = intentProvider;
            _tileSize = configurationService.GetTileSizeInPixels();
            SystemMessageBroker.AddListener<EntityCreated>(this);
            SystemMessageBroker.AddListener<ComponentAdded<TileInfo>>(this);
        }

        public void Handle(ComponentAdded<TileInfo> message)
        {
            _tileEntities.Add(message.Entity);
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<TileInfo>())
            {
                _tileEntities.Remove(message.Entity);
            }
        }

        public void Handle(EntityCreated message)
        {
            if (message.Entity is Player)
            {
                _playerEntity = message.Entity;
            }
        }

        public override void Reset()
        {
            _playerEntity = null;
            _tileEntities.Clear();
        }

        public void Update(GameTime gameTime)
        {
            var intents = _intentProvider.GetIntents();
            if (!intents.Any())
            {
                return;
            }

            var direction = MapIntentToDirection(intents.FirstOrDefault());
            if (direction == Vector2.Zero)
            {
                return;
            }

            _playerEntity.GetComponent<CommandQueue>()
                .Enqueue(new MovePlayerCommand(direction, _tileSize.ToVector2(), _playerEntity));
        }

        private Vector2 MapIntentToDirection(IIntent intent)
        {
            switch (intent)
            {
                case MovePlayerLeftIntent _: return new Vector2(-1, 0);
                case MovePlayerRightIntent _: return new Vector2(1, 0);
                case MovePlayerUpIntent _: return new Vector2(0, -1);
                case MovePlayerDownIntent _: return new Vector2(0, 1);
                default: return Vector2.Zero;
            }
        }
    }
}
