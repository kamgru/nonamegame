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

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                var end = entityState.Entity as End;
                end.Animator.Play(AnimationDictionary.EndOpen);
                end.State.InTransition = false;
            }
        }
    }
}
