using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Data
{
    public enum Intent
    {
        MoveUp = 1,
        MoveDown = 2,
        MoveLeft = 3, 
        MoveRight = 4,
        Confirm = 8,
        Cancel = 16
    }
}
