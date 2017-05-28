using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Screens
{
    public class ScreenManager : DrawableGameComponent
    {
        private readonly IList<Screen> _screens = new List<Screen>();
        private IList<Screen> _screensToProcess;

        public ScreenManager(Game game) : base(game)
        {
        }

        public void Push(Screen screen)
        {
            _screens.Add(screen);

            if (!screen.IsInitialized)
            {
                screen.Init();
            }

            _screensToProcess = new List<Screen>(_screens.Count);

            foreach (var scr in _screens.Reverse())
            {
                _screensToProcess.Insert(0, scr);
                if (scr.ScreenMode == ScreenMode.Single)
                {
                    break;
                }
            }
        }

        public TScreen Peek<TScreen>() where TScreen : Screen
        {
            return (TScreen)_screens.FirstOrDefault(x => x is TScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screensToProcess)
            {
                screen.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < _screensToProcess.Count; i++)
            {
                _screensToProcess[i].Update(gameTime, i == _screensToProcess.Count - 1);
            }
        }
    }
}
