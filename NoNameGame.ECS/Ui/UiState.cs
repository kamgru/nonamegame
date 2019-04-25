using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.ECS.Ui
{
    internal sealed class UiState
    {
        public int HotItemId { get; set; }
        public int ActiveItemId { get; set; }
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public bool LeftButtonDown { get; private set; }

        public void Prepare()
        {
            var mouseState = Mouse.GetState();

            MouseX = mouseState.X;
            MouseY = mouseState.Y;
            LeftButtonDown = mouseState.LeftButton == ButtonState.Pressed;
            HotItemId = 0;
        }

        public void Cleanup()
        {
            if (!LeftButtonDown)
            {
                ActiveItemId = 0;
            }
            else if (ActiveItemId == 0)
            {
                ActiveItemId = -1;
            }
        }

        public bool MouseOver(Rectangle rectangle)
        {
            return MouseX > rectangle.X
                && MouseY > rectangle.Y
                && MouseX <= rectangle.X + rectangle.Width
                && MouseY <= rectangle.Y + rectangle.Height;
        }
    }
}
