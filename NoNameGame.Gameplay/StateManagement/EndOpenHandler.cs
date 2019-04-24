using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

namespace NoNameGame.Gameplay.StateManagement
{
    public class EndOpenHandler : StateHandlerBase
    {
        public EndOpenHandler()
            : base(EndStates.Open)
        {
        }

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var end = entity as End;
            if (end.State.InTransition)
            {
                end.Animator.Play(AnimationDictionary.EndOpen);
                end.State.InTransition = false;
            }
        }
    }
}
