﻿using Game1.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game1.Input;
using Game1.Data;
using Game1.Services;
using Game1.Components;
using Game1.Managers;

namespace Game1.Systems
{
    public class PlayerInputHandlingSystem : SystemBase, IUpdatingSystem
    {
        private readonly Dictionary<Intent, Vector2> _directionMap = new Dictionary<Intent, Vector2>
        {
            {Intent.MoveDown, new Vector2(0, 1) },
            {Intent.MoveUp, new Vector2(0, -1) },
            {Intent.MoveRight, new Vector2(1, 0) },
            {Intent.MoveLeft, new Vector2(-1, 0) }
        };

        private readonly InputService _inputService;
        private readonly Point _tileSize;

        public PlayerInputHandlingSystem(IEntityManager entityManager, InputService inputService, IConfigurationService configurationService) 
            : base(entityManager)
        {
            _inputService = inputService;
            _tileSize = configurationService.GetTileSizeInPixels();
        }

        public void Update(GameTime gameTime)
        {
            var intents = _inputService.ConsumeIntents(_directionMap.Keys);

            var requestedDirections = _directionMap.Where(x => intents.Contains(x.Key)).Select(x => x.Value);

            if (requestedDirections.Count() == 1)
            {
                var player = EntityManager.GetEntities()
                    .Where(x => x.HasComponent<Player>())
                    .Where(x => x.HasComponent<PositionOnBoard>())
                    .Single();

                var direction = requestedDirections.First();
                if (direction != Vector2.Zero)
                {
                    var boardPosition = player.GetComponent<PositionOnBoard>();

                    var tile = EntityManager.GetEntities()
                        .Where(x => x.HasComponent<TileInfo>())
                        .First(x => x.GetComponent<TileInfo>().Position == boardPosition.Current);
                    tile.GetComponent<State>().CurrentState = TileStates.Abandoned;

                    boardPosition.Translate(direction.ToPoint());

                    player.GetComponent<TargetScreenPosition>().Position += direction * _tileSize.ToVector2();

                    player.GetComponent<State>().CurrentState = PlayerStates.Moving;
                }
            }
        }
    }
}