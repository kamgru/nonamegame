using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Data;

namespace NoNameGame.Gameplay.Components
{
    public class TileInfo : ComponentBase
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public TileType TileType { get; set; }
    }
}
