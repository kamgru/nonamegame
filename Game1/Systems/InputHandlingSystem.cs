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

        public InputHandlingSystem(IEntityManager entityManager, IInputMappingService inputMappingService)
        {
            _entityManager = entityManager;
            _inputMappingService = inputMappingService;
        }

        public void Update()
        {
            var intent = _inputMappingService.GetIntents();

            var entities = _entityManager.GetEntities().Where(x => x.HasComponent<IntentMapComponent>() && x.HasComponent<MovementComponent>());

            foreach (var entity in entities)
            {
                var intentComponent = entity.GetComponent<IntentMapComponent>();
                var velocity = Vector2.Zero;

                if ((intentComponent.Intent & intent) == Intent.MoveDown)
                {
                    velocity += new Vector2(0, 1);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveUp)
                {
                    velocity += new Vector2(0, -1);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveLeft)
                {
                    velocity += new Vector2(-1, 0);
                }

                if ((intentComponent.Intent & intent) == Intent.MoveRight)
                {
                    velocity += new Vector2(1, 0);
                }

                entity.GetComponent<MovementComponent>().Velocity = velocity;             
            }
        }
    }
}
