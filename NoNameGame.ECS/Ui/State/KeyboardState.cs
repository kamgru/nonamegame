using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;

namespace NoNameGame.ECS.Ui
{
    internal sealed class KeyboardUiState
    {
        public char CurrentCharacter { get; set; }
        public Keys LastKey { get; set; }
    }
}
