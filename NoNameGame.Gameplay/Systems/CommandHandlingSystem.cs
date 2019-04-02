using Microsoft.Xna.Framework;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Systems.CommandHandling;

namespace NoNameGame.Gameplay.Systems
{
    public class CommandHandlingSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<CommandQueue>>,
        IMessageListener<ComponentRemoved<CommandQueue>>
    {
        public CommandHandlingSystem()
        {
            SystemMessageBroker.AddListener<ComponentAdded<CommandQueue>>(this);
            SystemMessageBroker.AddListener<ComponentRemoved<CommandQueue>>(this);
        }

        public void Handle(ComponentAdded<CommandQueue> message)
        {
            Entities.Add(message.Entity);
        }

        public void Handle(ComponentRemoved<CommandQueue> message)
        {
            Entities.Remove(message.Entity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
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
