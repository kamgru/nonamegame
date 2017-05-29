using Game1.Core.Events;
using Game1.ECS.Components;
using Game1.ECS.Core;
using Game1.Gameplay.Components;

namespace Game1.Gameplay.Events
{
    public class PlayerAbandonedTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
        public State State { get; set; }
    }
}