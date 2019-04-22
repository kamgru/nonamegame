using System;

namespace NoNameGame.Main.Gui
{
    public class MenuItem
    {
        public string Text { get; set; }

        public event EventHandler OnSelected;

        public void Select()
            => OnSelected?.Invoke(this, EventArgs.Empty);

    }
}