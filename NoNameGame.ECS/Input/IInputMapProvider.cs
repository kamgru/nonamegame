using System.Collections.Generic;

namespace NoNameGame.ECS.Input
{
    public interface IInputMapProvider
    {
        IEnumerable<InputContext> GetActiveContexts();
        InputContext GetContextById(string id);

    }
}
