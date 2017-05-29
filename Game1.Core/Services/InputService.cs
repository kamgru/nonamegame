using Game1.Core.Input;
using Game1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core.Services
{
    public class InputService
    {
        private readonly IntentMapper _intentMapper;
        private readonly ContextManager _contextManager;

        public InputService(IntentMapper intentMapper, ContextManager contextManager)
        {
            _intentMapper = intentMapper;
            _contextManager = contextManager;
        }

        public IReadOnlyCollection<Intent> ConsumeIntents(IEnumerable<Intent> filter = null)
        {
            var intents = _intentMapper.ConsumeIntents().Select(x => (Intent)x.Id);
            if (filter != null)
            {
                intents = intents.Intersect(filter);
            }

            return intents.ToList();
        }

        public void SetContextActive(int id, bool active)
        {
            _contextManager.GetContext(id).Active = active;
        }
    }
}
