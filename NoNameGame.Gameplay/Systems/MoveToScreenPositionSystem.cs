using NoNameGame.Gameplay.Components;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using System.Collections.Generic;
using NoNameGame.ECS.Entities;
using System;
using System.Linq;

namespace NoNameGame.Gameplay.Systems
{
    public class MoveToScreenPositionSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<TargetScreenPosition>>,
        IMessageListener<ComponentRemoved<TargetScreenPosition>>
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public MoveToScreenPositionSystem()
        {
            SystemMessageBroker.AddListener<ComponentAdded<TargetScreenPosition>>(this);
            SystemMessageBroker.AddListener<ComponentRemoved<TargetScreenPosition>>(this);
        }

        public void Handle(ComponentRemoved<TargetScreenPosition> message)
        {
            _entities.Remove(message.Entity);
        }

        public void Handle(ComponentAdded<TargetScreenPosition> message)
        {
           _entities.Add(message.Entity);
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<TargetScreenPosition>())
            {
                _entities.Remove(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                var component = entity.GetComponent<TargetScreenPosition>();

                component.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                entity.Transform.Position = component.Elapsed >= component.Duration
                    ? component.Target
                    : CalculatePosition(component);
            }
        }

        private Vector2 CalculatePosition(TargetScreenPosition component)
        {
            var elapsed = component.Elapsed / (component.Duration / 2f);

            var newx = EaseInOut(elapsed, component.Start.X, component.Target.X - component.Start.X);
            var newy = EaseInOut(elapsed, component.Start.Y, component.Target.Y - component.Start.Y);

            return new Vector2(newx, newy);
        }

        private float EaseInOut(float elapsed, float start, float change)
        {
            return elapsed < 1
                ? change / 2 * elapsed * elapsed + start
                : -change / 2 * ((--elapsed) * (elapsed - 2) - 1) + start;
        }
    }
}
