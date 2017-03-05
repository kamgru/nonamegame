using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    public class BoardPosition : ComponentBase
    {
        public Point Current { get; set; }
        public Point Previous { get; set; }
    }
}
