using Game1.Core.Events;
using Game1.ECS.Core;
using Game1.Gameplay.Components;

namespace Game1.Gameplay.Events
{
    public class PlayerEnteredTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
    }
}