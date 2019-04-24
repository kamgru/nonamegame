using Microsoft.Xna.Framework;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using NoNameGame.Gameplay.Events;

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
            var player = entity as Player;

            if (player.State.InTransition)
            {
                player.Animator.Play(AnimationDictionary.PlayerFall);
                player.State.InTransition = false;
            }
            else
            {
                if (FallAnimationStillPlaying(player))
                {
                    return;
                }

                Entity.Destroy(player);
                GameEventManager.Raise(new PlayerDied());
            }
        }

        private bool FallAnimationStillPlaying(Player player)
        {
            return player.Animator.CurrentAnimation.Name == AnimationDictionary.PlayerFall
                && player.Animator.IsPlaying;
        }
    }
}
