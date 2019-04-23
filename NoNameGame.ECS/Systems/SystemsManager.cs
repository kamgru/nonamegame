using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.ECS.Systems
{
    public class SystemsManager
    {
        private readonly IDictionary<Type, IUpdatingSystem> _updatingSystems = new Dictionary<Type, IUpdatingSystem>();
        private readonly IDictionary<Type, IDrawingSystem> _drawingSystems = new Dictionary<Type, IDrawingSystem>();
        private readonly IDictionary<Type, ISystem> _basicSystems = new Dictionary<Type, ISystem>();

        public void Push(IUpdatingSystem system)
        {
            _updatingSystems.Add(system.GetType(), system);
        }

        public void Push(IDrawingSystem system)
        {
            _drawingSystems.Add(system.GetType(), system);
        }

        public void Push(ISystem system)
        {
            _basicSystems.Add(system.GetType(), system);
        }

        public TSystem Peek<TSystem>() where TSystem : ISystem
        {
            if (typeof(IUpdatingSystem).IsAssignableFrom(typeof(TSystem))
                && _updatingSystems.ContainsKey(typeof(TSystem)))
            {
                return (TSystem)_updatingSystems[typeof(TSystem)];
            }

            if (typeof(IDrawingSystem).IsAssignableFrom(typeof(TSystem))
                && _drawingSystems.ContainsKey(typeof(TSystem)))
            {
                return (TSystem)_drawingSystems[typeof(TSystem)];
            }

            if (_basicSystems.ContainsKey(typeof(TSystem)))
            {
                return (TSystem)_basicSystems[typeof(TSystem)];
            }

            return default;
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

        public void ResetSystems()
        {
            var allSystems = _basicSystems.Values
                .Concat(_updatingSystems.Values)
                .Concat(_drawingSystems.Values);

            foreach (var system in allSystems)
            {
                system.Reset();
            }
        }
    }
}
