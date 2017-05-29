using Game1.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Core.Screens
{
    public enum ScreenMode
    {
        Single = 0,
        Overlay = 1
    }

    public abstract class Screen
    {
        protected readonly ContentManager ContentManager;
        protected readonly ScreenManager ScreenManager;
        protected readonly InputService InputService;
        protected readonly SpriteBatch SpriteBatch;
        protected readonly Session Session;

        public bool IsInitialized { get; protected set; }
        public ScreenMode ScreenMode { get; set; }

        protected Screen(ScreenDependencies dependencies)
        {
            ContentManager = dependencies.ContentManager;
            ScreenManager = dependencies.ScreenManager;
            InputService = dependencies.InputService;
            SpriteBatch = dependencies.SpriteBatch;
            Session = dependencies.Session;
        }

        public virtual void Init()
        {
            IsInitialized = true;
        }
        public abstract void Update(GameTime gameTime, bool isActive);
        public abstract void Draw(GameTime gameTime);

    }
}