using System;

namespace Game1.Data
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public TileType TileType { get; set; }
    }

    [Flags]
    public enum TileType
    {
        Normal = 1,
        Start = 2,
        End = 4
    }
}
