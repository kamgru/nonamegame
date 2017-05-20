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

        public InputService(IntentMapper intentMapper)
        {
            _intentMapper = intentMapper;
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
    }
}
