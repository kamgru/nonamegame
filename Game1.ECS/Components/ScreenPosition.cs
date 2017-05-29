using System;
using Microsoft.Xna.Framework;
using Game1.ECS.Core;

namespace Game1.ECS.Components
{
    public class ScreenPosition : ComponentBase
    {
        private Vector2 _position;
        private ScreenPosition _parent;

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
            if (parent != null)
            {
                _parent = parent;
                _parent.OnPositionChange += Parent_OnPositionChange;
            }
            else if (_parent != null)
            {
                _parent.OnPositionChange -= Parent_OnPositionChange;
                _parent = null;
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
