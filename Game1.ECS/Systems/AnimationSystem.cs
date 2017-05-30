﻿using Game1.ECS.Api;
using Game1.ECS.Components;
using Game1.ECS.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ECS.Systems
{
    public class AnimationSystem : SystemBase, IUpdatingSystem
    {
        private readonly int _fps;

        public AnimationSystem(IEntityManager entityManager, int fps)
            :base(entityManager)
        {
            _fps = fps;
        }

        public void Update(GameTime gameTime)
        {
            var animators = EntityManager.GetEntitiesByComponent<Animator>().Select(x => x.GetComponent<Animator>());

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