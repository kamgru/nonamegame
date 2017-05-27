using Game1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Services
{
    public class BoardService
    {
        public Board GetBoard(int id)
        {
            return new Board
            {
                Tiles = new[]
                {
                    new Tile { Value = 1, X = 0, Y = 0, TextureName = "grey_tile_start", SheetName = "tile_break", TileType = TileType.Start },
                    new Tile { Value = 1, X = 1, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 2, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 3, Y = 0, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 0, Y = 1, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 1, Y = 1, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 2, Y = 1, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 2, X = 3, Y = 1, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 4, Y = 1, TextureName = "grey_tile", SheetName = "tile_break", TileType = TileType.Normal },
                    new Tile { Value = 1, X = 3, Y = 2, TextureName = "grey_tile_end", SheetName = "tile_break", TileType = TileType.End },

                }
            };
        }
    }
}
