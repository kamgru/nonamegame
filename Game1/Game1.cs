using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using NoNameGame.Data;
using NoNameGame.Main.Screens;
using NoNameGame.Core.Services;
using NoNameGame.Core.Screens;
using NoNameGame.Core.Input;

namespace NoNameGame.Main
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputService _inputService;
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

            SetupInput();
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

        private void SetupInput()
        {
            var contextManager = new ContextManager();
            foreach (var context in new ConfigurationService().GetInputContexts())
            {
                contextManager.Add(context);
            }
            _inputService = new InputService(new IntentMapper(contextManager, new InputProvider()), contextManager);
        }

        private void BootstrapScreens()
        {
            _screenManager = new ScreenManager(this);

            var screenDependencies = new ScreenDependencies
            {
                ContentManager = Content,
                ScreenManager = _screenManager,
                InputService = _inputService,
                SpriteBatch = _spriteBatch,
                Session = new Session()
            };

            _screenManager.Register(new MainMenuScreen(screenDependencies));
            _screenManager.Register(new GameplayScreen(screenDependencies));
            _screenManager.Register(new StageClearScreen(screenDependencies));

            _screenManager.Push<MainMenuScreen>();
        }
    }
}
