using System;
using Microsoft.Xna.Framework;

namespace Game1.Components
{
    public class TransformComponent : ComponentBase
    {
        private Vector2 _position = new Vector2();
        private TransformComponent _parent;

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

        public void SetParent(TransformComponent parent)
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
            var parent = sender as TransformComponent;
            if (parent != null)
            {
                Position += parent.Position;
            }
        }
    }
}
