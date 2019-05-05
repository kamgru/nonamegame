using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.ECS.Ui
{
    internal sealed class MouseState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool LeftButtonDown { get; set; }

        public bool MouseOver(Rectangle rectangle)
        {
            return X > rectangle.X
                && Y > rectangle.Y
                && X <= rectangle.X + rectangle.Width
                && Y <= rectangle.Y + rectangle.Height;
        }

        public void UpdateState()
        {
            var mouseState = Mouse.GetState();
            X = mouseState.X;
            Y = mouseState.Y;
            LeftButtonDown = mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
