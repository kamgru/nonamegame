using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Screens;
using NoNameGame.Gameplay.Data;
using NoNameGame.ECS.Ui;

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
            //_menuFont = ContentManager.Load<SpriteFont>("default");
            //InputMapProvider.GetContextById(Contexts.Menu)?.Activate();

            //_menu = new Menu(IntentProvider, SpriteBatch, _menuFont);

            //var viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            //_menu.Position = new Vector2(viewport.Width / 2, viewport.Height / 2);

            //var start = new MenuItem { Text = "Start" };

            //start.OnSelected += (sender, args) =>
            //{
            //    InputMapProvider.GetContextById(Contexts.Menu).Deactivate();
            //    ScreenManager.Push<GameplayScreen>();
            //};

            //var quit = new MenuItem { Text = "Quit" };
            //quit.OnSelected += (sender, args) =>
            //{
            //    ScreenManager.Game.Exit();
            //};

            //_menu.AddMenuItem(start);
            //_menu.AddMenuItem(quit);

            base.Init();
        }

        private string labelText = "dupa";

        public override void Update(GameTime gameTime, bool isActive)
        {
            if (isActive)
            {
                //_menu.Update(gameTime);

                

                if (ECS.Ui.Gui.Button(new Rectangle(10, 10, 64, 32), "left"))
                {
                    labelText = "chuj";
                }

                if (ECS.Ui.Gui.Button(new Rectangle(310, 10, 64, 32), "right"))
                {
                    labelText = "cipa";
                }

                Gui.Label(new Vector2(350, 100), labelText, Color.Red);

                Gui.Slider(new Rectangle(20, 90, 20, 256), 100, 50);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //SpriteBatch.DrawString(_menuFont, "THE GAME", Vector2.Zero, Color.Black);
            //_menu.Draw(gameTime);
        }
    }
}