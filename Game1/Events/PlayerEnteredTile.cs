using Game1.Components;
using Game1.Entities;

namespace Game1.Events
{

    public class PlayerEnteredTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
    }
}