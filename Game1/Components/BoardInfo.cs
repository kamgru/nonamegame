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
        public IEnumerable<TileInfo> TileInfos { get; set; }
        public Point Size { get; set; }

        public TileInfo GetTileInfoAt(Point point)
        {
            return TileInfos.FirstOrDefault(x => x.Position == point);
        }
    }
}
