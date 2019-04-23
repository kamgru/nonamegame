using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.Gameplay.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.Gameplay.Entities
{
    public class End : Entity
    {
        public Sprite Sprite { get; set; }
        public Animator Animator { get; set; }   
        public PositionOnBoard PositionOnBoard { get; set; }
        public State State { get; set; }
    }
}
