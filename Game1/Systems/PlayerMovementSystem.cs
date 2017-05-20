using Game1.Api;
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

namespace Game1.Systems
{
    public class PlayerMovementSystem : SystemBase, IUpdatingSystem
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

        public PlayerMovementSystem(IEntityManager entityManager, InputService inputService, IConfigurationService configurationService) : base(entityManager)
        {
            _inputService = inputService;
            _tileSize = configurationService.GetTileSizeInPixels();
        }

        public void Update(GameTime gameTime)
        {
            var intents = _inputService.GetCurrentIntents(_directionMap.Keys);

            var requestedDirections = _directionMap.Where(x => intents.Contains(x.Key)).Select(x => x.Value);

            if (requestedDirections.Count() == 1)
            {
                var entities = EntityManager.GetEntities().Where(x => x.HasComponent<IntentMap>()
                && !x.HasComponent<TargetScreenPosition>()
                && x.HasComponent<BoardPosition>());

                foreach (var entity in entities)
                {
                    var direction = requestedDirections.First();
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
