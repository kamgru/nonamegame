using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Input
{
    public class InputProvider
    {
        private KeyboardState _previousKeyboardState = new KeyboardState();

        public ICollection<Keys> GetPressedKeys()
        {
            var currentKeyboardState = Keyboard.GetState();

            var pressedKeys = currentKeyboardState.GetPressedKeys()
                .Except(_previousKeyboardState.GetPressedKeys())
                .ToList();

            _previousKeyboardState = currentKeyboardState;

            return pressedKeys;
        }
    }
}
