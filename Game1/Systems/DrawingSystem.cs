using Game1.Api;
using Game1.Components;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private readonly IEntityManager _entityManager;
        private readonly ContentManager _contentManager;

        private readonly SpriteFont _debugFont;

        public DrawingSystem(IEntityManager entityManager, ContentManager contentManager)
        {
            _entityManager = entityManager;
            _contentManager = contentManager;
            _debugFont = contentManager.Load<SpriteFont>("default");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var entites = _entityManager.GetEntities().Where(x => x.HasComponent<Sprite>())
                .Select(x => new { Texture2D = x.GetComponent<Sprite>().Texture2D, Position = x.GetComponent<Transform>().Position })
                .Where(x => x.Texture2D != null);

            foreach (var entity in entites)
            {
                spriteBatch.Draw(entity.Texture2D, entity.Position, Color.White);
            }

            var boardPosition = _entityManager.GetEntities().FirstOrDefault(x => x.HasComponent<BoardPosition>())?.GetComponent<BoardPosition>();
            if (boardPosition != null)
            {
                spriteBatch.DrawString(_debugFont, $"c:{boardPosition.Current.X}, {boardPosition.Current.Y}", Vector2.Zero, Color.Red);
            }
        }
    }
}
