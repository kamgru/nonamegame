using Microsoft.Xna.Framework.Input;
using NoNameGame.ECS.Input;
using System.Collections.Generic;
using System.Linq;

namespace NoNameGame.Gameplay.Data
{
    public static class Contexts
    {
        public static readonly string Gameplay = "gameplay";
        public static readonly string Menu = "menu";
    }

    public class MovePlayerLeftIntent : IIntent { }
    public class MovePlayerRightIntent : IIntent { }
    public class MovePlayerUpIntent : IIntent { }
    public class MovePlayerDownIntent : IIntent { }
    public class ConfirmIntent : IIntent { }
    public class CancelIntent : IIntent { }
    public class MenuUpIntent : IIntent { }
    public class MenuDownIntent : IIntent { }

    public class InputMapProvider : IInputMapProvider
    {
        private List<InputContext> _contexts = new List<InputContext>
        {
            new InputContext
            {
                Id = Contexts.Menu,
                InputIntentMap = new []
                {
                    new InputIntent
                    {
                        Intent = new ConfirmIntent(),
                        Key = Keys.Enter
                    },
                    new InputIntent
                    {
                        Intent = new MenuUpIntent(),
                        Key = Keys.Up
                    },
                    new InputIntent
                    {
                        Intent = new MenuDownIntent(),
                        Key = Keys.Down
                    }
                }
            },
            new InputContext
            {
                Id = Contexts.Gameplay,
                InputIntentMap = new []
                {
                    new InputIntent
                    {
                        Intent = new MovePlayerUpIntent(),
                        Key = Keys.Up
                    },
                    new InputIntent
                    {
                        Intent = new MovePlayerDownIntent(),
                        Key = Keys.Down
                    },
                    new InputIntent
                    {
                        Intent = new MovePlayerLeftIntent(),
                        Key = Keys.Left
                    },
                    new InputIntent
                    {
                        Intent = new MovePlayerRightIntent(),
                        Key = Keys.Right
                    },
                    new InputIntent
                    {
                        Intent = new ConfirmIntent(),
                        Key = Keys.Enter
                    },
                    new InputIntent
                    {
                        Intent = new CancelIntent(),
                        Key = Keys.Escape
                    }
                }
            }
        };

        public IEnumerable<InputContext> GetActiveContexts()
        {
            return _contexts.Where(x => x.Active);
        }

        public InputContext GetContextById(string id)
        {
            return _contexts.FirstOrDefault(x => x.Id == id);
        }
    }
}
