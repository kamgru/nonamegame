using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Core.Screens
{
    public class ScreenManager : DrawableGameComponent
    {
        private readonly IList<Screen> _screens = new List<Screen>();
        private readonly Stack<Screen> _screenStack = new Stack<Screen>();

        public ScreenManager(Game game) : base(game)
        {
        }

        public void Push<TScreen>() where TScreen : Screen
        {
            var screen = _screens.FirstOrDefault(x => x is TScreen);

            if (screen == null)
            {
                throw new InvalidOperationException($"Screen type {typeof(TScreen).ToString()} not registered");
            }

            if (!screen.IsInitialized)
            {
                screen.Init();
            }

            if (screen.ScreenMode == ScreenMode.Single)
            {
                _screenStack.Clear();
            }

            _screenStack.Push(screen);
        }

        public TScreen Peek<TScreen>() where TScreen : Screen
        {
            return (TScreen)_screens.FirstOrDefault(x => x is TScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screenStack.Reverse())
            {
                screen.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var idx = 0;
            foreach (var screen in _screenStack.ToList())
            {
                idx++;
                screen.Update(gameTime, idx == _screenStack.Count);
            }
        }

        public void Register<TScreen>(TScreen screen) where TScreen : Screen
        {
            if (_screens.Any(x => x is TScreen))
            {
                throw new ArgumentException($"Screen type {typeof(TScreen).ToString()} already registered");
            }

            _screens.Add(screen);
        }
    }
}
