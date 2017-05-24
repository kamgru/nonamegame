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
        private readonly List<Screen> _screens = new List<Screen>();


        public ScreenManager(Game game) : base(game)
        {
        }

        public void Push(Screen screen)
        {
            if (_screens.Contains(screen))
            {
                _screens.Remove(screen);
            }
            _screens.Add(screen);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                screen.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                screen.Update(gameTime);
            }
        }
    }
}
