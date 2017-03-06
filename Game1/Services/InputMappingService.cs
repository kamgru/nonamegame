using Game1.Api;
using Game1.Data;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1.Services
{

    public class InputMappingService : IInputMappingService
    {
        private KeyboardState _previousState = Keyboard.GetState();

        public Intent GetIntents()
        {
            Intent intent = 0;

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A) && !_previousState.IsKeyDown(Keys.A))
            {
                intent |= Intent.MoveLeft;
            }

            if (state.IsKeyDown(Keys.D) && !_previousState.IsKeyDown(Keys.D))
            {
                intent |= Intent.MoveRight;
            }

            if (state.IsKeyDown(Keys.W) && !_previousState.IsKeyDown(Keys.W))
            {
                intent |= Intent.MoveUp;
            }

            if (state.IsKeyDown(Keys.S) && !_previousState.IsKeyDown(Keys.S))
            {
                intent |= Intent.MoveDown;
            }

            _previousState = state;

            return intent;
        }
    }
}
