using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameGame.ECS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.ECS.Gui
{
    public static class ImGui
    {
        private static ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _defaultFont;
        private static Texture2D _btnTexture;

        private static Stack<Action> _guiActions = new Stack<Action>();

        private static MouseState _previousMouseState;
        private static MouseState _currentMouseState;

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _contentManager = contentManager;
            _spriteBatch = spriteBatch;
            _defaultFont = _contentManager.Load<SpriteFont>("default");
            _btnTexture = _contentManager.Load<Texture2D>("Gui\\btn_proxy");
            _previousMouseState = _currentMouseState = Mouse.GetState();
        }

        public static void Label(Vector2 position, string text, Color color = default)
        {
            _guiActions.Push(() => _spriteBatch.DrawString(_defaultFont, text, position, color));
        }

        public static bool Button(Vector2 position, Vector2 size, string text)
        {
            _guiActions.Push(() => _spriteBatch.DrawString(_defaultFont, text, position, Color.White));

            var hot = true;

            if (_previousMouseState.LeftButton == ButtonState.Pressed)
            {
                hot = false;
            }

            if (_currentMouseState.LeftButton != ButtonState.Pressed)
            {
                hot = false;
            }

            if (_currentMouseState.X < position.X 
                || _currentMouseState.X > (position.X + 64)
                || _currentMouseState.Y < position.Y
                || _currentMouseState.Y > (position.Y + 64))
            {
                hot = false;
            }

            if (hot)
            {
                _guiActions.Push(() => _spriteBatch.Draw(_btnTexture, position, null, Color.White));
            }
            else
            {
                _guiActions.Push(() => _spriteBatch.Draw(_btnTexture, position, null, Color.Red));
            }

            return hot;
        }

        public static void Update()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public static void Draw()
        {
            foreach(var action in _guiActions)
            {
                action();
            }

            _guiActions.Clear();
        }
    }
}
