using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;

namespace NoNameGame.Gameplay.Components
{
    public class PositionOnBoard : ComponentBase
    {
        public Point Current { get; set; }
        public Point Previous { get; set; }

        public void Translate(Point translation)
        {
            Previous = Current;
            Current += translation;
        }
    }
}
