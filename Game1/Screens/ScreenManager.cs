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
        private readonly ICollection<Screen> _screens = new List<Screen>();

        private IEnumerable<Screen> _currentScreens;

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

            if (screen.IsSingle)
            {
                _currentScreens = new[] { screen };
            }
            else
            {
                _currentScreens = _screens.ToList();
            }
        }

        public TScreen Peek<TScreen>() where TScreen : Screen
        {
            return (TScreen)_screens.FirstOrDefault(x => x is TScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _currentScreens)
            {
                screen.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var screen in _currentScreens)
            {
                screen.Update(gameTime);
            }
        }
    }
}
