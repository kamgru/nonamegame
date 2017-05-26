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

        public bool IsSingle { get; protected set; }

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
}