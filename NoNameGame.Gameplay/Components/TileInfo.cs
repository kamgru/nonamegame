using NoNameGame.Data;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;

namespace NoNameGame.Gameplay.Components
{
    public class TileInfo : ComponentBase
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public bool Destroyed { get; set; }
        public TileType TileType { get; set; }
    }
}
