using NoNameGame.Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoNameGame.ECS.Core;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay.Components
{
    public class TileInfo : ComponentBase
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public bool Destroyed { get; set; }
        public TileType TileType { get; set; }
    }
}
