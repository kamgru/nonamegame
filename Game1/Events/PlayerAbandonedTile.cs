using Game1.Components;
using Game1.ECS.Components;
using Game1.ECS.Core;

namespace Game1.Events
{
    public class PlayerAbandonedTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
        public State State { get; set; }
    }
}