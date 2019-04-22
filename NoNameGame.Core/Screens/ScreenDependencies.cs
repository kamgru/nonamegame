using NoNameGame.Core.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.Core.Input;

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
