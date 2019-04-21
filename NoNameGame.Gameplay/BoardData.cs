using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay
{
    public class BoardData
    {
        public IReadOnlyCollection<TileData> Tiles { get; set; }
    }
}
