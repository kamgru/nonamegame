using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Screens;
using NoNameGame.Gameplay.Data;
using NoNameGame.Main.Gui;

namespace NoNameGame.Main.Screens
{

    public class MainMenuScreen : Screen
    {
        private SpriteFont _menuFont;
        private Menu _menu;

        public MainMenuScreen(ScreenDependencies dependencies)
            : base(dependencies)
        {
        }

        public override void Init()
        {
            _menuFont = ContentManager.Load<SpriteFont>("default");
            InputMapProvider.GetContextById(Contexts.Menu)?.Activate();

            _menu = new Menu(IntentProvider, SpriteBatch, _menuFont);

            var viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            _menu.Position = new Vector2(viewport.Width / 2, viewport.Height / 2);

            var start = new MenuItem { Text = "Start" };

            start.OnSelected += (sender, args) =>
            {
                InputMapProvider.GetContextById(Contexts.Menu).Deactivate();
                ScreenManager.Push<GameplayScreen>();
            };

            var quit = new MenuItem { Text = "Quit" };
            quit.OnSelected += (sender, args) =>
            {
                ScreenManager.Game.Exit();
            };

            _menu.AddMenuItem(start);
            _menu.AddMenuItem(quit);

            base.Init();
        }

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (isActive)
            {
                _menu.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(_menuFont, "THE GAME", Vector2.Zero, Color.Black);
            _menu.Draw(gameTime);
        }
    }
}