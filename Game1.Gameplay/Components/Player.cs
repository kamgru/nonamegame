using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game1.ECS.Core;
using System.Threading.Tasks;

namespace Game1.Gameplay.Components
{
    public class Player : ComponentBase
    {
        public string Name { get; set; } = "Player";
    }
}
