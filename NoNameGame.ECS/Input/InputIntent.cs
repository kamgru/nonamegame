using Microsoft.Xna.Framework.Input;

namespace NoNameGame.ECS.Input
{
    public class InputIntent
    {
        public IIntent Intent { get; set; }
        public Keys Key { get; set; }
    }
}
