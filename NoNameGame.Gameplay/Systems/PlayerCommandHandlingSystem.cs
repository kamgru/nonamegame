using Microsoft.Xna.Framework;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Systems.CommandHandling;

namespace NoNameGame.Gameplay.Systems
{
    public class PlayerCommandHandlingSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<CommandQueue>>,
        IMessageListener<ComponentRemoved<CommandQueue>>
    {
        private MovePlayerCommandHandler _movePlayerCommandHandler;

        public PlayerCommandHandlingSystem(MovePlayerCommandHandler movePlayerCommandHandler)
        {
            _movePlayerCommandHandler = movePlayerCommandHandler;
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
                    if (command is MovePlayerCommand movePlayerCommand)
                    {
                        _movePlayerCommandHandler.Handle(movePlayerCommand);
                    }
                }
            }
        }
    }
}
