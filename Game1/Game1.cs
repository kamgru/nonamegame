using Game1.Api;
using Game1.Components;
using Game1.Services;
using Game1.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Game1.Factories;
using Game1.Input;
using Game1.Data;
using Game1.Screens;

namespace Game1
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

            this.Services.AddService(new Session());

            var contextManager = new ContextManager();
            contextManager.Add(new InputContext
            {
                Id = (int)Context.Gameplay,
                Active = true,
                Name = "gameplay context",
                Intents = new[]
                {
                    new InputIntent
                    {
                         Id = (int)Intent.MoveLeft,
                         Key = Keys.A
                    },
                    new InputIntent
                    {
                        Id = (int)Intent.MoveRight,
                        Key = Keys.D
                    },
                    new InputIntent
                    {
                         Id = (int)Intent.MoveUp,
                         Key = Keys.W
                    },
                    new InputIntent
                    {
                        Id = (int)Intent.MoveDown,
                        Key = Keys.S
                    },
                }
            });

            contextManager.Add(new InputContext
            {
                Id = (int)Context.Generic,
                Active = true,
                Name = "generic context",
                Intents = new[]
                {
                    new InputIntent
                    {
                        Id = (int)Intent.Confirm,
                        Key = Keys.Space
                    }
                }
            });

            Content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _inputService = new InputService(new IntentMapper(contextManager, new InputProvider()), contextManager);
            _screenManager = new ScreenManager(this);

            var mainMenu = new MainMenuScreen(new ScreenDependencies
            {
                ContentManager = Content,
                ScreenManager = _screenManager,
                InputService = _inputService,
                SpriteBatch = _spriteBatch
            });
            mainMenu.Init();

            _screenManager.Push(mainMenu);            
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
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
    }
}
