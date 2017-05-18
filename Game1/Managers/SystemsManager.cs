using Game1.Api;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Managers
{
    public class SystemsManager
    {
        private HashSet<IUpdatingSystem> _updatingSystems = new HashSet<IUpdatingSystem>();
        private HashSet<IDrawingSystem> _drawingSystems = new HashSet<IDrawingSystem>();

        public void Push(IUpdatingSystem system)
        {
            _updatingSystems.Add(system);
        }

        public void Push(IDrawingSystem system)
        {
            _drawingSystems.Add(system);
        }

        public void UpdateSystems(GameTime gameTime)
        {
            foreach (var system in _updatingSystems)
            {
                system.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var system in _drawingSystems)
            {
                system.Draw();
            }
        }
    }
}
