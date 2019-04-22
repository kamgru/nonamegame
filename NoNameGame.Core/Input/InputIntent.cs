using Microsoft.Xna.Framework.Input;

namespace NoNameGame.Core.Input
{
    public class InputIntent
    {
        public IIntent Intent { get; set; }
        public Keys Key { get; set; }
    }
}
