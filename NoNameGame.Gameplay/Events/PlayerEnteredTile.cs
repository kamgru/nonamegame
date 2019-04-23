using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Entities;

namespace NoNameGame.Gameplay.Events
{
    public class PlayerEnteredTile : IGameEvent
    {
        public Tile Tile { get; }

        public PlayerEnteredTile(Tile tile)
        {
            Tile = tile;
        }
    }
}