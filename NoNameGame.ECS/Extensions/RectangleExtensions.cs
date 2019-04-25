using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.ECS
{
    public static class RectangleExtensions
    {
        public static Vector2 TopLeft(this Rectangle rectangle)
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }
    }
}
