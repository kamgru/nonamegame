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
                    new TileData { Value = 1, X = 0, Y = 0, TextureName = "grey_tile_start", SheetName = "tile_break", TileType = TileType.Start },
                    new TileData { Value = 1, X = 1, Y = 0, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 2, Y = 0, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 3, Y = 0, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 0, Y = 1, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 1, Y = 1, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 2, Y = 1, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 2, X = 3, Y = 1, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 4, Y = 1, TextureName = "ice_tile_32", SheetName = "ice_breaking_32", TileType = TileType.Normal },
                    new TileData { Value = 1, X = 3, Y = 2, TextureName = "grey_tile_end", SheetName = "tile_break", TileType = TileType.End },

                }
            },new BoardData
            {
                Tiles = new []
                {
                    new TileData { Value = 1, X = 0, Y = 0, TextureName = "grey_tile_start", SheetName = "tile_break", TileType = TileType.Start},
                    new TileData { Value = 1, X = 1, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal},
                    new TileData { Value = 1, X = 2, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal},
                    new TileData { Value = 1, X = 3, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal},
                    new TileData { Value = 1, X = 4, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal},
                    new TileData { Value = 1, X = 5, Y = 0, TextureName = "grey_tile_end", SheetName = "tile_break", TileType = TileType.End},
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
