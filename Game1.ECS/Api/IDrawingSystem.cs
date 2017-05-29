using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ECS.Api
{
    public interface IDrawingSystem : ISystem
    {
        void Draw();
    }
}
