using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.ECS.Input
{
    public class IntentProvider
    {
        private readonly InputProvider _inputProvider;
        private readonly IInputMapProvider _inputMapProvider;
        public IntentProvider(InputProvider inputProvider, IInputMapProvider inputMapProvider)
        {
            _inputProvider = inputProvider;
            _inputMapProvider = inputMapProvider;
        }

        public IEnumerable<IIntent> GetIntents()
        {
            var pressedKeys = _inputProvider.GetPressedKeys();

            var intents = _inputMapProvider.GetActiveContexts()
                .SelectMany(context => context.InputIntentMap);

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

            return mappedIntents.Select(x => x.Intent);
        }
    }
}
