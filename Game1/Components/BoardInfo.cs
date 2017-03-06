using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    public class BoardInfo : ComponentBase
    {
        private ICollection<TileInfo> _tiles;

        public ICollection<TileInfo> TileInfos
        {
            get { return _tiles; }
            set { _tiles = value; }
        }
        public Point Size { get; set; }

        public TileInfo GetTileAt(Point point)
        {
            return TileInfos.FirstOrDefault(x => x.Position == point);
        }

        public void RemoveTileAt(Point point)
        {
            var tile = _tiles.FirstOrDefault(x => x.Position == point);
            if (tile != null)
            {
                _tiles.Remove(tile);
            }
        }
    }
}
