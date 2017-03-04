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
        private readonly IConfigurationService _configurationService;
        private readonly Point _tileSize;

        public InputHandlingSystem(IEntityManager entityManager, IInputMappingService inputMappingService, IConfigurationService configurationService)
        {
            _entityManager = entityManager;
            _inputMappingService = inputMappingService;
            _configurationService = configurationService;
            _tileSize = _configurationService.GetTileSizeInPixels();
        }

        public void Update()
        {
            var intent = _inputMappingService.GetIntents();

            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<IntentMapComponent>() && x.HasComponent<MoveToComponent>());

            foreach (var entity in entities)
            {
                var intentComponent = entity.GetComponent<IntentMapComponent>();
                var direction = Vector2.Zero;

                if ((intentComponent.Intent & intent) == Intent.MoveDown)
                {
                    direction += new Vector2(0, 1);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveUp)
                {
                    direction += new Vector2(0, -1);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveLeft)
                {
                    direction += new Vector2(-1, 0);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveRight)
                {
                    direction += new Vector2(1, 0);
                }

                if (direction != Vector2.Zero)
                {
                    var moveTo = entity.GetComponent<MoveToComponent>();
                    if (!moveTo.Active)
                    {
                        moveTo.Target = entity.GetComponent<TransformComponent>().Position + direction * _tileSize.ToVector2();
                        moveTo.Active = true;
                    }                    
                }
            }
        }
    }
}
