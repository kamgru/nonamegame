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
    public class SpriteDrawingSystem : SystemBase, IDrawingSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _debugFont;

        public SpriteDrawingSystem(IEntityManager entityManager, SystemsManager systemsManager, ContentManager contentManager, SpriteBatch spriteBatch)
            :base(entityManager, systemsManager)
        {
            _spriteBatch = spriteBatch;
            _debugFont = contentManager.Load<SpriteFont>("default");
        }

        public void Draw()
        {
            var entites = EntityManager.GetEntities().Where(x => x.HasComponent<Sprite>())
                .Select(x => new { Sprite = x.GetComponent<Sprite>(), Position = x.GetComponent<Transform>().Position })
                .Where(x => x.Sprite.Texture2D != null);

            foreach (var entity in entites)
            {
                _spriteBatch.Draw(entity.Sprite.Texture2D, entity.Position, entity.Sprite.Rectangle, Color.White);
            }

            var boardPosition = EntityManager.GetEntities().FirstOrDefault(x => x.HasComponent<PositionOnBoard>())?.GetComponent<PositionOnBoard>();
            if (boardPosition != null)
            {
                _spriteBatch.DrawString(_debugFont, $"c:{boardPosition.Current.X}, {boardPosition.Current.Y}", Vector2.Zero, Color.Red);
            }
        }
    }
}
