using NoNameGame.Core.Events;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Events
{
    public class PlayerEnteredTile : IGameEvent
    {
        public TileInfo TileInfo { get; set; }
        public Entity TileEntity { get; set; }
    }
}