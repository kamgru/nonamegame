using Game1.Data;
using Game1.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Services
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

        public IReadOnlyCollection<Intent> GetCurrentIntents(IEnumerable<Intent> filter = null)
        {
            var intents = _intentMapper.MapIntents().Select(x => (Intent)x.Id);
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
