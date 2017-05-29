using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game1.ECS.Core;
using System.Threading.Tasks;

namespace Game1.Components
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
