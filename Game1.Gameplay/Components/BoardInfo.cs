using Game1.ECS.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Gameplay.Components
{
    public class BoardInfo : ComponentBase
    {
        public Point Size { get; set; }
    }
}
