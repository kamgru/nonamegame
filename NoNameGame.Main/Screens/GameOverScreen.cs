using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Screens;
using NoNameGame.Gameplay.Data;
using System.Linq;

namespace NoNameGame.Main.Screens
{
    public class GameOverScreen : Screen
    {
        private SpriteFont _defaultFont;
        private Texture2D _overlayTexture;
        private Viewport _viewport;
        private Vector2 _position;
        private string _text = "GAME OVER";

        public GameOverScreen(ScreenDependencies dependencies) 
            : base(dependencies)
        {
            ScreenMode = ScreenMode.Overlay;
        }

        public override void Init()
        {
            base.Init();
            _defaultFont = ContentManager.Load<SpriteFont>("default");
            _overlayTexture = ContentManager.Load<Texture2D>("blank");
            _viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            _position = new Vector2((_viewport.Width - _defaultFont.MeasureString(_text).X) / 2, _viewport.Height / 2);
        }

        public override void OnEnter()
        {
            InputMapProvider.GetContextById(Contexts.Menu).Activate();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_overlayTexture, _viewport.Bounds, null, new Color(0, 0, 0, 150));
            SpriteBatch.DrawString(_defaultFont, "GAME OVER", _position, Color.White);
            SpriteBatch.DrawString(_defaultFont, "press ENTER to restart", new Vector2(_position.X, _position.Y + 30), Color.White);
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            var intents = IntentProvider.GetIntents().ToArray();
            if (!intents.Any())
            {
                return;
            }

            if (intents[0] is ConfirmIntent)
            {
                InputMapProvider.GetContextById(Contexts.Menu).Deactivate();

                var gameplay = ScreenManager.Peek<GameplayScreen>();
                ScreenManager.Push<GameplayScreen>();
            }
        }
    }
}
