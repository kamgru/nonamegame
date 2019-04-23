using Microsoft.Xna.Framework;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using System.Linq;

namespace NoNameGame.Gameplay.Factories
{
    public class BoardFactory
    {
        private readonly TileFactory _tileFactory;

        public BoardFactory(TileFactory tileFactory)
        {
            _tileFactory = tileFactory;
        }

        public Board CreateBoard(BoardData data)
        {
            var board = new Board();

            var tiles = data.Tiles
                .Select(tileData =>
                {
                    var tile = _tileFactory.CreateTile(tileData);
                    tile.Transform.SetParent(board.Transform);
                    return tile;
                })
                .ToList();

            board.AddComponent(new BoardInfo
            {
                Size = new Point
                {
                    X = tiles.Max(t => t.GetComponent<TileInfo>().Position.X) + 1,
                    Y = tiles.Max(t => t.GetComponent<TileInfo>().Position.Y) + 1
                }
            });

            board.Name = "Board";

            SystemMessageBroker.Send(new EntityCreated(board));

            return board;
        }
    }
}
