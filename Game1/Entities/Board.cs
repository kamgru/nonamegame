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

        public BoardInfo BoardInfo { get; private set; }
        public Board(IEnumerable<Tile> tiles)
        {
            if (tiles == null)
            {
                throw new ArgumentNullException();
            }

            if (!tiles.Any())
            {
                throw new ArgumentException();
            }

            _tiles = tiles;
            BoardInfo = AddComponent(new BoardInfo
            {
                Size = new Point(_tiles.Max(t => t.TileInfo.Position.X) + 1, _tiles.Max(t => t.TileInfo.Position.Y) + 1)
            }) as BoardInfo;
        }

        public TileInfo GetTileInfoAt(Point point)
        {
            return _tiles.FirstOrDefault(x => x.TileInfo.Position == point)?.TileInfo ?? default(TileInfo);
        }
    }
}
