﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Components
{
    public class Sprite : ComponentBase
    {
        public Texture2D Texture2D { get; set; }
        public Rectangle? Rectangle { get; set; }
    }
}
