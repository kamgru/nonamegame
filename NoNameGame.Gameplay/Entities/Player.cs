using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Entities
{
    public class Player : Entity
    {
        public Animator Animator { get; set; }
        public CommandQueue CommandQueue { get; set; }
        public PositionOnBoard PositionOnBoard { get; set; }
        public Sprite Sprite { get; set; }
        public State State { get; set; }
    }
}
