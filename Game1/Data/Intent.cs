using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Data
{
    [Flags]
    public enum Intent
    {
        MoveUp = 1,
        MoveDown = 2,
        MoveLeft = 4, 
        MoveRight = 8
    }
}
