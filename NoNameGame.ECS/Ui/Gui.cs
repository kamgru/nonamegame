using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NoNameGame.ECS.Ui
{
    public static partial class Gui
    {
        private static ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _defaultFont;
        private static Texture2D _btnTexture;
        private static Texture2D _blankTexture;

        private static ControlIdGenerator _idGenerator;
        private static UiState _uiState;

        private static Color _grey = new Color(40, 40, 40, 160);
        private static List<(int controlId, IEnumerable<Action> draws)> _draws = new List<(int, IEnumerable<Action>)>();

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _contentManager = contentManager;
            _spriteBatch = spriteBatch;
            _defaultFont = _contentManager.Load<SpriteFont>("default");
            _btnTexture = _contentManager.Load<Texture2D>("Gui\\btn_proxy");
            _blankTexture = _contentManager.Load<Texture2D>("blank");

            _uiState = new UiState();
            _idGenerator = new ControlIdGenerator();
        }

        public static float Slider(Rectangle destinationRectangle, float sliderValue)
        {
            var draws = new List<Action>();
            var id = _idGenerator.GenerateId();

            var knobHeight = 8;

            var yOffset = (int)(destinationRectangle.Height * sliderValue) - (knobHeight / 2);

            if (_uiState.MouseOver(destinationRectangle))
            {
                _uiState.HotItemId = id;

                if (_uiState.ActiveItemId == 0 && _uiState.LeftButtonDown)
                {
                    _uiState.ActiveItemId = id;
                }
            }

            draws.Add(() => _spriteBatch.Draw(_blankTexture, destinationRectangle, _grey));

            if (_uiState.ActiveItemId == id || _uiState.HotItemId == id)
            {
                draws.Add(() => _spriteBatch.Draw(_blankTexture, new Rectangle(destinationRectangle.X, destinationRectangle.Y + yOffset, 8, knobHeight), Color.Red));
            }
            else
            {
                draws.Add(() => _spriteBatch.Draw(_blankTexture, new Rectangle(destinationRectangle.X, destinationRectangle.Y + yOffset, 8, knobHeight), Color.Blue));
            }

            PushDraw(id, draws.ToArray());

            if (_uiState.ActiveItemId == id)
            {
                int mousePosition = _uiState.MouseY - destinationRectangle.Y;
                if (mousePosition < 0) mousePosition = 0;
                if (mousePosition > destinationRectangle.Height) mousePosition = destinationRectangle.Height;

                sliderValue = (float)mousePosition / destinationRectangle.Height;
            }

            return sliderValue;
        }

        public static void Label(Vector2 position, string text, Color color = default)
        {
            PushDraw(_idGenerator.GenerateId(), () => _spriteBatch.DrawString(_defaultFont, text, position, color));
        }

        public static bool Button(Rectangle destinationRectangle, string text)
        {
            var draws = new List<Action>();
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
                    draws.Add(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.Red));
                }
                else
                {
                    draws.Add(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.Yellow));
                }
            }
            else
            {
                draws.Add(() => _spriteBatch.Draw(_btnTexture, destinationRectangle, null, Color.White));
            }

            draws.Add(() => _spriteBatch.DrawString(_defaultFont, text, destinationRectangle.TopLeft(), Color.White));

            PushDraw(id, draws.ToArray());

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
            foreach (var d in _draws)
            {
                foreach (var a in d.draws)
                {
                    a();
                }
            }

            _draws.Clear();
        }



        private static void PushDraw(int controlId, params Action[] draws)
        {
            if (_draws.Any(x => x.controlId == controlId))
            {
                return;
            }

            _draws.Add((controlId, draws));
            
        }
    }
}
