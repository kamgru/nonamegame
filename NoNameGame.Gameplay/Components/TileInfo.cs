using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Data;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.Components
{
    public class TileInfo : ComponentBase
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public bool IsClearable { get; private set; }
        public TileType TileType
        {
            get { return tileType; }
            set
            {
                tileType = value;
                IsClearable = _clearableTypes.Any(item => item == tileType);
            }
        }

        private TileType tileType;
        private static IEnumerable<TileType> _clearableTypes = new[]
        {
            TileType.Single,
            TileType.Double,
            TileType.Triple,
        };
    }
}
