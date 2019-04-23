using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Entities
{
    public class Tile : Entity
    {
        public Animator Animator { get; set; }
        public Sprite Sprite { get; set; }
        public State State { get; set; }
        public TileInfo TileInfo { get; set; }
    }
}
