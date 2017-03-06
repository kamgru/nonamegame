using Game1.Api;
using Game1.Common;
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
    public class InputHandlingSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly IInputMappingService _inputMappingService;
        private readonly Point _tileSize;

        public InputHandlingSystem(IEntityManager entityManager, IInputMappingService inputMappingService, IConfigurationService configurationService)
        {
            _entityManager = entityManager;
            _inputMappingService = inputMappingService;
            _tileSize = configurationService.GetTileSizeInPixels();
        }

        public void Update()
        {
            var intents = _inputMappingService.GetIntents();

            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<IntentMap>() 
                && !x.HasComponent<TargetScreenPosition>()
                && x.HasComponent<BoardPosition>());

            foreach (var entity in entities)
            {
                var intent = entity.GetComponent<IntentMap>().Intent & intents;
                var direction = Vector2.Zero;

                if (intent == Intent.MoveDown)
                {
                    direction += new Vector2(0, 1);
                }

                if (intent == Intent.MoveUp)
                {
                    direction += new Vector2(0, -1);
                }

                if (intent == Intent.MoveLeft)
                {
                    direction += new Vector2(-1, 0);
                }

                if (intent == Intent.MoveRight)
                {
                    direction += new Vector2(1, 0);
                }

                if (direction != Vector2.Zero)
                {
                    entity.AddComponent(new TargetScreenPosition
                    {
                        Target = entity.Transform.Position + direction * new Vector2(_tileSize.X, _tileSize.Y)
                    });

                    var boardPosition = entity.GetComponent<BoardPosition>();
                    boardPosition.Translate(direction.ToPoint());
                }
            }
        }
    }
}
