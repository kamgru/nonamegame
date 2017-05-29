using System;

namespace Game1.Data
{

    [Flags]
    public enum TileType
    {
        Normal = 1,
        Start = 2,
        End = 4
    }
}