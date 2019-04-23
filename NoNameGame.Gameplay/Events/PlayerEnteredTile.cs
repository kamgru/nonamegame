using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Events
{
    public class PlayerEnteredTile : IGameEvent
    {
        public TileInfo TileInfo { get; }
        public Entity TileEntity { get; }
        public PositionOnBoard PositionOnBoard { get; }

        public PlayerEnteredTile(TileInfo tileInfo, Entity tileEntity, PositionOnBoard positionOnBoard)
        {
            TileInfo = tileInfo;
            TileEntity = tileEntity;
            PositionOnBoard = positionOnBoard;
        }
    }
}