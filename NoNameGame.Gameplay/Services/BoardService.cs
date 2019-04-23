using NoNameGame.Gameplay.Data;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.Services
{
    public class BoardService
    {
        private readonly IEnumerable<BoardData> _boards = new[]
        {
            new BoardData
            {
                Tiles = new[]
                {
                    new TileData { Value = 1, X = 0, Y = 0, TileType = TileType.Start },
                    new TileData { Value = 1, X = 1, Y = 0, TileType = TileType.Single },
                    new TileData { Value = 1, X = 2, Y = 0, TileType = TileType.Single },
                    new TileData { Value = 1, X = 3, Y = 0, TileType = TileType.Single },
                    new TileData { Value = 1, X = 0, Y = 1, TileType = TileType.Single },
                    new TileData { Value = 1, X = 1, Y = 1, TileType = TileType.Single },
                    new TileData { Value = 1, X = 2, Y = 1, TileType = TileType.Single },
                    new TileData { Value = 2, X = 3, Y = 1, TileType = TileType.Double },
                    new TileData { Value = 1, X = 4, Y = 1, TileType = TileType.Single },
                    new TileData { Value = 1, X = 3, Y = 2, TileType = TileType.End },

                }
            },new BoardData
            {
                Tiles = new []
                {
                    new TileData { Value = 1, X = 0, Y = 0, TileType = TileType.Start},
                    new TileData { Value = 1, X = 1, Y = 0, TileType = TileType.Single},
                    new TileData { Value = 1, X = 2, Y = 0, TileType = TileType.Single},
                    new TileData { Value = 1, X = 3, Y = 0, TileType = TileType.Single},
                    new TileData { Value = 3, X = 4, Y = 0, TileType = TileType.Triple},
                    new TileData { Value = 1, X = 5, Y = 0, TileType = TileType.End},
                }
            },
        };

        public BoardData GetBoard(int id)
        {
            if (id > _boards.Count() - 1)
            {
                return _boards.FirstOrDefault();
            }

            return _boards.ElementAt(id);
        }
    }
}
