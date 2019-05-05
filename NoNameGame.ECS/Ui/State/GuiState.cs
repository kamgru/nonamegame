namespace NoNameGame.ECS.Ui
{
    internal sealed class GuiState
    {
        public int HotItemId { get; set; }
        public int ActiveItemId { get; set; }
        
        public int ActiveTextBoxId { get; set; }

        public MouseState Mouse { get; set; } = new MouseState();
        public KeyboardUiState Keyboard { get; set; } = new KeyboardUiState();

        public void Begin()
        {
            Mouse.UpdateState();

            HotItemId = 0;
        }

        public void End()
        {
            if (!Mouse.LeftButtonDown)
            {
                ActiveItemId = 0;
            }
            else if (ActiveItemId == 0)
            {
                ActiveItemId = -1;
            }

            Keyboard.CurrentCharacter = default;
            Keyboard.LastKey = Microsoft.Xna.Framework.Input.Keys.None;
        }
    }
}
