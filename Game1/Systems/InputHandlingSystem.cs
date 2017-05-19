using Game1.Api;
using Game1.Data;
using Game1.Components;
using Game1.Managers;
using Game1.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Systems
{
    public class InputHandlingSystem : IUpdatingSystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IInputMappingService _inputMappingService;
        private readonly Point _tileSize;

        private readonly Dictionary<Intent, Vector2> _directionMap = new Dictionary<Intent, Vector2>
        {
            {Intent.MoveDown, new Vector2(0, 1) },
            {Intent.MoveUp, new Vector2(0, -1) },
            {Intent.MoveRight, new Vector2(1, 0) },
            {Intent.MoveLeft, new Vector2(-1, 0) }
        };

        public InputHandlingSystem(IEntityManager entityManager, IInputMappingService inputMappingService, IConfigurationService configurationService)
        {
            _entityManager = entityManager;
            _inputMappingService = inputMappingService;
            _tileSize = configurationService.GetTileSizeInPixels();
        }

        public void Update(GameTime gameTime)
        {
            var intents = _inputMappingService.GetIntents();

            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<IntentMap>() 
                && !x.HasComponent<TargetScreenPosition>()
                && x.HasComponent<BoardPosition>());

            foreach (var entity in entities)
            {
                var intent = entity.GetComponent<IntentMap>().Intent & intents;

                if (intent != 0)
                {
                    var requestedDirections = _directionMap.Where(x => intent.HasFlag(x.Key)).ToList();

                    if (requestedDirections.Count == 1)
                    {
                        var direction = requestedDirections.First().Value;
                        if (direction != Vector2.Zero)
                        {
                            entity.AddComponent(new TargetScreenPosition
                            {
                                Target = entity.Transform.Position + direction * new Vector2(_tileSize.X, _tileSize.Y)
                            });

                            var boardPosition = entity.GetComponent<BoardPosition>();
                            boardPosition.Translate(direction.ToPoint());

                            entity.GetComponent<Animator>().Play("walk");
                        }
                    }
                }
            }
        }
    }
}
