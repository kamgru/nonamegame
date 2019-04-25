using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NoNameGame.ECS.Ui
{
    public static partial class Gui
    {
        private static ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _defaultFont;
        private static Texture2D _btnTexture;

        private static readonly Stack<Action> _guiActions = new Stack<Action>();

        private static ControlIdGenerator _idGenerator;
        private static UiState _uiState;

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _contentManager = contentManager;
            _spriteBatch = spriteBatch;
            _defaultFont = _contentManager.Load<SpriteFont>("default");
            _btnTexture = _contentManager.Load<Texture2D>("Gui\\btn_proxy");

            _uiState = new UiState();
            _idGenerator = new ControlIdGenerator();
        }

        public static void Label(Vector2 position, string text, Color color = default)
        {
            _guiActions.Push(() => _spriteBatch.DrawString(_defaultFont, text, position, color));
        }

        public static bool Button(Rectangle destinationRectangle, string text)
        {
            _guiActions.Push(() => _spriteBatch.DrawString(_defaultFont, text, destinationRectangle.TopLeft(), Color.White));

            var id = _idGenerator.GenerateId();
            if (_uiState.MouseOver(destinationRectangle))
            {
                _uiState.HotItemId = id;

                if (_uiState.ActiveItemId == 0 && _uiState.LeftButtonDown)
                {
                    _uiState.ActiveItemId = id;
                }
            }

            if (_uiState.HotItemId == id)
            {
                if (_uiState.ActiveItemId == id)
                {
                    _guiActions.Push(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.Red));
                }
                else
                {
                    _guiActions.Push(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.Yellow));
                }
            }
            else
            {
                _guiActions.Push(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.White));

            }

            if (!_uiState.LeftButtonDown
                && _uiState.HotItemId == id
                && _uiState.ActiveItemId == id)
            {
                return true;
            }

            return false;
        }

        public static void Begin()
        {
            _uiState.Prepare();
            _idGenerator.Reset();
        }

        public static void End()
        {
            _uiState.Cleanup();
        }

        public static void Draw()
        {
            foreach (var action in _guiActions)
            {
                action();
            }

            _guiActions.Clear();
        }
    }
}
