using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Data;
using Game1.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Screens
{
    public abstract class Screen
    {
        protected readonly ContentManager ContentManager;
        protected readonly ScreenManager ScreenManager;
        protected readonly InputService InputService;
        protected readonly SpriteBatch SpriteBatch;

        public bool IsInitialized { get; protected set; }

        protected Screen(ContentManager contentManager, ScreenManager screenManager, InputService inputService, SpriteBatch spriteBatch)
        {
            ContentManager = contentManager;
            ScreenManager = screenManager;
            InputService = inputService;
            SpriteBatch = spriteBatch;
        }

        public virtual void Init()
        {
            IsInitialized = true;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

    }

    public class GameplayScreen : Screen
    {
        public GameplayScreen(ContentManager contentManager, ScreenManager screenManager, InputService inputService, SpriteBatch spriteBatch) 
            : base(contentManager, screenManager, inputService, spriteBatch)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

    public class MainMenuScreen : Screen
    {
        private SpriteFont _menuFont;

        public MainMenuScreen(ContentManager contentManager, ScreenManager screenManager, InputService inputService, SpriteBatch spriteBatch)
            : base(contentManager, screenManager, inputService, spriteBatch)
        {
        }

        public override void Init()
        {
            _menuFont = ContentManager.Load<SpriteFont>("default");
            InputService.SetContextActive(0, true);
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            var intents = InputService.ConsumeIntents(new[] {Intent.Confirm});

            if (intents.Any())
            {
                ScreenManager.Push(new GameplayScreen(ContentManager, ScreenManager, InputService, SpriteBatch));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(_menuFont, "MAIN MENU", Vector2.Zero,  Color.Black);
        }
    }
}
