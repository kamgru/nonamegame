using System.Collections.Generic;

namespace NoNameGame.Core.Input
{
    public class InputContext
    {
        public string Id { get; set; }
        public IEnumerable<InputIntent> InputIntentMap { get; set; }
        public bool Active { get; private set; }

        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }
}
