﻿using Game1.Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game1.ECS.Core;
using System.Threading.Tasks;

namespace Game1.Gameplay.Components
{
    public class TileInfo : ComponentBase
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public bool Destroyed { get; set; }
        public TileType TileType { get; set; }
    }
}