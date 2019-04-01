using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;

namespace NoNameGame.Gameplay.Systems.CommandHandling
{
    public class MovePlayerCommand : ICommand
    {
        public Vector2 Direction { get; }
        public Vector2 Distance { get; }
        public Entity Player { get; }

        public MovePlayerCommand(
            Vector2 direction, 
            Vector2 distance,
            Entity player)
        {
            Direction = direction;
            Distance = distance;
            Player = player;
        }
    }
}
