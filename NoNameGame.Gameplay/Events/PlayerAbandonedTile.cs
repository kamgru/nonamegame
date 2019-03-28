using NoNameGame.Core.Events;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Events
{
    public class PlayerAbandonedTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
        public State State { get; set; }
    }
}