using Microsoft.Xna.Framework;

namespace Game1.Components
{

    public class MoveToComponent : ComponentBase
    {
        public Vector2 Target { get; set; }
        public float Speed { get; set; }
    }
}