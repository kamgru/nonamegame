using Game1.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Board : Entity
    {
        private IEnumerable<Tile> _tiles;

        public Board(IEnumerable<Tile> tiles)
        {
            _tiles = tiles;
        }

        public TileInfo GetTileInfoAt(Point point)
        {
            return _tiles.FirstOrDefault(x => x.TileInfo.Position == point)?.TileInfo;
        }
                
        public Point GetBoardSize()
        {
            return new Point(
                _tiles.Max(t => t.TileInfo.Position.X) + 1,
                _tiles.Max(t => t.TileInfo.Position.Y) + 1
            );
        }

    }
}
