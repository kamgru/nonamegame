using System.Collections.Generic;

namespace Game1.Input
{
    public class InputContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Intent> Intents { get; set; }
        public bool Active { get; set; }
    }
}
