using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private static GuiState _state;

        private static Color _grey = new Color(40, 40, 40, 160);
        private static List<(int controlId, IEnumerable<Action> draws)> _draws = new List<(int, IEnumerable<Action>)>();

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch, GameWindow gameWindow)
        {
            _contentManager = contentManager;
            _spriteBatch = spriteBatch;

            _defaultFont = _contentManager.Load<SpriteFont>("default");
            _btnTexture = _contentManager.Load<Texture2D>("Gui\\btn_proxy");
            _blankTexture = _contentManager.Load<Texture2D>("blank");

            _state = new GuiState();

            _idGenerator = new ControlIdGenerator();

            gameWindow.TextInput += (s, e) =>
            {
                _state.Keyboard.CurrentCharacter = e.Character;
                _state.Keyboard.LastKey = e.Key;
            };
        }

        public static float Slider(Rectangle destinationRectangle, float sliderValue)
        {
            var draws = new List<Action>();
            var id = _idGenerator.GenerateId();

            var knobHeight = 8;

            var yOffset = (int)(destinationRectangle.Height * sliderValue) - (knobHeight / 2);

            if (_state.Mouse.MouseOver(destinationRectangle))
            {
                _state.HotItemId = id;

                if (_state.ActiveItemId == 0 && _state.Mouse.LeftButtonDown)
                {
                    _state.ActiveItemId = id;
                }
            }

            draws.Add(() => _spriteBatch.Draw(_blankTexture, destinationRectangle, _grey));

            if (_state.ActiveItemId == id || _state.HotItemId == id)
            {
                draws.Add(() => _spriteBatch.Draw(_blankTexture, new Rectangle(destinationRectangle.X, destinationRectangle.Y + yOffset, 8, knobHeight), Color.Red));
            }
            else
            {
                draws.Add(() => _spriteBatch.Draw(_blankTexture, new Rectangle(destinationRectangle.X, destinationRectangle.Y + yOffset, 8, knobHeight), Color.Blue));
            }

            PushDraw(id, draws.ToArray());

            if (_state.ActiveItemId == id)
            {
                int mousePosition = _state.Mouse.Y - destinationRectangle.Y;
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

        public static string TextBox(Rectangle dstRect, string text, Color color = default)
        {
            var id = _idGenerator.GenerateId();

            if (_state.Mouse.MouseOver(dstRect))
            {
                _state.HotItemId = id;

                if (_state.ActiveItemId == 0 && _state.Mouse.LeftButtonDown)
                {
                    _state.ActiveItemId = id;
                }
            }

            var draws = new List<Action>();
            draws.Add(() => _spriteBatch.Draw(_blankTexture, dstRect, _grey));

            if (_state.ActiveItemId == id)
            {
                _state.ActiveTextBoxId = id;
            }

            var position = new Vector2(dstRect.X, dstRect.Y);

            if (_state.ActiveTextBoxId == id)
            { 
                if (_state.Keyboard.LastKey == Keys.Back)
                {
                    text = text.Substring(0, Math.Max(0, text.Length - 1));
                }
                else if (_state.Keyboard.CurrentCharacter != default 
                    &&  _defaultFont.Characters.Contains(_state.Keyboard.CurrentCharacter))
                {
                    text += _state.Keyboard.CurrentCharacter;

                    var size = _defaultFont.MeasureString(text);

                    if (size.X > dstRect.Width)
                    {
                        position.X -= size.X - dstRect.Width;
                    }
                }
            }

            draws.Add(() => _spriteBatch.DrawString(_defaultFont, text, position, color));


            PushDraw(id, draws.ToArray());

            if (_state.ActiveTextBoxId == id && _state.HotItemId != id && _state.Mouse.LeftButtonDown)
            {
                _state.ActiveTextBoxId = 0;
            }

            return text;
        }

        public static bool Button(Rectangle destinationRectangle, string text)
        {
            var draws = new List<Action>();
            var id = _idGenerator.GenerateId();

            if (_state.Mouse.MouseOver(destinationRectangle))
            {
                _state.HotItemId = id;

                if (_state.ActiveItemId == 0 && _state.Mouse.LeftButtonDown)
                {
                    _state.ActiveItemId = id;
                }
            }

            if (_state.HotItemId == id)
            {
                if (_state.ActiveItemId == id)
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

            if (!_state.Mouse.LeftButtonDown
                && _state.HotItemId == id
                && _state.ActiveItemId == id)
            {
                return true;
            }

            return false;
        }

        public static void Begin()
        {
            _state.Begin();
            _idGenerator.Reset();
        }

        public static void End()
        {
            _state.End();
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
