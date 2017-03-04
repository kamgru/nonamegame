using Game1.Components;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Systems
{
    public class DrawingSystem : ISystem
    {
        private readonly IEntityFactory _entityFactory;



        public DrawingSystem(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var entites = _entityFactory.GetEntities().Where(x => x.HasComponent<SpriteComponent>())
                .Select(x => new { Texture2D = x.GetComponent<SpriteComponent>().Texture2D, Position = x.GetComponent<TransformComponent>().Position })
                .Where(x => x.Texture2D != null);

            foreach (var entity in entites)
            {
                spriteBatch.Draw(entity.Texture2D, entity.Position, Color.White);
            }
        }
    }
}
