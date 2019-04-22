using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.CommandHandling;
using System.Collections.Generic;

namespace NoNameGame.ECS.Systems
{
    public class CommandHandlingSystem
        : SystemBase,
        IUpdatingSystem,
        IMessageListener<ComponentAdded<CommandQueue>>,
        IMessageListener<ComponentRemoved<CommandQueue>>
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public CommandHandlingSystem()
        {
            SystemMessageBroker.AddListener<ComponentAdded<CommandQueue>>(this);
            SystemMessageBroker.AddListener<ComponentRemoved<CommandQueue>>(this);
        }

        public void Handle(ComponentAdded<CommandQueue> message)
        {
            _entities.Add(message.Entity);
        }

        public void Handle(ComponentRemoved<CommandQueue> message)
        {
            _entities.Remove(message.Entity);
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<CommandQueue>())
            {
                _entities.Remove(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                var queue = entity.GetComponent<CommandQueue>();
                while (queue.Dequeue() is ICommand command)
                {
                    command.Execute();
                }
            }
        }
    }
}
