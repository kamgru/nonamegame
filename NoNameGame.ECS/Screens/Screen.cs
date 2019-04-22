using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Input;

namespace NoNameGame.Core.Screens
{
    public abstract class Screen
    {
        protected readonly ContentManager ContentManager;
        protected readonly ScreenManager ScreenManager;
        protected readonly SpriteBatch SpriteBatch;
        protected readonly Session Session;
        protected readonly IInputMapProvider InputMapProvider;
        protected readonly IntentProvider IntentProvider;

        public bool IsInitialized { get; protected set; }
        public ScreenMode ScreenMode { get; set; }

        protected Screen(ScreenDependencies dependencies)
        {
            ContentManager = dependencies.ContentManager;
            ScreenManager = dependencies.ScreenManager;
            SpriteBatch = dependencies.SpriteBatch;
            Session = dependencies.Session;
            InputMapProvider = dependencies.InputMapProvider;
            IntentProvider = dependencies.IntentProvider;
        }

        public virtual void Init()
        {
            IsInitialized = true;
        }
        public abstract void Update(GameTime gameTime, bool isActive);
        public abstract void Draw(GameTime gameTime);

    }
}