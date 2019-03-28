using NoNameGame.ECS.Api;
using NoNameGame.ECS.Components;
using Microsoft.Xna.Framework;
using System.Linq;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.ECS.Systems
{
    public class AnimationSystem 
        : SystemBase, 
        IUpdatingSystem,
        IMessageListener<ComponentAdded<Animator>>
    {
        private readonly int _fps;

        public AnimationSystem(int fps)
        {
            _fps = fps;
            SystemMessageBroker.AddListener<ComponentAdded<Animator>>(this);
        }

        public void Handle(ComponentAdded<Animator> message)
        {
            Entities.Add(message.Entity);
        }

        public override void Handle(EntityCreated message)
        {
            if (message.Entity.HasComponent<Animator>())
            {
                Entities.Add(message.Entity);
            }
        }

        public void Update(GameTime gameTime)
        {
            var animators = Entities.Select(x => x.GetComponent<Animator>());

            foreach (var animator in animators.Where(x => x.IsPlaying && x.CurrentAnimation != null))
            {
                var sprite = animator.Entity.GetComponent<Sprite>();
                if (sprite == null)
                {
                    continue;
                }

                var animation = animator.CurrentAnimation;

                animation.Elapsed += gameTime.ElapsedGameTime.Milliseconds;

                if (animation.Elapsed > _fps / animation.Speed)
                {
                    animation.CurrentFrame++;
                    if (animation.CurrentFrame >= animation.FrameCount)
                    {
                        animation.CurrentFrame = 0;

                        if (!animation.Looped)
                        {
                            animator.Stop();                    
                            continue;                            
                        }
                    }
                    animation.Elapsed = gameTime.ElapsedGameTime.Milliseconds;

                    sprite.Texture2D = animation.Texture2D;
                    sprite.Rectangle = animation.CurrentRectangle;
                }
            }
        }
    }
}
