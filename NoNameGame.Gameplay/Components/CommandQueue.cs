using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Systems.CommandHandling;
using System.Collections.Generic;

namespace NoNameGame.Gameplay.Components
{
    public class CommandQueue : ComponentBase
    {
        private Queue<ICommand> commands;

        public CommandQueue()
        {
            commands = new Queue<ICommand>();
        }

        public void Enqueue(ICommand command)
        {
            commands.Enqueue(command);
        }

        public ICommand Dequeue()
        {
            return commands.Count > 0
                ? commands.Dequeue()
                : null;
        }
    }
}
