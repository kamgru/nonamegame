using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Input;
using NoNameGame.Gameplay.Data;

namespace NoNameGame.Main.Gui
{
    public class Menu
    {
        private IntentProvider _intentProvider;
        private int _currentIndex;
        private readonly IList<MenuItem> _menuItems;
        private readonly SpriteFont _defaultFont;
        private readonly SpriteBatch _spriteBatch;

        public Vector2 Position { get; set; }

        public Menu(IntentProvider intentProvider, SpriteBatch spriteBatch, SpriteFont font) 
        {
            _intentProvider = intentProvider;
            _spriteBatch = spriteBatch;
            _defaultFont = font;
            _menuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
            => _menuItems.Add(item);

        public void Update(GameTime gameTime)
        {
            var intents = _intentProvider.GetIntents();

            if (intents.Any(x => x is ConfirmIntent))
            {
                _menuItems[_currentIndex].Select();
            }

            if (intents.Any(x => x is MoveUpIntent))
            {
                _currentIndex -= (int)Vector2.UnitY.Y;
            }
            if (intents.Any(x => x is MoveDownIntent))
            {
                _currentIndex += (int)Vector2.UnitY.Y;
            }

            if (_currentIndex < 0)
            {
                _currentIndex = 0;
            }
            if (_currentIndex > _menuItems.Count - 1)
            {
                _currentIndex = _menuItems.Count - 1;
            }
        }

        public void Draw(GameTime gameTime)
        {
            var pulse = (float)Math.Sin(gameTime.TotalGameTime.Milliseconds * 6.28f) + 1;
            var scale = 1 + pulse * 0.25f;
            for (var i = 0; i < _menuItems.Count; i++)
            {
                var posY = Position.Y + (_defaultFont.MeasureString(_menuItems[i].Text).Y + 10) * (i + 1);

                _spriteBatch.DrawString(_defaultFont, _menuItems[i].Text, new Vector2(Position.X, posY),
                    i == _currentIndex ? Color.Red : Color.Black,
                     0, Vector2.Zero, i == _currentIndex ? scale : 1, SpriteEffects.None, 0);
            }
        }
    }
}
