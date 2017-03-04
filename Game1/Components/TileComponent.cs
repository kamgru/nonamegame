using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    public enum TileType
    {
        Normal = 1,
        Start = 2,
    }

    public class TileComponent : ComponentBase
    {
        public int Value { get; set; }
        public TileType TileType { get; set; }
    }
}
