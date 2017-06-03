using System.Collections.Generic;

namespace NoNameGame.Core.Input
{
    public class InputContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<InputIntent> Intents { get; set; }
        public bool Active { get; set; }
    }
}
