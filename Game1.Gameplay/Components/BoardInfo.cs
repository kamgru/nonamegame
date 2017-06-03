using NoNameGame.ECS.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay.Components
{
    public class BoardInfo : ComponentBase
    {
        public Point Size { get; set; }
    }
}
