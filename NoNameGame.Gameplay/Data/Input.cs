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

    public class MoveLeftIntent : IIntent { }
    public class MoveRightIntent : IIntent { }
    public class MoveUpIntent : IIntent { }
    public class MoveDownIntent : IIntent { }
    public class ConfirmIntent : IIntent { }
    public class CancelIntent : IIntent { }

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
                        Intent = new MoveUpIntent(),
                        Key = Keys.Up
                    },
                    new InputIntent
                    {
                        Intent = new MoveDownIntent(),
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
                        Intent = new MoveUpIntent(),
                        Key = Keys.Up
                    },
                    new InputIntent
                    {
                        Intent = new MoveDownIntent(),
                        Key = Keys.Down
                    },
                    new InputIntent
                    {
                        Intent = new MoveLeftIntent(),
                        Key = Keys.Left
                    },
                    new InputIntent
                    {
                        Intent = new MoveRightIntent(),
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
