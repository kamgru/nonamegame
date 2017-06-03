using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Core.Input
{
    public class ContextManager
    {
        private readonly ICollection<InputContext> _contexts = new List<InputContext>();

        public void Add(InputContext context)
            => _contexts.Add(context);

        public IReadOnlyCollection<InputContext> GetActiveContexts()
            => _contexts.Where(context => context.Active).ToList();

        public InputContext GetContext(int id)
        {
            return _contexts.FirstOrDefault(x => x.Id == id);
        }
    }
}
