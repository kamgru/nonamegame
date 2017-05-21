using System;
using Microsoft.Xna.Framework;

namespace Game1.Components
{
    public class ScreenPosition : ComponentBase
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPositionChange?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler OnPositionChange;

        public void SetParent(ScreenPosition parent)
        {
            if (parent == null)
            {
                parent.OnPositionChange -= Parent_OnPositionChange;
            }
            else
            {
                parent.OnPositionChange += Parent_OnPositionChange;
            }
        }

        private void Parent_OnPositionChange(object sender, EventArgs e)
        {
            var parent = sender as ScreenPosition;
            if (parent != null)
            {
                Position += parent.Position;
            }
        }
    }
}
