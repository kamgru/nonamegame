using Game1.Api;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Managers
{
    public class SystemsManager
    {
        private readonly IDictionary<Type, IUpdatingSystem> _updatingSystems = new Dictionary<Type, IUpdatingSystem>();
        private readonly IDictionary<Type, IDrawingSystem> _drawingSystems = new Dictionary<Type, IDrawingSystem>();


        public void Push(IUpdatingSystem system)
        {
            _updatingSystems.Add(system.GetType(), system);
        }

        public void Push(IDrawingSystem system)
        {
            _drawingSystems.Add(system.GetType(), system);
        }


        public void UpdateSystems(GameTime gameTime)
        {
            foreach (var system in _updatingSystems.Values.Where(x => x.IsActive()))
            {
                system.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var system in _drawingSystems.Values.Where(x => x.IsActive()))
            {
                system.Draw();
            }
        }
    }
}
