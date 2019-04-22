using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Core.Screens;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Input;
using NoNameGame.Gameplay.Data;
using NoNameGame.Main.Screens;

namespace NoNameGame.Main
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();

            Content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            BootstrapScreens();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif
            _screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _screenManager.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void BootstrapScreens()
        {
            _screenManager = new ScreenManager(this);

            var inputMapProvider = new InputMapProvider();
            var screenDependencies = new ScreenDependencies
            {
                ContentManager = Content,
                ScreenManager = _screenManager,
                SpriteBatch = _spriteBatch,
                Session = new Session(),
                InputMapProvider = inputMapProvider,
                IntentProvider = new IntentProvider(new InputProvider(), inputMapProvider)
            };

            _screenManager.Register(new MainMenuScreen(screenDependencies));
            _screenManager.Register(new GameplayScreen(screenDependencies));
            _screenManager.Register(new StageClearScreen(screenDependencies));

            _screenManager.Push<MainMenuScreen>();
        }
    }
}
