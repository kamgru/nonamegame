using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoNameGame.ECS.Core;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay.Components
{
    public class PositionOnBoard : ComponentBase
    {
        public Point Current { get; set; }
        public Point Previous { get; set; }

        public void Translate(Point translation)
        {
            Previous = Current;
            Current += translation;
        }
    }
}
