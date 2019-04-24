using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerDeadHandler : StateHandlerBase
    {
        public PlayerDeadHandler()
            : base(PlayerStates.Dead)
        {
        }

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var state = entity.GetComponent<State>();
            if (state.InTransition)
            {
                Entity.Destroy(entity);
            }
        }
    }
}
