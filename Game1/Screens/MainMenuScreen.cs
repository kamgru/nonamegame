using System.Linq;
using Game1.Data;
using Game1.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Core.Screens;

namespace Game1.Screens
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
            InputService.SetContextActive(0, true);

            _menu = new Menu(InputService, SpriteBatch, _menuFont);

            var viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            _menu.Position= new Vector2(viewport.Width / 2, viewport.Height / 2);

            var start = new MenuItem {Text = "Start"};
            start.OnSelected += (sender, args) =>
            {
                var gameplay = new GameplayScreen(new ScreenDependencies
                {
                    ContentManager = ContentManager,
                    ScreenManager = ScreenManager,
                    InputService = InputService,
                    SpriteBatch = SpriteBatch
                }, 0);

                ScreenManager.Push(gameplay);
            };

            var quit = new MenuItem {Text = "Quit"};
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
            SpriteBatch.DrawString(_menuFont, "THE GAME", Vector2.Zero,  Color.Black);
            _menu.Draw(gameTime);
        }
    }
}