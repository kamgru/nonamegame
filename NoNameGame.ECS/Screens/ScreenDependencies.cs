using NoNameGame.Core.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Input;

namespace NoNameGame.Core.Screens
{
    public class ScreenDependencies
    {
        public ContentManager ContentManager { get; set; }
        public ScreenManager ScreenManager { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Session Session { get; set; }
        public IInputMapProvider InputMapProvider { get; set; }
        public IntentProvider IntentProvider { get; set; }
    }
}
