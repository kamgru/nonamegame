using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoNameGame.ECS.Core;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay.Components
{
    public class Player : ComponentBase
    {
        public string Name { get; set; } = "Player";
    }
}
