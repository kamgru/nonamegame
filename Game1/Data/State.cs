using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Data
{
    public struct State
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class Constants
    {
        public static class PlayerStates
        {
            public static State Idle = new State
            {
                Id = 1,
                Name = "Idle"
            };

            public static State Moving = new State
            {
                Id = 2,
                Name = "Moving"
            };
        }
    }
}
