using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NoNameGame.Gameplay.Data
{
    public class BoardData
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public IReadOnlyCollection<TileData> Tiles { get; set; }
    }
}
