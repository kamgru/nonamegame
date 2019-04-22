using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;

namespace NoNameGame.Gameplay.Components
{
    public class TargetScreenPosition : ComponentBase
    {
        public Vector2 Target { get; set; }
        public Vector2 Start { get; set; }
        public float Duration { get; set; }
        public float Elapsed { get; set; }
    }
}