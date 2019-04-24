using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Systems.StateHandling
{
    public abstract class StateHandlerBase
    {
        public string State { get; }

        protected StateHandlerBase(string state)
        {
            State = state;
        }

        public abstract void UpdateState(Entity entity, GameTime gameTime);
    }
}
