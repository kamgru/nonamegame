using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Input
{
    public class IntentMapper
    {
        private ContextManager _contextManager;
        private InputProvider _inputProvider;

        public IntentMapper(ContextManager contextManager, InputProvider inputProvider)
        {
            _contextManager = contextManager;
            _inputProvider = inputProvider;
        }

        public IReadOnlyCollection<InputIntent> MapIntents()
        {
            var pressedKeys = _inputProvider.GetPressedKeys();
            var intents = _contextManager.GetActiveContexts().SelectMany(context => context.Intents);

            var mappedIntents = new List<InputIntent>();

            if (pressedKeys.Any())
            {
                foreach (var intent in intents)
                {
                    if (pressedKeys.Contains(intent.Key))
                    {
                        mappedIntents.Add(intent);
                        pressedKeys.Remove(intent.Key);
                    }
                }
            }

            return mappedIntents;
        }
    }
}
